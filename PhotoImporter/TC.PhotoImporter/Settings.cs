using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using static TC.PhotoImporter.Localization;

namespace TC.PhotoImporter
{
    sealed class Settings
    {
        private readonly string _readError;

        private Settings(
            string readError,
            string sourceFolderPath,
            string destinationFolderPath,
            int maxWidthOrHeight,
            long quality,
            bool deleteSourceFiles,
            bool groupByYear,
            string normalizedJpegFileExtension,
            string destinationFileNamePrefix)
        {
            _readError = readError;
            SourceFolderPath = sourceFolderPath;
            DestinationFolderPath = destinationFolderPath;
            MaxWidthOrHeight = maxWidthOrHeight;
            Quality = quality;
            DeleteSourceFiles = deleteSourceFiles;
            GroupByYear = groupByYear;
            NormalizedJpegFileExtension = normalizedJpegFileExtension;
            DestinationFileNamePrefix = destinationFileNamePrefix;
        }

        public string SourceFolderPath { get; }
        public string DestinationFolderPath { get; }
        public int MaxWidthOrHeight { get; }
        public long Quality { get; }
        public bool DeleteSourceFiles { get; }
        public bool GroupByYear { get; }
        public string NormalizedJpegFileExtension { get; }
        public string DestinationFileNamePrefix { get; }

        public static Settings ReadFromFile(string filePath)
        {
            string readError = "", sourceFolderPath = "", destinationFolderPath = "",
                normalizedJpegFileExtension = "", destinationFileNamePrefix = "";
            int maxWidthOrHeight = 0;
            long quality = 80;
            bool deleteSourceFiles = false, groupByYear = true;

            try
            {
                foreach (var setting in new SettingsFileReader(filePath).ReadSettings())
                {
                    switch (setting.Key)
                    {
                        case nameof(SourceFolderPath): sourceFolderPath = setting.Value; break;
                        case nameof(DestinationFolderPath): destinationFolderPath = setting.Value; break;
                        case nameof(MaxWidthOrHeight): maxWidthOrHeight = ParseInt32(setting.Value); break;
                        case nameof(Quality): quality = ParseInt32(setting.Value); break;
                        case nameof(DeleteSourceFiles): deleteSourceFiles = ParseBoolean(setting.Value); break;
                        case nameof(GroupByYear): groupByYear = ParseBoolean(setting.Value); break;
                        case nameof(NormalizedJpegFileExtension): normalizedJpegFileExtension = setting.Value; break;
                        case nameof(DestinationFileNamePrefix): destinationFileNamePrefix = setting.Value; break;
                    }
                }
            }
            catch (ImportException ex)
            {
                readError = ex.Message;
            }

            // make sure that normalizedJpegFileExtension starts with a period (if it's not empty)
            if (normalizedJpegFileExtension.Length > 0 && normalizedJpegFileExtension[0] != '.')
            {
                normalizedJpegFileExtension = "." + normalizedJpegFileExtension;
            }

            return new Settings(
                readError, sourceFolderPath, destinationFolderPath, maxWidthOrHeight, quality,
                deleteSourceFiles, groupByYear, normalizedJpegFileExtension, destinationFileNamePrefix);
        }

        private static int ParseInt32(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result) ? result : 0;
        }

        private static bool ParseBoolean(string value)
        {
            return bool.TryParse(value, out bool result) && result;
        }

        public string Validate()
        {
            return string.Join(Environment.NewLine, DetermineErrors());
        }

        IEnumerable<string> DetermineErrors()
        {
            return DetermineErrorsWithNulls().Where(error => error != null);
        }

        IEnumerable<string> DetermineErrorsWithNulls()
        {
            if (!string.IsNullOrEmpty(_readError))
            {
                yield return _readError;
            }
            else
            {
                yield return GetFolderPathError(SourceFolderPath, nameof(SourceFolderPath));
                yield return GetFolderPathError(DestinationFolderPath, nameof(DestinationFolderPath));
            }
        }

        private static string GetFolderPathError(string path, string settingName)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return Format(Properties.Resources.SettingCannotBeEmpty, settingName);
            }

            try
            {
                Path.GetFullPath(path);
            }
            catch (PathTooLongException)
            {
                return Format(Properties.Resources.SettingContainsPathTooLong, settingName, path);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is NotSupportedException)
            {
                return Format(Properties.Resources.SettingContainsInvalidPath, settingName, path);
            }

            return null;
        }
    }
}
