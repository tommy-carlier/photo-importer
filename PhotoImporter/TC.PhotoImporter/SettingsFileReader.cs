using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

using static TC.PhotoImporter.Localization;

namespace TC.PhotoImporter
{
    internal sealed class SettingsFileReader
    {
        private readonly string _filePath;

        internal SettingsFileReader(string filePath)
        {
            _filePath = filePath;
        }

        internal IEnumerable<KeyValuePair<string, string>> ReadSettings()
        {
            string key, value;
            foreach (string line in ReadLines())
            {
                if (TryParseLine(line.Trim(), out key, out value))
                {
                    yield return new KeyValuePair<string, string>(key, value);
                }
            }
        }

        private IEnumerable<string> ReadLines()
        {
            if (!File.Exists(_filePath))
            {
                throw CreateImportException(null, Properties.Resources.SettingsFileNotFound, _filePath);
            }

            try
            {
                return File.ReadLines(_filePath);
            }
            catch (PathTooLongException ex)
            {
                throw CreateImportException(ex, Properties.Resources.SettingsFilePathTooLong, _filePath);
            }
            catch (Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
            {
                throw CreateImportException(ex, Properties.Resources.SettingsFileAccessDenied, _filePath);
            }
            catch (IOException ex)
            {
                throw CreateImportException(ex, Properties.Resources.ErrorWhileReadingSettingsFile, _filePath);
            }
        }

        private static bool TryParseLine(string line, out string key, out string value)
        {
            key = "";
            value = "";

            if (string.IsNullOrEmpty(line))
            {
                return false;
            }

            int indexOfColon = line.IndexOf(':');
            if (indexOfColon <= 0)
            {
                return false;
            }

            key = line.Substring(0, indexOfColon).TrimEnd();
            if (indexOfColon + 1 < line.Length)
            {
                value = line.Substring(indexOfColon + 1).TrimStart();
            }

            return true;
        }
    }
}
