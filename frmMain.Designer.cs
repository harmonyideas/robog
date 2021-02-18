/*
Copyright (C) 2014 

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.






*/

namespace RoboG
{
    partial class frmMain : BackupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.DotTimer = new System.Windows.Forms.Timer(this.components);
			this.CloseTimer = new System.Windows.Forms.Timer(this.components);
			this.lblTargetDir = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.bnClose = new System.Windows.Forms.Button();
			this.bnErrors = new System.Windows.Forms.Button();
			this.lblSizeCaption = new System.Windows.Forms.Label();
			this.lblFileCaption = new System.Windows.Forms.Label();
			this.lblDirectoryCaption = new System.Windows.Forms.Label();
			this.lblStatusCaption = new System.Windows.Forms.Label();
			this.bnDetails = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.OverallProgress = new System.Windows.Forms.ProgressBar();
			this.lblSize = new System.Windows.Forms.Label();
			this.lblFile = new System.Windows.Forms.Label();
			this.lblDirectory = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.frmMainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.lblStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblStripStatus2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblTransferRate = new System.Windows.Forms.ToolStripStatusLabel();
			this.frmMainStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// DotTimer
			// 
			this.DotTimer.Enabled = true;
			this.DotTimer.Interval = 800;
			this.DotTimer.Tick += new System.EventHandler(this.DotTimer_Tick);
			// 
			// CloseTimer
			// 
			this.CloseTimer.Interval = 1000;
			this.CloseTimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
			// 
			// lblTargetDir
			// 
			this.lblTargetDir.Location = new System.Drawing.Point(92, 53);
			this.lblTargetDir.Name = "lblTargetDir";
			this.lblTargetDir.Size = new System.Drawing.Size(849, 13);
			this.lblTargetDir.TabIndex = 17;
			this.lblTargetDir.Text = "Waiting for sources...";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Target Path:";
			// 
			// bnClose
			// 
			this.bnClose.Location = new System.Drawing.Point(831, 197);
			this.bnClose.Name = "bnClose";
			this.bnClose.Size = new System.Drawing.Size(110, 30);
			this.bnClose.TabIndex = 14;
			this.bnClose.Text = "Close";
			this.bnClose.UseVisualStyleBackColor = true;
			this.bnClose.Visible = false;
			this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
			// 
			// bnErrors
			// 
			this.bnErrors.Location = new System.Drawing.Point(715, 197);
			this.bnErrors.Name = "bnErrors";
			this.bnErrors.Size = new System.Drawing.Size(110, 30);
			this.bnErrors.TabIndex = 13;
			this.bnErrors.Text = "Show errors";
			this.bnErrors.UseVisualStyleBackColor = true;
			this.bnErrors.Visible = false;
			this.bnErrors.Click += new System.EventHandler(this.bnErrors_Click);
			// 
			// lblSizeCaption
			// 
			this.lblSizeCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSizeCaption.AutoSize = true;
			this.lblSizeCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSizeCaption.Location = new System.Drawing.Point(806, 73);
			this.lblSizeCaption.Name = "lblSizeCaption";
			this.lblSizeCaption.Size = new System.Drawing.Size(35, 13);
			this.lblSizeCaption.TabIndex = 12;
			this.lblSizeCaption.Text = "Size:";
			// 
			// lblFileCaption
			// 
			this.lblFileCaption.AutoSize = true;
			this.lblFileCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFileCaption.Location = new System.Drawing.Point(12, 73);
			this.lblFileCaption.Name = "lblFileCaption";
			this.lblFileCaption.Size = new System.Drawing.Size(31, 13);
			this.lblFileCaption.TabIndex = 11;
			this.lblFileCaption.Text = "File:";
			// 
			// lblDirectoryCaption
			// 
			this.lblDirectoryCaption.AutoSize = true;
			this.lblDirectoryCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDirectoryCaption.Location = new System.Drawing.Point(12, 30);
			this.lblDirectoryCaption.Name = "lblDirectoryCaption";
			this.lblDirectoryCaption.Size = new System.Drawing.Size(71, 13);
			this.lblDirectoryCaption.TabIndex = 10;
			this.lblDirectoryCaption.Text = "Source Dir:";
			// 
			// lblStatusCaption
			// 
			this.lblStatusCaption.AutoSize = true;
			this.lblStatusCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatusCaption.Location = new System.Drawing.Point(12, 9);
			this.lblStatusCaption.Name = "lblStatusCaption";
			this.lblStatusCaption.Size = new System.Drawing.Size(47, 13);
			this.lblStatusCaption.TabIndex = 9;
			this.lblStatusCaption.Text = "Status:";
			// 
			// bnDetails
			// 
			this.bnDetails.Location = new System.Drawing.Point(15, 197);
			this.bnDetails.Name = "bnDetails";
			this.bnDetails.Size = new System.Drawing.Size(110, 30);
			this.bnDetails.TabIndex = 7;
			this.bnDetails.Text = "Show details";
			this.bnDetails.UseVisualStyleBackColor = true;
			this.bnDetails.Click += new System.EventHandler(this.bnDetails_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.Location = new System.Drawing.Point(92, 9);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(849, 21);
			this.lblStatus.TabIndex = 6;
			// 
			// OverallProgress
			// 
			this.OverallProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.OverallProgress.Location = new System.Drawing.Point(15, 143);
			this.OverallProgress.Maximum = 1000;
			this.OverallProgress.Name = "OverallProgress";
			this.OverallProgress.Size = new System.Drawing.Size(926, 26);
			this.OverallProgress.TabIndex = 5;
			this.OverallProgress.Visible = false;
			// 
			// lblSize
			// 
			this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSize.Location = new System.Drawing.Point(847, 73);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(94, 13);
			this.lblSize.TabIndex = 4;
			// 
			// lblFile
			// 
			this.lblFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblFile.Location = new System.Drawing.Point(92, 73);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(849, 13);
			this.lblFile.TabIndex = 3;
			// 
			// lblDirectory
			// 
			this.lblDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblDirectory.Location = new System.Drawing.Point(92, 30);
			this.lblDirectory.Name = "lblDirectory";
			this.lblDirectory.Size = new System.Drawing.Size(849, 21);
			this.lblDirectory.TabIndex = 2;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ProgressBar.Location = new System.Drawing.Point(12, 111);
			this.ProgressBar.Maximum = 1000;
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(929, 26);
			this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.ProgressBar.TabIndex = 1;
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.BackColor = System.Drawing.Color.White;
			this.txtLog.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLog.Location = new System.Drawing.Point(6, 218);
			this.txtLog.MaxLength = 427670000;
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(938, 0);
			this.txtLog.TabIndex = 0;
			this.txtLog.Visible = false;
			this.txtLog.WordWrap = false;
			// 
			// frmMainStatusStrip
			// 
			this.frmMainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStripStatus,
            this.lblStripStatus2,
            this.lblTransferRate});
			this.frmMainStatusStrip.Location = new System.Drawing.Point(0, 240);
			this.frmMainStatusStrip.Name = "frmMainStatusStrip";
			this.frmMainStatusStrip.Size = new System.Drawing.Size(950, 22);
			this.frmMainStatusStrip.TabIndex = 15;
			this.frmMainStatusStrip.Text = "statusStrip1";
			// 
			// lblStripStatus
			// 
			this.lblStripStatus.AutoToolTip = true;
			this.lblStripStatus.Name = "lblStripStatus";
			this.lblStripStatus.Size = new System.Drawing.Size(311, 17);
			this.lblStripStatus.Spring = true;
			this.lblStripStatus.Text = "Current Status:";
			this.lblStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblStripStatus2
			// 
			this.lblStripStatus2.AutoSize = false;
			this.lblStripStatus2.Name = "lblStripStatus2";
			this.lblStripStatus2.Size = new System.Drawing.Size(311, 17);
			this.lblStripStatus2.Spring = true;
			// 
			// lblTransferRate
			// 
			this.lblTransferRate.Name = "lblTransferRate";
			this.lblTransferRate.Size = new System.Drawing.Size(311, 17);
			this.lblTransferRate.Spring = true;
			this.lblTransferRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(950, 262);
			this.Controls.Add(this.lblTargetDir);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bnClose);
			this.Controls.Add(this.bnErrors);
			this.Controls.Add(this.lblSizeCaption);
			this.Controls.Add(this.lblFileCaption);
			this.Controls.Add(this.lblDirectoryCaption);
			this.Controls.Add(this.lblStatusCaption);
			this.Controls.Add(this.bnDetails);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.OverallProgress);
			this.Controls.Add(this.lblSize);
			this.Controls.Add(this.lblFile);
			this.Controls.Add(this.lblDirectory);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.frmMainStatusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(966, 300);
			this.MinimumSize = new System.Drawing.Size(500, 300);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RoboG";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHaupt_FormClosing);
			this.Load += new System.EventHandler(this.frmHaupt_Load);
			this.Shown += new System.EventHandler(this.frmMain_Shown);
			this.frmMainStatusStrip.ResumeLayout(false);
			this.frmMainStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label lblDirectory;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ProgressBar OverallProgress;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button bnDetails;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblDirectoryCaption;
        private System.Windows.Forms.Label lblFileCaption;
        private System.Windows.Forms.Label lblSizeCaption;
        private System.Windows.Forms.Timer DotTimer;
        private System.Windows.Forms.Timer CloseTimer;
        private System.Windows.Forms.Button bnErrors;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.StatusStrip frmMainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStripStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStripStatus2;
        private System.Windows.Forms.ToolStripStatusLabel lblTransferRate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblTargetDir;
		
		
    }
}

