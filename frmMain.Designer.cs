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
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblProgressText = new System.Windows.Forms.Label();
			this.lblDirectoryCaption = new System.Windows.Forms.Label();
			this.lblDirectory = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblTargetDir = new System.Windows.Forms.Label();
			this.lblFileCaption = new System.Windows.Forms.Label();
			this.lblFile = new System.Windows.Forms.Label();
			this.lblSizeCaption = new System.Windows.Forms.Label();
			this.lblSize = new System.Windows.Forms.Label();
			this.lblFilesCaption = new System.Windows.Forms.Label();
			this.lblSpeedCaption = new System.Windows.Forms.Label();
			this.lblElapsedCaption = new System.Windows.Forms.Label();
			this.lblRemainingCaption = new System.Windows.Forms.Label();
			this.lblErrorsCaption = new System.Windows.Forms.Label();
			this.lblFilesValue = new System.Windows.Forms.Label();
			this.lblSpeedValue = new System.Windows.Forms.Label();
			this.lblElapsedValue = new System.Windows.Forms.Label();
			this.lblRemainingValue = new System.Windows.Forms.Label();
			this.lblErrorsValue = new System.Windows.Forms.Label();
			this.OverallProgress = new System.Windows.Forms.ProgressBar();
			this.pnlSeparator = new System.Windows.Forms.Panel();
			this.bnDetails = new System.Windows.Forms.Button();
			this.bnErrors = new System.Windows.Forms.Button();
			this.bnClose = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.frmMainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.lblStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
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
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatus.AutoEllipsis = true;
			this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblStatus.Location = new System.Drawing.Point(18, 14);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(724, 28);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "Starting...";
			// 
			// lblProgressText
			// 
			this.lblProgressText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgressText.AutoEllipsis = true;
			this.lblProgressText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProgressText.Location = new System.Drawing.Point(20, 74);
			this.lblProgressText.Name = "lblProgressText";
			this.lblProgressText.Size = new System.Drawing.Size(720, 18);
			this.lblProgressText.TabIndex = 2;
			// 
			// lblFilesCaption
			// 
			this.lblFilesCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFilesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblFilesCaption.Location = new System.Drawing.Point(20, 104);
			this.lblFilesCaption.Name = "lblFilesCaption";
			this.lblFilesCaption.Size = new System.Drawing.Size(136, 17);
			this.lblFilesCaption.TabIndex = 3;
			this.lblFilesCaption.Text = "Files";
			// 
			// lblFilesValue
			// 
			this.lblFilesValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFilesValue.AutoEllipsis = true;
			this.lblFilesValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFilesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblFilesValue.Location = new System.Drawing.Point(20, 121);
			this.lblFilesValue.Name = "lblFilesValue";
			this.lblFilesValue.Size = new System.Drawing.Size(136, 24);
			this.lblFilesValue.TabIndex = 4;
			this.lblFilesValue.Text = "0";
			// 
			// lblSpeedCaption
			// 
			this.lblSpeedCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblSpeedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblSpeedCaption.Location = new System.Drawing.Point(166, 104);
			this.lblSpeedCaption.Name = "lblSpeedCaption";
			this.lblSpeedCaption.Size = new System.Drawing.Size(136, 17);
			this.lblSpeedCaption.TabIndex = 5;
			this.lblSpeedCaption.Text = "Speed";
			// 
			// lblSpeedValue
			// 
			this.lblSpeedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblSpeedValue.AutoEllipsis = true;
			this.lblSpeedValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblSpeedValue.Location = new System.Drawing.Point(166, 121);
			this.lblSpeedValue.Name = "lblSpeedValue";
			this.lblSpeedValue.Size = new System.Drawing.Size(136, 24);
			this.lblSpeedValue.TabIndex = 6;
			this.lblSpeedValue.Text = "-";
			// 
			// lblElapsedCaption
			// 
			this.lblElapsedCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblElapsedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblElapsedCaption.Location = new System.Drawing.Point(312, 104);
			this.lblElapsedCaption.Name = "lblElapsedCaption";
			this.lblElapsedCaption.Size = new System.Drawing.Size(136, 17);
			this.lblElapsedCaption.TabIndex = 7;
			this.lblElapsedCaption.Text = "Elapsed";
			// 
			// lblElapsedValue
			// 
			this.lblElapsedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblElapsedValue.AutoEllipsis = true;
			this.lblElapsedValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblElapsedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblElapsedValue.Location = new System.Drawing.Point(312, 121);
			this.lblElapsedValue.Name = "lblElapsedValue";
			this.lblElapsedValue.Size = new System.Drawing.Size(136, 24);
			this.lblElapsedValue.TabIndex = 8;
			this.lblElapsedValue.Text = "-";
			// 
			// lblRemainingCaption
			// 
			this.lblRemainingCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRemainingCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblRemainingCaption.Location = new System.Drawing.Point(458, 104);
			this.lblRemainingCaption.Name = "lblRemainingCaption";
			this.lblRemainingCaption.Size = new System.Drawing.Size(136, 17);
			this.lblRemainingCaption.TabIndex = 9;
			this.lblRemainingCaption.Text = "Time left";
			// 
			// lblRemainingValue
			// 
			this.lblRemainingValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRemainingValue.AutoEllipsis = true;
			this.lblRemainingValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRemainingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblRemainingValue.Location = new System.Drawing.Point(458, 121);
			this.lblRemainingValue.Name = "lblRemainingValue";
			this.lblRemainingValue.Size = new System.Drawing.Size(136, 24);
			this.lblRemainingValue.TabIndex = 10;
			this.lblRemainingValue.Text = "-";
			// 
			// lblErrorsCaption
			// 
			this.lblErrorsCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblErrorsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblErrorsCaption.Location = new System.Drawing.Point(604, 104);
			this.lblErrorsCaption.Name = "lblErrorsCaption";
			this.lblErrorsCaption.Size = new System.Drawing.Size(136, 17);
			this.lblErrorsCaption.TabIndex = 11;
			this.lblErrorsCaption.Text = "Errors";
			// 
			// lblErrorsValue
			// 
			this.lblErrorsValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblErrorsValue.AutoEllipsis = true;
			this.lblErrorsValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblErrorsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
			this.lblErrorsValue.Location = new System.Drawing.Point(604, 121);
			this.lblErrorsValue.Name = "lblErrorsValue";
			this.lblErrorsValue.Size = new System.Drawing.Size(136, 24);
			this.lblErrorsValue.TabIndex = 12;
			this.lblErrorsValue.Text = "-";
			// 
			// lblDirectoryCaption
			// 
			this.lblDirectoryCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblDirectoryCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblDirectoryCaption.Location = new System.Drawing.Point(20, 168);
			this.lblDirectoryCaption.Name = "lblDirectoryCaption";
			this.lblDirectoryCaption.Size = new System.Drawing.Size(72, 17);
			this.lblDirectoryCaption.TabIndex = 13;
			this.lblDirectoryCaption.Text = "Source:";
			// 
			// lblDirectory
			// 
			this.lblDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDirectory.AutoEllipsis = true;
			this.lblDirectory.Location = new System.Drawing.Point(95, 168);
			this.lblDirectory.Name = "lblDirectory";
			this.lblDirectory.Size = new System.Drawing.Size(645, 17);
			this.lblDirectory.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.label1.Location = new System.Drawing.Point(20, 190);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 17);
			this.label1.TabIndex = 15;
			this.label1.Text = "Target:";
			// 
			// lblTargetDir
			// 
			this.lblTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTargetDir.AutoEllipsis = true;
			this.lblTargetDir.Location = new System.Drawing.Point(95, 190);
			this.lblTargetDir.Name = "lblTargetDir";
			this.lblTargetDir.Size = new System.Drawing.Size(645, 17);
			this.lblTargetDir.TabIndex = 16;
			// 
			// lblFileCaption
			// 
			this.lblFileCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFileCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblFileCaption.Location = new System.Drawing.Point(20, 212);
			this.lblFileCaption.Name = "lblFileCaption";
			this.lblFileCaption.Size = new System.Drawing.Size(72, 17);
			this.lblFileCaption.TabIndex = 17;
			this.lblFileCaption.Text = "File:";
			// 
			// lblFile
			// 
			this.lblFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFile.AutoEllipsis = true;
			this.lblFile.Location = new System.Drawing.Point(95, 212);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(525, 17);
			this.lblFile.TabIndex = 18;
			// 
			// lblSizeCaption
			// 
			this.lblSizeCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSizeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblSizeCaption.Location = new System.Drawing.Point(628, 212);
			this.lblSizeCaption.Name = "lblSizeCaption";
			this.lblSizeCaption.Size = new System.Drawing.Size(40, 17);
			this.lblSizeCaption.TabIndex = 19;
			this.lblSizeCaption.Text = "Size:";
			// 
			// lblSize
			// 
			this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSize.Location = new System.Drawing.Point(668, 212);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(72, 17);
			this.lblSize.TabIndex = 20;
			this.lblSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// OverallProgress
			// 
			this.OverallProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.OverallProgress.Location = new System.Drawing.Point(20, 48);
			this.OverallProgress.MarqueeAnimationSpeed = 40;
			this.OverallProgress.Maximum = 1000;
			this.OverallProgress.Name = "OverallProgress";
			this.OverallProgress.Size = new System.Drawing.Size(720, 20);
			this.OverallProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.OverallProgress.TabIndex = 30;
			// 
			// pnlSeparator
			// 
			this.pnlSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.pnlSeparator.Location = new System.Drawing.Point(20, 156);
			this.pnlSeparator.Name = "pnlSeparator";
			this.pnlSeparator.Size = new System.Drawing.Size(720, 1);
			this.pnlSeparator.TabIndex = 31;
			// 
			// bnDetails
			// 
			this.bnDetails.Location = new System.Drawing.Point(20, 246);
			this.bnDetails.Name = "bnDetails";
			this.bnDetails.Size = new System.Drawing.Size(120, 30);
			this.bnDetails.TabIndex = 0;
			this.bnDetails.Text = "Show details";
			this.bnDetails.UseVisualStyleBackColor = true;
			this.bnDetails.Click += new System.EventHandler(this.bnDetails_Click);
			// 
			// bnErrors
			// 
			this.bnErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnErrors.Location = new System.Drawing.Point(510, 246);
			this.bnErrors.Name = "bnErrors";
			this.bnErrors.Size = new System.Drawing.Size(110, 30);
			this.bnErrors.TabIndex = 1;
			this.bnErrors.Text = "Show errors";
			this.bnErrors.UseVisualStyleBackColor = true;
			this.bnErrors.Visible = false;
			this.bnErrors.Click += new System.EventHandler(this.bnErrors_Click);
			// 
			// bnClose
			// 
			this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bnClose.Location = new System.Drawing.Point(630, 246);
			this.bnClose.Name = "bnClose";
			this.bnClose.Size = new System.Drawing.Size(110, 30);
			this.bnClose.TabIndex = 2;
			this.bnClose.Text = "Close";
			this.bnClose.UseVisualStyleBackColor = true;
			this.bnClose.Visible = false;
			this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.BackColor = System.Drawing.Color.White;
			this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLog.Location = new System.Drawing.Point(20, 286);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(720, 0);
			this.txtLog.TabIndex = 3;
			this.txtLog.Visible = false;
			this.txtLog.WordWrap = false;
			// 
			// frmMainStatusStrip
			// 
			this.frmMainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStripStatus});
			this.frmMainStatusStrip.Location = new System.Drawing.Point(0, 290);
			this.frmMainStatusStrip.Name = "frmMainStatusStrip";
			this.frmMainStatusStrip.Size = new System.Drawing.Size(760, 22);
			this.frmMainStatusStrip.SizingGrip = false;
			this.frmMainStatusStrip.TabIndex = 32;
			// 
			// lblStripStatus
			// 
			this.lblStripStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblStripStatus.Name = "lblStripStatus";
			this.lblStripStatus.Size = new System.Drawing.Size(745, 17);
			this.lblStripStatus.Spring = true;
			this.lblStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(760, 312);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lblProgressText);
			this.Controls.Add(this.lblDirectoryCaption);
			this.Controls.Add(this.lblDirectory);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblTargetDir);
			this.Controls.Add(this.lblFileCaption);
			this.Controls.Add(this.lblFile);
			this.Controls.Add(this.lblSizeCaption);
			this.Controls.Add(this.lblSize);
			this.Controls.Add(this.lblFilesCaption);
			this.Controls.Add(this.lblSpeedCaption);
			this.Controls.Add(this.lblElapsedCaption);
			this.Controls.Add(this.lblRemainingCaption);
			this.Controls.Add(this.lblErrorsCaption);
			this.Controls.Add(this.lblFilesValue);
			this.Controls.Add(this.lblSpeedValue);
			this.Controls.Add(this.lblElapsedValue);
			this.Controls.Add(this.lblRemainingValue);
			this.Controls.Add(this.lblErrorsValue);
			this.Controls.Add(this.OverallProgress);
			this.Controls.Add(this.pnlSeparator);
			this.Controls.Add(this.bnDetails);
			this.Controls.Add(this.bnErrors);
			this.Controls.Add(this.bnClose);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.frmMainStatusStrip);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(560, 351);
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

		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblProgressText;
		private System.Windows.Forms.Label lblDirectoryCaption;
		private System.Windows.Forms.Label lblDirectory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblTargetDir;
		private System.Windows.Forms.Label lblFileCaption;
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.Label lblSizeCaption;
		private System.Windows.Forms.Label lblSize;
		private System.Windows.Forms.Label lblFilesCaption;
		private System.Windows.Forms.Label lblSpeedCaption;
		private System.Windows.Forms.Label lblElapsedCaption;
		private System.Windows.Forms.Label lblRemainingCaption;
		private System.Windows.Forms.Label lblErrorsCaption;
		private System.Windows.Forms.Label lblFilesValue;
		private System.Windows.Forms.Label lblSpeedValue;
		private System.Windows.Forms.Label lblElapsedValue;
		private System.Windows.Forms.Label lblRemainingValue;
		private System.Windows.Forms.Label lblErrorsValue;
		private System.Windows.Forms.ProgressBar OverallProgress;
		private System.Windows.Forms.Panel pnlSeparator;
		private System.Windows.Forms.Button bnDetails;
		private System.Windows.Forms.Button bnErrors;
		private System.Windows.Forms.Button bnClose;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Timer DotTimer;
		private System.Windows.Forms.Timer CloseTimer;
		private System.Windows.Forms.StatusStrip frmMainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel lblStripStatus;
	}
}
