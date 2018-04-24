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
        private readonly string _readError, _sourceFolderPath, _destinationFolderPath;
        private readonly int _maxWidthOrHeight;
        private readonly long _quality;
        private readonly bool _deleteSourceFiles;

        private Settings(
            string readError,
            string sourceFolderPath,
            string destinationFolderPath,
            int maxWidthOrHeight,
            long quality,
            bool deleteSourceFiles)
        {
            _readError = readError;
            _sourceFolderPath = sourceFolderPath;
            _destinationFolderPath = destinationFolderPath;
            _maxWidthOrHeight = maxWidthOrHeight;
            _quality = quality;
            _deleteSourceFiles = deleteSourceFiles;
        }

        public string SourceFolderPath { get { return _sourceFolderPath; } }
        public string DestinationFolderPath { get { return _destinationFolderPath; } }
        public int MaxWidthOrHeight { get { return _maxWidthOrHeight; } }
        public long Quality { get { return _quality; } }
        public bool DeleteSourceFiles { get { return _deleteSourceFiles; } }

        public static Settings ReadFromFile(string filePath)
        {
            string readError = "", sourceFolderPath = "", destinationFolderPath = "";
            int maxWidthOrHeight = 0;
            long quality = 80;
            bool deleteSourceFiles = false;

            try
            {
                foreach (var setting in new SettingsFileReader(filePath).ReadSettings())
                {
                    switch (setting.Key)
                    {
                        case "SourceFolderPath":
                            sourceFolderPath = setting.Value;
                            break;

                        case "DestinationFolderPath":
                            destinationFolderPath = setting.Value;
                            break;

                        case "MaxWidthOrHeight":
                            maxWidthOrHeight = ParseInt32(setting.Value);
                            break;

                        case "Quality":
                            quality = ParseInt32(setting.Value);
                            break;

                        case "DeleteSourceFiles":
                            deleteSourceFiles = ParseBoolean(setting.Value);
                            break;
                    }
                }
            }
            catch (ImportException ex)
            {
                readError = ex.Message;
            }

            return new Settings(readError, sourceFolderPath, destinationFolderPath, maxWidthOrHeight, quality, deleteSourceFiles);
        }

        private static int ParseInt32(string value)
        {
            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
            {
                return result;
            }
            return 0;
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
                yield return GetFolderPathError(_sourceFolderPath, "SourceFolderPath");
                yield return GetFolderPathError(_destinationFolderPath, "DestinationFolderPath");
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
