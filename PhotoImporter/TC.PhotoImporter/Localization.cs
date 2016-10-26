using System;
using System.Globalization;

namespace TC.PhotoImporter
{
    internal static class Localization
    {
        internal static string Format(string format, object arg)
        {
            return string.Format(CultureInfo.InvariantCulture, format, arg);
        }

        internal static string Format(string format, object arg1, object arg2)
        {
            return string.Format(CultureInfo.InvariantCulture, format, arg1, arg2);
        }

        internal static string Format(string format, object arg1, object arg2, object arg3)
        {
            return string.Format(CultureInfo.InvariantCulture, format, arg1, arg2, arg3);
        }

        internal static Exception CreateImportException(Exception innerException, string messageFormat, object arg)
        {
            return new ImportException(Format(messageFormat, arg), innerException);
        }

        internal static Exception CreateImportException(Exception innerException, string messageFormat, object arg1, object arg2)
        {
            return new ImportException(Format(messageFormat, arg1, arg2), innerException);
        }
    }
}
