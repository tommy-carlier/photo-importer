using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TC.PhotoImporter
{
    using static FormattableString;

    public partial class MainForm : Form
    {
        private Importer _importer;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var settings = Settings.Instance;
            string error = settings.Validate();
            if (string.IsNullOrEmpty(error))
            {
                var progressReceiver = new ProgressReceiver(this);
                if (Directory.Exists(settings.SourceFolderPath))
                {
                    _importer = new Importer(settings, progressReceiver);
                    _importer.Start();
                }
                else
                {
                    progressReceiver.ReportFailure(Invariant($"Source folder “{settings.SourceFolderPath}” does not exist"));
                }
            }
            else
            {
                statusLabel.Text = error;
            }

            SetFolderLinkContent(sourceIcon, sourceLink, settings.SourceFolderPath);
            SetFolderLinkContent(destinationIcon, destinationLink, settings.DestinationFolderPath);
        }

        private static void SetFolderLinkContent(PictureBox icon, LinkLabel link, string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                icon.Visible = false;
                link.Visible = false;
            }
            else
            {
                icon.Visible = true;
                link.Visible = true;
                link.Text = folderPath;
            }
        }

        private void OnFolderLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start((sender as LinkLabel).Text);
        }

        private void OnWebsiteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.tcx.be/");
        }
    }
}
