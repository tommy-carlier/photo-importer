using System;

namespace TC.PhotoImporter
{
    interface IImportProgressReporter
    {
        void ReportStarted();
        void ReportFileCount(int fileCount);
        void ReportFileStarted(string fileName);
        void ReportFileCreationTime(DateTime creationTime);
        void ReportFileFinished();
        void ReportAllFinished();
        void ReportFailure(string errorMessage);
    }
}
