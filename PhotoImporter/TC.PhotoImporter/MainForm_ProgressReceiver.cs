using System;
using System.Windows.Forms;

using static TC.PhotoImporter.Localization;

namespace TC.PhotoImporter
{
    partial class MainForm
    {
        private sealed class ProgressReceiver : IImportProgressReceiver
        {
            private readonly MainForm _form;
            private readonly Label _statusLabel;
            private readonly FileProgressTracker _progress;

            internal ProgressReceiver(MainForm form)
            {
                _form = form;
                _statusLabel = form._statusLabel;
                _progress = new FileProgressTracker(form._progress);
            }

            #region IImportProgressReceiver implementation

            void IImportProgressReceiver.ReportStarted()
            {
                InvokeUI(ReportStartedUI);
            }

            void IImportProgressReceiver.ReportFileCount(int fileCount)
            {
                InvokeUI(ReportFileCountUI, fileCount);
            }

            void IImportProgressReceiver.ReportFileStarted(string fileName)
            {
                InvokeUI(ReportFileStartedUI, fileName);
            }

            void IImportProgressReceiver.ReportFileFinished()
            {
                InvokeUI(ReportFileFinishedUI);
            }

            void IImportProgressReceiver.ReportAllFinished()
            {
                InvokeUI(ReportAllFinishedUI);
            }

            void IImportProgressReceiver.ReportFailure(string errorMessage)
            {
                InvokeUI(ReportFailureUI, errorMessage);
            }

            #endregion IImportProgressReceiver implementation

            private void InvokeUI(Action action)
            {
                _form.BeginInvoke(action);
            }

            private void InvokeUI<TArg>(Action<TArg> action, TArg arg)
            {
                _form.BeginInvoke(action, arg);
            }

            private void ReportStartedUI()
            {
                _statusLabel.Text = Properties.Resources.Starting;
                _progress.Start();
            }

            private void ReportFileCountUI(int fileCount)
            {
                _progress.InitializeTotalFileCount(fileCount);
            }

            private void ReportFileStartedUI(string fileName)
            {
                _statusLabel.Text = Format(
                    Properties.Resources.Importing,
                    fileName,
                    _progress.CurrentFileOrdinal,
                    _progress.TotalFileCount);
            }

            private void ReportFileFinishedUI()
            {
                _progress.FinishFile();
            }

            private void ReportAllFinishedUI()
            {
                _statusLabel.Text = GetAllFinishedStatusText(_progress.TotalFileCount);
            }

            private static string GetAllFinishedStatusText(int photoCount)
            {
                switch (photoCount)
                {
                    case 0: return Properties.Resources.NoPhotosImported;
                    case 1: return Properties.Resources.OnePhotoImported;
                    default: return Format(Properties.Resources.MultiplePhotosImported, photoCount);
                }
            }

            private void ReportFailureUI(string errorMessage)
            {
                _statusLabel.Text = errorMessage;
                _progress.Hide();
            }
        }
    }
}