using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace TC.PhotoImporter
{
    public partial class MainForm : Form
    {
        private readonly Settings _settings = Settings.Instance;
        private readonly ProgressReceiver _progressReceiver;
        private Importer _importer;

        public MainForm()
        {
            InitializeComponent();
            Text = Properties.Resources.ImportPhotos;
            _progressReceiver = new ProgressReceiver(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string error = _settings.Validate();
            if (string.IsNullOrEmpty(error))
            {
                StartImporting();
            }
            else
            {
                _statusLabel.Text = error;
            }

            SetFolderLinkContent(_sourceIcon, _sourceLink, _settings.SourceFolderPath);
            SetFolderLinkContent(_destinationIcon, _destinationLink, _settings.DestinationFolderPath);
        }

        private void StartImporting()
        {
            var errors = new List<string>(2);
            if (!Directory.Exists(_settings.SourceFolderPath))
            {
                errors.Add(string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Resources.SourceFolderDoesNotExist,
                    _settings.SourceFolderPath));
            }
            if (!Directory.Exists(_settings.DestinationFolderPath))
            {
                errors.Add(string.Format(
                    CultureInfo.InvariantCulture,
                    Properties.Resources.DestinationFolderDoesNotExist,
                    _settings.DestinationFolderPath));
            }

            if (errors.Count == 0)
            {
                _timerToCheckFoldersExist.Stop();
                _importer = new Importer(_settings, _progressReceiver);
                _importer.Start();
            }
            else
            {
                _progressReceiver.ReportFailure(string.Join(Environment.NewLine, errors));
                _timerToCheckFoldersExist.Start();
            }
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
            StartProcess((sender as LinkLabel).Text);
        }

        private void OnWebsiteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartProcess("http://www.tcx.be/");
        }

        private void OnTimerToCheckFoldersExistTick(object sender, EventArgs e)
        {
            StartImporting();
        }

        private void StartProcess(string fileName)
        {
            try
            {
                Process.Start(fileName);
            }
            catch(Win32Exception ex)
            {
                ShowErrorDialog(ex.Message);
            }
            catch(FileNotFoundException ex)
            {
                ShowErrorDialog(ex.Message);
            }
        }

        private void ShowErrorDialog(string message)
        {
            MessageBox.Show(this, message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
