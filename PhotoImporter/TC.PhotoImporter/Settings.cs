using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TC.PhotoImporter
{
    sealed class Settings
    {
        readonly Properties.Settings _settings;

        private Settings(Properties.Settings settings)
        {
            _settings = settings;
        }

        public static readonly Settings Instance = new Settings(Properties.Settings.Default);

        public string SourceFolderPath { get { return _settings.SourceFolderPath; } }
        public string DestinationFolderPath { get { return _settings.DestinationFolderPath; } }
        public int MaxWidthOrHeight { get { return _settings.MaxWidthOrHeight; } }
        public long Quality { get { return _settings.Quality; } }
        public bool DeleteSourceFiles { get { return _settings.DeleteSourceFiles; } }

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
            yield return GetFolderPathError(_settings.SourceFolderPath, nameof(_settings.SourceFolderPath));
            yield return GetFolderPathError(_settings.DestinationFolderPath, nameof(_settings.DestinationFolderPath));
        }

        private static string GetFolderPathError(string path, string settingName)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Format(
                    CultureInfo.InvariantCulture, 
                    Properties.Resources.SettingCannotBeEmpty, 
                    settingName);
            }

            try
            {
                Path.GetFullPath(path);
            }
            catch (PathTooLongException)
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Resources.SettingContainsPathTooLong, 
                    settingName, 
                    path);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is NotSupportedException)
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Resources.SettingContainsInvalidPath,
                    settingName,
                    path);
            }

            return null;
        }
    }
}
