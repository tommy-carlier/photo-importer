using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using static TC.PhotoImporter.Localization;

namespace TC.PhotoImporter
{
    public partial class MainForm : Form
    {
        private readonly IImportProgressReporter _progressReporter;
        private readonly FormAutoCenterer _autoCenterer;

        private Settings _settings;
        private Importer _importer;

        public MainForm()
        {
            InitializeComponent();
            Text = Properties.Resources.ImportPhotos + GetVersionTitleSuffix();
            _progressReporter = new ProgressReporter(this);
            _autoCenterer = new FormAutoCenterer(this);
        }

        private static string GetVersionTitleSuffix()
        {
            try
            {
                var version = new Version(Application.ProductVersion);
                return Format(
                    Properties.Resources.VersionTitleSuffix,
                    version.ToString(fieldCount: version.Build == 0 ? 2 : 3));
            }
            catch (ArgumentException) { return ""; }
            catch (FormatException) { return ""; }
            catch (OverflowException) { return ""; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _settings = Settings.ReadFromFile("TC.PhotoImporter.ini");

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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _autoCenterer.Dispose();
        }

        private void StartImporting()
        {
            var errors = new List<string>(2);
            if (!Directory.Exists(_settings.SourceFolderPath))
            {
                errors.Add(Format(
                    Properties.Resources.SourceFolderDoesNotExist,
                    _settings.SourceFolderPath));
            }
            if (!Directory.Exists(_settings.DestinationFolderPath))
            {
                errors.Add(Format(
                    Properties.Resources.DestinationFolderDoesNotExist,
                    _settings.DestinationFolderPath));
            }

            if (errors.Count == 0)
            {
                _timerToCheckFoldersExist.Stop();
                _importer = new Importer(_settings, _progressReporter);
                _importer.Start();
            }
            else
            {
                _progressReporter.ReportFailure(string.Join(Environment.NewLine, errors));
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
            StartProcess("https://www.tcx.be/");
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
