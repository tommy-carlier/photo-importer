using System;
using System.Runtime.Serialization;

namespace TC.PhotoImporter
{
    [Serializable]
    public sealed class ImportException : Exception
    {
        public ImportException() { }
        public ImportException(string message) : base(message) { }
        public ImportException(string message, Exception inner) : base(message, inner) { }
        private ImportException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
