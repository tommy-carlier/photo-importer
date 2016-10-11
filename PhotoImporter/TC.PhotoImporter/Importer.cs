using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace TC.PhotoImporter
{
    sealed class Importer
    {
        readonly Settings _settings;
        readonly IImportProgressReceiver _progress;

        public Importer(Settings settings, IImportProgressReceiver progress)
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
                _progress.ReportFileCount(sourceFiles.Length);

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

        private FileInfo[] GetSourceFiles()
        {
            try
            {
                var sourceFolder = new DirectoryInfo(_settings.SourceFolderPath);
                return sourceFolder.GetFiles("*.jpg", SearchOption.TopDirectoryOnly);
            }
            catch(SecurityException ex)
            {
                throw new ImportException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Properties.Resources.AccessDeniedToSourceFolder,
                        _settings.SourceFolderPath),
                    ex);
            }
        }

        private void Import(FileInfo sourceFile)
        {
            _progress.ReportFileStarted(sourceFile.Name);

            string destinationFolderPath = GetDestinationFolderPath(sourceFile);
            string destinationFilePath = Path.Combine(destinationFolderPath, sourceFile.Name);

            using (var sourceImage = ReadImage(sourceFile.FullName))
            {
                Size destinationSize = CalculateDestinationSize(sourceImage.Size);
                using (var destinationImage = CreateDestinationImage(sourceImage, destinationSize))
                {
                    CopyExifData(sourceImage, destinationImage);
                    SaveJpeg(destinationImage, destinationFilePath);
                }
            }

            CopyCreationTime(sourceFile, destinationFilePath);
            if (_settings.DeleteSourceFiles)
            {
                Delete(sourceFile);
            }

            _progress.ReportFileFinished();
        }

        private string GetDestinationFolderPath(FileInfo sourceFile)
        {
            DateTime creationTime = sourceFile.CreationTime;
            string path = Path.Combine(
                _settings.DestinationFolderPath,
                creationTime.ToString("yyyy", CultureInfo.InvariantCulture),
                creationTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch(UnauthorizedAccessException ex)
                {
                    throw new ImportException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            Properties.Resources.AccessDeniedToDestinationFolder,
                            path),
                        ex);
                }
                catch(PathTooLongException ex)
                {
                    throw new ImportException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            Properties.Resources.PathOfDestinationFolderTooLong,
                            path),
                        ex);
                }
                catch(IOException ex)
                {
                    throw new ImportException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            Properties.Resources.ErrorWhileCreatingFolder,
                            path,
                            ex.Message),
                        ex);
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
                throw new ImportException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Properties.Resources.CannotReadImageFromPath,
                        filePath),
                    ex);
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
                throw new ImportException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Properties.Resources.CannotSaveJpegFile,
                        filePath,
                        ex.Message),
                    ex);
            }
        }

        private static void CopyCreationTime(FileInfo sourceFile, string destinationFilePath)
        {
            try
            {
                File.SetCreationTimeUtc(destinationFilePath, sourceFile.CreationTimeUtc);
            }
            catch(UnauthorizedAccessException ex)
            {
                throw new ImportException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Properties.Resources.CannotSetCreationTimeAccessDenied,
                        destinationFilePath),
                    ex);
            }
            catch(IOException ex)
            {
                throw new ImportException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Properties.Resources.CannotSetCreationTimeUnknownError,
                        destinationFilePath,
                        ex.Message),
                    ex);
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
                throw new ImportException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Properties.Resources.CannotDeleteSourceFileAccessDenied,
                        file.FullName),
                    ex);
            }
            catch(IOException ex)
            {
                throw new ImportException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        Properties.Resources.CannotDeleteSourceFileUnknownError,
                        file.FullName,
                        ex.Message),
                    ex);
            }
        }
    }
}
