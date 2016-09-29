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
            System.Windows.Forms.LinkLabel link;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusLabel = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            layout = new System.Windows.Forms.TableLayoutPanel();
            link = new System.Windows.Forms.LinkLabel();
            layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Location = new System.Drawing.Point(6, 6);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(6);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(433, 17);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "(Status)";
            // 
            // progress
            // 
            this.progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progress.Location = new System.Drawing.Point(6, 35);
            this.progress.Margin = new System.Windows.Forms.Padding(6);
            this.progress.MaximumSize = new System.Drawing.Size(0, 20);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(433, 20);
            this.progress.Step = 1;
            this.progress.TabIndex = 1;
            // 
            // layout
            // 
            layout.AutoSize = true;
            layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.Controls.Add(link, 0, 2);
            layout.Controls.Add(this.statusLabel, 0, 0);
            layout.Controls.Add(this.progress, 0, 1);
            layout.Dock = System.Windows.Forms.DockStyle.Fill;
            layout.Location = new System.Drawing.Point(0, 0);
            layout.MinimumSize = new System.Drawing.Size(300, 0);
            layout.Name = "layout";
            layout.RowCount = 3;
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.Size = new System.Drawing.Size(445, 156);
            layout.TabIndex = 0;
            // 
            // link
            // 
            link.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            link.AutoSize = true;
            link.DisabledLinkColor = System.Drawing.SystemColors.GrayText;
            link.Dock = System.Windows.Forms.DockStyle.Fill;
            link.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            link.LinkColor = System.Drawing.SystemColors.GrayText;
            link.Location = new System.Drawing.Point(4, 65);
            link.Margin = new System.Windows.Forms.Padding(4);
            link.Name = "link";
            link.Size = new System.Drawing.Size(437, 87);
            link.TabIndex = 2;
            link.TabStop = true;
            link.Text = "www.tcx.be";
            link.TextAlign = System.Drawing.ContentAlignment.TopRight;
            link.VisitedLinkColor = System.Drawing.SystemColors.GrayText;
            link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(445, 156);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ProgressBar progress;
    }
}

