namespace TC.PhotoImporter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TableLayoutPanel layout;
            System.Windows.Forms.LinkLabel websiteLink;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._statusLabel = new System.Windows.Forms.Label();
            this._progress = new System.Windows.Forms.ProgressBar();
            this._sourceIcon = new System.Windows.Forms.PictureBox();
            this._sourceLink = new System.Windows.Forms.LinkLabel();
            this._destinationIcon = new System.Windows.Forms.PictureBox();
            this._destinationLink = new System.Windows.Forms.LinkLabel();
            this._timerToCheckFoldersExist = new System.Windows.Forms.Timer(this.components);
            layout = new System.Windows.Forms.TableLayoutPanel();
            websiteLink = new System.Windows.Forms.LinkLabel();
            layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._sourceIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._destinationIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            layout.AutoSize = true;
            layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.Controls.Add(this._statusLabel, 0, 0);
            layout.Controls.Add(this._progress, 0, 1);
            layout.Controls.Add(this._sourceIcon, 0, 2);
            layout.Controls.Add(this._sourceLink, 1, 2);
            layout.Controls.Add(this._destinationIcon, 0, 3);
            layout.Controls.Add(this._destinationLink, 1, 3);
            layout.Controls.Add(websiteLink, 1, 4);
            layout.Dock = System.Windows.Forms.DockStyle.Fill;
            layout.Location = new System.Drawing.Point(0, 0);
            layout.MinimumSize = new System.Drawing.Size(300, 0);
            layout.Name = "layout";
            layout.Padding = new System.Windows.Forms.Padding(3);
            layout.RowCount = 5;
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.Size = new System.Drawing.Size(445, 167);
            layout.TabIndex = 0;
            // 
            // statusLabel
            // 
            this._statusLabel.AutoSize = true;
            layout.SetColumnSpan(this._statusLabel, 2);
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Location = new System.Drawing.Point(9, 9);
            this._statusLabel.Margin = new System.Windows.Forms.Padding(6);
            this._statusLabel.Name = "statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(427, 17);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.Text = "(Status)";
            // 
            // progress
            // 
            layout.SetColumnSpan(this._progress, 2);
            this._progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this._progress.Location = new System.Drawing.Point(9, 38);
            this._progress.Margin = new System.Windows.Forms.Padding(6);
            this._progress.MaximumSize = new System.Drawing.Size(0, 20);
            this._progress.Name = "progress";
            this._progress.Size = new System.Drawing.Size(427, 20);
            this._progress.Step = 1;
            this._progress.TabIndex = 1;
            this._progress.Visible = false;
            // 
            // sourceIcon
            // 
            this._sourceIcon.Image = global::TC.PhotoImporter.Properties.Resources.Camera_32xLG;
            this._sourceIcon.Location = new System.Drawing.Point(6, 64);
            this._sourceIcon.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this._sourceIcon.Name = "sourceIcon";
            this._sourceIcon.Size = new System.Drawing.Size(32, 32);
            this._sourceIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._sourceIcon.TabIndex = 5;
            this._sourceIcon.TabStop = false;
            // 
            // sourceLink
            // 
            this._sourceLink.AutoEllipsis = true;
            this._sourceLink.AutoSize = true;
            this._sourceLink.DisabledLinkColor = System.Drawing.SystemColors.GrayText;
            this._sourceLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sourceLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._sourceLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._sourceLink.Location = new System.Drawing.Point(45, 68);
            this._sourceLink.Margin = new System.Windows.Forms.Padding(4);
            this._sourceLink.Name = "sourceLink";
            this._sourceLink.Size = new System.Drawing.Size(393, 24);
            this._sourceLink.TabIndex = 2;
            this._sourceLink.TabStop = true;
            this._sourceLink.Text = "(source)";
            this._sourceLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._sourceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnFolderLinkClicked);
            // 
            // destinationIcon
            // 
            this._destinationIcon.Image = global::TC.PhotoImporter.Properties.Resources.Folder_32x;
            this._destinationIcon.Location = new System.Drawing.Point(6, 96);
            this._destinationIcon.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this._destinationIcon.Name = "destinationIcon";
            this._destinationIcon.Size = new System.Drawing.Size(32, 32);
            this._destinationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._destinationIcon.TabIndex = 6;
            this._destinationIcon.TabStop = false;
            // 
            // destinationLink
            // 
            this._destinationLink.AutoEllipsis = true;
            this._destinationLink.AutoSize = true;
            this._destinationLink.DisabledLinkColor = System.Drawing.SystemColors.GrayText;
            this._destinationLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this._destinationLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._destinationLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._destinationLink.Location = new System.Drawing.Point(45, 100);
            this._destinationLink.Margin = new System.Windows.Forms.Padding(4);
            this._destinationLink.Name = "destinationLink";
            this._destinationLink.Size = new System.Drawing.Size(393, 24);
            this._destinationLink.TabIndex = 3;
            this._destinationLink.TabStop = true;
            this._destinationLink.Text = "(destination)";
            this._destinationLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._destinationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnFolderLinkClicked);
            // 
            // websiteLink
            // 
            websiteLink.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            websiteLink.AutoSize = true;
            websiteLink.DisabledLinkColor = System.Drawing.SystemColors.GrayText;
            websiteLink.Dock = System.Windows.Forms.DockStyle.Fill;
            websiteLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            websiteLink.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            websiteLink.LinkColor = System.Drawing.SystemColors.GrayText;
            websiteLink.Location = new System.Drawing.Point(45, 132);
            websiteLink.Margin = new System.Windows.Forms.Padding(4);
            websiteLink.Name = "websiteLink";
            websiteLink.Size = new System.Drawing.Size(393, 28);
            websiteLink.TabIndex = 4;
            websiteLink.TabStop = true;
            websiteLink.Text = "www.tcx.be";
            websiteLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            websiteLink.VisitedLinkColor = System.Drawing.SystemColors.GrayText;
            websiteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnWebsiteLinkClicked);
            // 
            // timerToCheckFoldersExist
            // 
            this._timerToCheckFoldersExist.Interval = 3000;
            this._timerToCheckFoldersExist.Tick += new System.EventHandler(this.OnTimerToCheckFoldersExistTick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(445, 167);
            this.Controls.Add(layout);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Photo Importer";
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._sourceIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._destinationIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.ProgressBar _progress;
        private System.Windows.Forms.PictureBox _sourceIcon;
        private System.Windows.Forms.LinkLabel _sourceLink;
        private System.Windows.Forms.PictureBox _destinationIcon;
        private System.Windows.Forms.LinkLabel _destinationLink;
        private System.Windows.Forms.Timer _timerToCheckFoldersExist;
    }
}

