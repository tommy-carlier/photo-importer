using System.Windows.Forms;

namespace TC.PhotoImporter
{
    internal sealed class FileProgressTracker
    {
        private readonly ProgressBar _bar;

        internal FileProgressTracker(ProgressBar bar)
        {
            _bar = bar;
        }

        internal void Start()
        {
            ChangeStyle(Style.UnknownFileCount);
        }

        internal void Hide()
        {
            ChangeStyle(Style.Hidden);
        }

        internal void InitializeTotalFileCount(int fileCount)
        {
            FinishedFileCount = 0;
            TotalFileCount = fileCount;
            ChangeStyle(Style.KnownFileCount);
        }

        internal void FinishFile()
        {
            _bar.PerformStep();
        }

        internal int TotalFileCount
        {
            get { return _bar.Maximum; }
            private set { _bar.Maximum = value; }
        }

        internal int FinishedFileCount
        {
            get { return _bar.Value; }
            private set { _bar.Value = value; }
        }

        internal int CurrentFileOrdinal
        {
            get { return FinishedFileCount + 1; }
        }

        private void ChangeStyle(Style style)
        {
            _bar.Visible = style != Style.Hidden;
            _bar.Style =
                style == Style.KnownFileCount
                    ? ProgressBarStyle.Continuous
                    : ProgressBarStyle.Marquee;
        }

        private enum Style
        {
            Hidden = 0,
            UnknownFileCount,
            KnownFileCount,
        }
    }
}
