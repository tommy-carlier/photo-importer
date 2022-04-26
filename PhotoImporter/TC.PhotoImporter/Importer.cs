using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

using static TC.PhotoImporter.Localization;

namespace TC.PhotoImporter
{
    sealed class Importer
    {
        readonly Settings _settings;
        readonly IImportProgressReporter _progress;

        public Importer(Settings settings, IImportProgressReporter progress)
        {
            _settings = settings;
            _progress = progress;
        }

        public void Start()
        {
            _progress.ReportStarted();
            Task.Run(new Action(ImportAll));
        }

        private void ImportAll()
        {
            try
            {
                var sourceFiles = GetSourceFiles();
                _progress.ReportFileCount(sourceFiles.Count);

                foreach (var sourceFile in sourceFiles)
                {
                    Import(sourceFile);
                }

                _progress.ReportAllFinished();
            }
            catch(ImportException ex)
            {
                _progress.ReportFailure(ex.Message);
            }
        }

        private IReadOnlyList<FileInfo> GetSourceFiles()
        {
            try
            {
                var sourceFolder = new DirectoryInfo(_settings.SourceFolderPath);
                return Enumerable.Concat(
                    sourceFolder.EnumerateFiles("*.jpg", SearchOption.AllDirectories),
                    sourceFolder.EnumerateFiles("*.jpeg", SearchOption.AllDirectories)
                    ).ToList();
            }
            catch(SecurityException ex)
            {
                throw CreateImportException(ex, Properties.Resources.AccessDeniedToSourceFolder, _settings.SourceFolderPath);
            }
        }

        private void Import(FileInfo sourceFile)
        {
            _progress.ReportFileStarted(sourceFile.Name);

            using (var sourceImage = ReadImage(sourceFile.FullName))
            {
                DateTime creationTime = GetCreationTime(sourceImage, sourceFile);
                _progress.ReportFileCreationTime(creationTime);

                string destinationFolderPath = GetDestinationFolderPath(creationTime);
                string destinationFilePath = Path.Combine(destinationFolderPath, sourceFile.Name);

                Size destinationSize = CalculateDestinationSize(sourceImage.Size);
                using (var destinationImage = CreateDestinationImage(sourceImage, destinationSize))
                {
                    CopyExifData(sourceImage, destinationImage);
                    SaveJpeg(destinationImage, destinationFilePath);
                }

                SetCreationTime(destinationFilePath, creationTime);
            }
            if (_settings.DeleteSourceFiles)
            {
                Delete(sourceFile);
            }

            _progress.ReportFileFinished();
        }

        private static DateTime GetCreationTime(Image image, FileInfo file)
        {
            string propertyValue = GetPropertyValue(image, 0x9003 /* DateTimeOriginal */);
            if(propertyValue != null && DateTime.TryParseExact(propertyValue, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime creationTime))
            {
                return creationTime;
            }

            return Min(file.CreationTime, file.LastWriteTime);
        }

        private static string GetPropertyValue(Image image, int propID)
        {
            try
            {
                var propertyItem = image.GetPropertyItem(propID);
                if (propertyItem != null)
                {
                    return System.Text.Encoding.ASCII.GetString(propertyItem.Value, 0, propertyItem.Len - 1);
                }
            }
            catch (ArgumentException) { /* The image format does not support property items */ }
            return null;
        }

        private static DateTime Min(DateTime dt1, DateTime dt2)
        {
            return dt1 < dt2 ? dt1 : dt2;
        }

        private string GetDestinationFolderPath(DateTime creationTime)
        {
            string path = _settings.GroupByYear
                ? Path.Combine(_settings.DestinationFolderPath, creationTime.FormatYear(), creationTime.FormatDate())
                : Path.Combine(_settings.DestinationFolderPath, creationTime.FormatDate());

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch(UnauthorizedAccessException ex)
                {
                    throw CreateImportException(ex, Properties.Resources.AccessDeniedToDestinationFolder, path);
                }
                catch(PathTooLongException ex)
                {
                    throw CreateImportException(ex, Properties.Resources.PathOfDestinationFolderTooLong, path);
                }
                catch(IOException ex)
                {
                    throw CreateImportException(ex, Properties.Resources.ErrorWhileCreatingFolder, path, ex.Message);
                }
            }

            return path;
        }

        private static Image ReadImage(string filePath)
        {
            try
            {
                return Image.FromFile(filePath, useEmbeddedColorManagement: true);
            }
            catch(OutOfMemoryException ex)
            {
                throw CreateImportException(ex, Properties.Resources.CannotReadImageFromPath, filePath);
            }
        }

        private Size CalculateDestinationSize(Size sourceSize)
        {
            int maxWidthOrHeight = _settings.MaxWidthOrHeight;
            if(maxWidthOrHeight <= 0)
            {
                return sourceSize;
            }

            if(sourceSize.Width >= sourceSize.Height) // landscape orientation
            {
                if(maxWidthOrHeight < sourceSize.Width)
                {
                    return new Size(maxWidthOrHeight, sourceSize.Height * maxWidthOrHeight / sourceSize.Width);
                }
                else
                {
                    return sourceSize;
                }
            }
            else // portrait orientation
            {
                if(maxWidthOrHeight < sourceSize.Height)
                {
                    return new Size(sourceSize.Width * maxWidthOrHeight / sourceSize.Height, maxWidthOrHeight);
                }
                else
                {
                    return sourceSize;
                }
            }
        }

        private static Image CreateDestinationImage(Image sourceImage, Size destinationSize)
        {
            var destinationImage = new Bitmap(destinationSize.Width, destinationSize.Height);
            try
            {
                destinationImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

                using (var graphics = Graphics.FromImage(destinationImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var attributes = new ImageAttributes())
                    {
                        attributes.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(
                            image: sourceImage,
                            destRect: new Rectangle(Point.Empty, destinationSize),
                            srcX: 0, srcY: 0, srcWidth: sourceImage.Width, srcHeight: sourceImage.Height,
                            srcUnit: GraphicsUnit.Pixel,
                            imageAttr: attributes);
                    }
                }

                return destinationImage;
            }
            catch
            {
                destinationImage.Dispose();
                throw;
            }
        }

        private static void CopyExifData(Image sourceImage, Image destinationImage)
        {
            foreach(var item in sourceImage.PropertyItems)
            {
                destinationImage.SetPropertyItem(item);
            }
        }

        private void SaveJpeg(Image image, string filePath)
        {
            try
            {
                using (var parameters = new EncoderParameters(1))
                using (var qualityParameter = new EncoderParameter(Encoder.Quality, _settings.Quality))
                {
                    parameters.Param[0] = qualityParameter;
                    var codec = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    image.Save(filePath, codec, parameters);
                }
            }
            catch(ExternalException ex)
            {
                throw CreateImportException(ex, Properties.Resources.CannotSaveJpegFile, filePath, ex.Message);
            }
        }

        private static void SetCreationTime(string destinationFilePath, DateTime creationTime)
        {
            try
            {
                File.SetCreationTimeUtc(destinationFilePath, creationTime.ToUniversalTime());
            }
            catch(UnauthorizedAccessException ex)
            {
                throw CreateImportException(ex, Properties.Resources.CannotSetCreationTimeAccessDenied, destinationFilePath);
            }
            catch(IOException ex)
            {
                throw CreateImportException(ex, Properties.Resources.CannotSetCreationTimeUnknownError, destinationFilePath, ex.Message);
            }
        }

        private static void Delete(FileInfo file)
        {
            try
            {
                file.Delete();
            }
            catch(SecurityException ex)
            {
                throw CreateImportException(ex, Properties.Resources.CannotDeleteSourceFileAccessDenied, file.FullName);
            }
            catch(IOException ex)
            {
                throw CreateImportException(ex, Properties.Resources.CannotDeleteSourceFileUnknownError, file.FullName);
            }
        }
    }
}
