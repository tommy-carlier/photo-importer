namespace TC.PhotoImporter
{
    interface IImportProgressReceiver
    {
        void ReportStarted();
        void ReportFileCount(int fileCount);
        void ReportFileStarted(string fileName);
        void ReportFileFinished();
        void ReportAllFinished();
        void ReportFailure(string errorMessage);
    }
}
