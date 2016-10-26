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
            private readonly ProgressBar _progress;

            internal ProgressReceiver(MainForm form)
            {
                _form = form;
                _statusLabel = form._statusLabel;
                _progress = form._progress;
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

            #region TotalFileCount, FinishedFileCount

            private int TotalFileCount
            {
                get { return _progress.Maximum; }
                set { _progress.Maximum = value; }
            }

            private int FinishedFileCount
            {
                get { return _progress.Value; }
            }

            private void ResetFinishedFileCount()
            {
                _progress.Value = 0;
            }

            private void IncrementFinishedFileCount()
            {
                _progress.PerformStep();
            }

            #endregion TotalFileCount, FinishedFileCount

            #region ProgressStyle

            private enum ProgressStyle
            {
                Hidden = 0,
                UnknownFileCount,
                KnownFileCount,
            }

            private void ChangeProgressStyle(ProgressStyle style)
            {
                _progress.Visible = style != ProgressStyle.Hidden;
                _progress.Style =
                    style == ProgressStyle.KnownFileCount
                        ? ProgressBarStyle.Continuous 
                        : ProgressBarStyle.Marquee;
            }

            #endregion ProgressStyle

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
                ChangeProgressStyle(ProgressStyle.UnknownFileCount);
            }

            private void ReportFileCountUI(int fileCount)
            {
                ResetFinishedFileCount();
                TotalFileCount = fileCount;
                ChangeProgressStyle(ProgressStyle.KnownFileCount);
            }

            private void ReportFileStartedUI(string fileName)
            {
                _statusLabel.Text = Format(
                    Properties.Resources.Importing,
                    fileName,
                    FinishedFileCount + 1,
                    TotalFileCount);
            }

            private void ReportFileFinishedUI()
            {
                IncrementFinishedFileCount();
            }

            private void ReportAllFinishedUI()
            {
                _statusLabel.Text = GetAllFinishedStatusText(TotalFileCount);
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
                ChangeProgressStyle(ProgressStyle.Hidden);
            }
        }
    }
}