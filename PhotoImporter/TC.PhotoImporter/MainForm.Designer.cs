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
            System.Windows.Forms.TableLayoutPanel layout;
            System.Windows.Forms.LinkLabel websiteLink;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusLabel = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.sourceIcon = new System.Windows.Forms.PictureBox();
            this.sourceLink = new System.Windows.Forms.LinkLabel();
            this.destinationIcon = new System.Windows.Forms.PictureBox();
            this.destinationLink = new System.Windows.Forms.LinkLabel();
            layout = new System.Windows.Forms.TableLayoutPanel();
            websiteLink = new System.Windows.Forms.LinkLabel();
            layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            layout.AutoSize = true;
            layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.Controls.Add(this.statusLabel, 0, 0);
            layout.Controls.Add(this.progress, 0, 1);
            layout.Controls.Add(this.sourceIcon, 0, 2);
            layout.Controls.Add(this.sourceLink, 1, 2);
            layout.Controls.Add(this.destinationIcon, 0, 3);
            layout.Controls.Add(this.destinationLink, 1, 3);
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
            this.statusLabel.AutoSize = true;
            layout.SetColumnSpan(this.statusLabel, 2);
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Location = new System.Drawing.Point(9, 9);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(6);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(427, 17);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "(Status)";
            // 
            // progress
            // 
            layout.SetColumnSpan(this.progress, 2);
            this.progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progress.Location = new System.Drawing.Point(9, 38);
            this.progress.Margin = new System.Windows.Forms.Padding(6);
            this.progress.MaximumSize = new System.Drawing.Size(0, 20);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(427, 20);
            this.progress.Step = 1;
            this.progress.TabIndex = 1;
            this.progress.Visible = false;
            // 
            // sourceIcon
            // 
            this.sourceIcon.Image = global::TC.PhotoImporter.Properties.Resources.Camera_32xLG;
            this.sourceIcon.Location = new System.Drawing.Point(6, 64);
            this.sourceIcon.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.sourceIcon.Name = "sourceIcon";
            this.sourceIcon.Size = new System.Drawing.Size(32, 32);
            this.sourceIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.sourceIcon.TabIndex = 5;
            this.sourceIcon.TabStop = false;
            // 
            // sourceLink
            // 
            this.sourceLink.AutoEllipsis = true;
            this.sourceLink.AutoSize = true;
            this.sourceLink.DisabledLinkColor = System.Drawing.SystemColors.GrayText;
            this.sourceLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.sourceLink.Location = new System.Drawing.Point(45, 68);
            this.sourceLink.Margin = new System.Windows.Forms.Padding(4);
            this.sourceLink.Name = "sourceLink";
            this.sourceLink.Size = new System.Drawing.Size(393, 24);
            this.sourceLink.TabIndex = 2;
            this.sourceLink.TabStop = true;
            this.sourceLink.Text = "(source)";
            this.sourceLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sourceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnFolderLinkClicked);
            // 
            // destinationIcon
            // 
            this.destinationIcon.Image = global::TC.PhotoImporter.Properties.Resources.Folder_32x;
            this.destinationIcon.Location = new System.Drawing.Point(6, 96);
            this.destinationIcon.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.destinationIcon.Name = "destinationIcon";
            this.destinationIcon.Size = new System.Drawing.Size(32, 32);
            this.destinationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.destinationIcon.TabIndex = 6;
            this.destinationIcon.TabStop = false;
            // 
            // destinationLink
            // 
            this.destinationLink.AutoEllipsis = true;
            this.destinationLink.AutoSize = true;
            this.destinationLink.DisabledLinkColor = System.Drawing.SystemColors.GrayText;
            this.destinationLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destinationLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.destinationLink.Location = new System.Drawing.Point(45, 100);
            this.destinationLink.Margin = new System.Windows.Forms.Padding(4);
            this.destinationLink.Name = "destinationLink";
            this.destinationLink.Size = new System.Drawing.Size(393, 24);
            this.destinationLink.TabIndex = 3;
            this.destinationLink.TabStop = true;
            this.destinationLink.Text = "(destination)";
            this.destinationLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.destinationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnFolderLinkClicked);
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
            ((System.ComponentModel.ISupportInitialize)(this.sourceIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.LinkLabel destinationLink;
        private System.Windows.Forms.LinkLabel sourceLink;
        private System.Windows.Forms.PictureBox sourceIcon;
        private System.Windows.Forms.PictureBox destinationIcon;
    }
}

