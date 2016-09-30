﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace TC.PhotoImporter
{
    using static FormattableString;

    partial class MainForm
    {
        private sealed class ProgressReceiver : IImportProgressReceiver
        {
            readonly MainForm _form;
            readonly Label _statusLabel;
            readonly ProgressBar _progress;

            int _totalFileCount, _finishedFileCount;
            string _currentFileName, _errorMessage;

            internal ProgressReceiver(MainForm form)
            {
                _form = form;
                _statusLabel = form.statusLabel;
                _progress = form.progress;
            }

            #region IImportProgressReceiver implementation

            public void ReportStarted()
            {
                InvokeUI(ReportStartedUI);
            }

            public void ReportFileCount(int fileCount)
            {
                _totalFileCount = fileCount;
                InvokeUI(ReportFileCountUI);
            }

            public void ReportFileStarted(string fileName)
            {
                _currentFileName = fileName;
                InvokeUI(ReportFileStartedUI);
            }

            public void ReportFileFinished()
            {
                Interlocked.Increment(ref _finishedFileCount);
                InvokeUI(ReportFileFinishedUI);
            }

            public void ReportAllFinished()
            {
                InvokeUI(ReportAllFinishedUI);
            }

            public void ReportFailure(string errorMessage)
            {
                _errorMessage = errorMessage;
                InvokeUI(ReportFailureUI);
            }

            #endregion IImportProgressReceiver implementation

            private void InvokeUI(Action action)
            {
                _form.BeginInvoke(action);
            }

            private void ReportStartedUI()
            {
                _statusLabel.Text = "Starting…";
                _progress.Visible = true;
                _progress.Style = ProgressBarStyle.Marquee;
            }

            private void ReportFileCountUI()
            {
                _progress.Style = ProgressBarStyle.Continuous;
                _progress.Maximum = _totalFileCount;
                _progress.Value = _finishedFileCount;
            }

            private void ReportFileStartedUI()
            {
                _statusLabel.Text = Invariant($"Importing {_currentFileName} ({_finishedFileCount + 1}/{_totalFileCount})…");
            }

            private void ReportFileFinishedUI()
            {
                _progress.PerformStep();
            }

            private void ReportAllFinishedUI()
            {
                _statusLabel.Text = GetAllFinishedStatusText(_totalFileCount);
            }

            private static string GetAllFinishedStatusText(int photoCount)
            {
                switch (photoCount)
                {
                    case 0: return "No photos were imported.";
                    case 1: return "Finished importing 1 photo.";
                    default: return Invariant($"Finished importing {photoCount} photos.");
                }
            }

            private void ReportFailureUI()
            {
                _statusLabel.Text = _errorMessage;
                _progress.Visible = false;
            }
        }
    }
}