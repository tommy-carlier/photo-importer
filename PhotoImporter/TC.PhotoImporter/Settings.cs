using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TC.PhotoImporter
{
    using static Environment;
    using static FormattableString;

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
            return string.Join(NewLine, DetermineErrors());
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

        static string GetFolderPathError(string path, string settingName)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return Invariant($"Setting “{settingName}” cannot be empty");
            }

            try
            {
                Path.GetFullPath(path);
            }
            catch (PathTooLongException)
            {
                return Invariant($"Setting “{settingName}” contains a path that is too long{NewLine}(“{path}”)");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is NotSupportedException)
            {
                return Invariant($"Setting “{settingName}” contains an invalid path “{path}”");
            }

            return null;
        }
    }
}
