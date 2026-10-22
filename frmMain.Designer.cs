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
			this.OverallProgress = new System.Windows.Forms.ProgressBar();
			this.lblProgressText = new System.Windows.Forms.Label();
			this.lblFilesCaption = new System.Windows.Forms.Label();
			this.lblFilesValue = new System.Windows.Forms.Label();
			this.lblSpeedCaption = new System.Windows.Forms.Label();
			this.lblSpeedValue = new System.Windows.Forms.Label();
			this.lblFileRateCaption = new System.Windows.Forms.Label();
			this.lblFileRateValue = new System.Windows.Forms.Label();
			this.lblElapsedCaption = new System.Windows.Forms.Label();
			this.lblElapsedValue = new System.Windows.Forms.Label();
			this.lblRemainingCaption = new System.Windows.Forms.Label();
			this.lblRemainingValue = new System.Windows.Forms.Label();
			this.lblErrorsCaption = new System.Windows.Forms.Label();
			this.lblErrorsValue = new System.Windows.Forms.Label();
			this.lblJobsCaption = new System.Windows.Forms.Label();
			this.lblJobsValue = new System.Windows.Forms.Label();
			this.pnlSeparator = new System.Windows.Forms.Panel();
			this.tabJobs = new System.Windows.Forms.TabControl();
			this.bnLogs = new System.Windows.Forms.Button();
			this.bnCommand = new System.Windows.Forms.Button();
			this.bnErrors = new System.Windows.Forms.Button();
			this.bnClose = new System.Windows.Forms.Button();
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
			this.lblStatus.Location = new System.Drawing.Point(18, 14);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(784, 28);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "Starting...";
			this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.AutoEllipsis = true;
			// 
			// OverallProgress
			// 
			this.OverallProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.OverallProgress.Location = new System.Drawing.Point(20, 48);
			this.OverallProgress.Name = "OverallProgress";
			this.OverallProgress.Size = new System.Drawing.Size(780, 20);
			this.OverallProgress.TabIndex = 2;
			this.OverallProgress.MarqueeAnimationSpeed = 30;
			this.OverallProgress.Maximum = 10000;
			this.OverallProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			// 
			// lblProgressText
			// 
			this.lblProgressText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgressText.Location = new System.Drawing.Point(20, 74);
			this.lblProgressText.Name = "lblProgressText";
			this.lblProgressText.Size = new System.Drawing.Size(780, 18);
			this.lblProgressText.TabIndex = 3;
			this.lblProgressText.Text = "";
			this.lblProgressText.AutoEllipsis = true;
			this.lblProgressText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblFilesCaption
			// 
			this.lblFilesCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFilesCaption.Location = new System.Drawing.Point(20, 104);
			this.lblFilesCaption.Name = "lblFilesCaption";
			this.lblFilesCaption.Size = new System.Drawing.Size(105, 17);
			this.lblFilesCaption.TabIndex = 4;
			this.lblFilesCaption.Text = "Files";
			this.lblFilesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblFilesValue
			// 
			this.lblFilesValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFilesValue.Location = new System.Drawing.Point(20, 121);
			this.lblFilesValue.Name = "lblFilesValue";
			this.lblFilesValue.Size = new System.Drawing.Size(105, 24);
			this.lblFilesValue.TabIndex = 5;
			this.lblFilesValue.Text = "0";
			this.lblFilesValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFilesValue.AutoEllipsis = true;
			// 
			// lblSpeedCaption
			// 
			this.lblSpeedCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblSpeedCaption.Location = new System.Drawing.Point(131, 104);
			this.lblSpeedCaption.Name = "lblSpeedCaption";
			this.lblSpeedCaption.Size = new System.Drawing.Size(105, 17);
			this.lblSpeedCaption.TabIndex = 6;
			this.lblSpeedCaption.Text = "Speed";
			this.lblSpeedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblFileRateCaption
			// 
			this.lblFileRateCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFileRateCaption.Location = new System.Drawing.Point(242, 104);
			this.lblFileRateCaption.Name = "lblFileRateCaption";
			this.lblFileRateCaption.Size = new System.Drawing.Size(105, 17);
			this.lblFileRateCaption.TabIndex = 6;
			this.lblFileRateCaption.Text = "Files/s";
			this.lblFileRateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblSpeedValue
			// 
			this.lblSpeedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblSpeedValue.Location = new System.Drawing.Point(131, 121);
			this.lblSpeedValue.Name = "lblSpeedValue";
			this.lblSpeedValue.Size = new System.Drawing.Size(105, 24);
			this.lblSpeedValue.TabIndex = 7;
			this.lblSpeedValue.Text = "-";
			this.lblSpeedValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedValue.AutoEllipsis = true;
			// 
			// lblFileRateValue
			// 
			this.lblFileRateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblFileRateValue.Location = new System.Drawing.Point(242, 121);
			this.lblFileRateValue.Name = "lblFileRateValue";
			this.lblFileRateValue.Size = new System.Drawing.Size(105, 24);
			this.lblFileRateValue.TabIndex = 7;
			this.lblFileRateValue.Text = "-";
			this.lblFileRateValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFileRateValue.AutoEllipsis = true;
			// 
			// lblElapsedCaption
			// 
			this.lblElapsedCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblElapsedCaption.Location = new System.Drawing.Point(353, 104);
			this.lblElapsedCaption.Name = "lblElapsedCaption";
			this.lblElapsedCaption.Size = new System.Drawing.Size(105, 17);
			this.lblElapsedCaption.TabIndex = 8;
			this.lblElapsedCaption.Text = "Elapsed";
			this.lblElapsedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblElapsedValue
			// 
			this.lblElapsedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblElapsedValue.Location = new System.Drawing.Point(353, 121);
			this.lblElapsedValue.Name = "lblElapsedValue";
			this.lblElapsedValue.Size = new System.Drawing.Size(105, 24);
			this.lblElapsedValue.TabIndex = 9;
			this.lblElapsedValue.Text = "-";
			this.lblElapsedValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblElapsedValue.AutoEllipsis = true;
			// 
			// lblRemainingCaption
			// 
			this.lblRemainingCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRemainingCaption.Location = new System.Drawing.Point(464, 104);
			this.lblRemainingCaption.Name = "lblRemainingCaption";
			this.lblRemainingCaption.Size = new System.Drawing.Size(105, 17);
			this.lblRemainingCaption.TabIndex = 10;
			this.lblRemainingCaption.Text = "Time left";
			this.lblRemainingCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblRemainingValue
			// 
			this.lblRemainingValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRemainingValue.Location = new System.Drawing.Point(464, 121);
			this.lblRemainingValue.Name = "lblRemainingValue";
			this.lblRemainingValue.Size = new System.Drawing.Size(105, 24);
			this.lblRemainingValue.TabIndex = 11;
			this.lblRemainingValue.Text = "-";
			this.lblRemainingValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRemainingValue.AutoEllipsis = true;
			// 
			// lblErrorsCaption
			// 
			this.lblErrorsCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblErrorsCaption.Location = new System.Drawing.Point(575, 104);
			this.lblErrorsCaption.Name = "lblErrorsCaption";
			this.lblErrorsCaption.Size = new System.Drawing.Size(105, 17);
			this.lblErrorsCaption.TabIndex = 12;
			this.lblErrorsCaption.Text = "Errors";
			this.lblErrorsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblErrorsValue
			// 
			this.lblErrorsValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblErrorsValue.Location = new System.Drawing.Point(575, 121);
			this.lblErrorsValue.Name = "lblErrorsValue";
			this.lblErrorsValue.Size = new System.Drawing.Size(105, 24);
			this.lblErrorsValue.TabIndex = 13;
			this.lblErrorsValue.Text = "0";
			this.lblErrorsValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblErrorsValue.AutoEllipsis = true;
			// 
			// lblJobsCaption
			// 
			this.lblJobsCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblJobsCaption.Location = new System.Drawing.Point(686, 104);
			this.lblJobsCaption.Name = "lblJobsCaption";
			this.lblJobsCaption.Size = new System.Drawing.Size(105, 17);
			this.lblJobsCaption.TabIndex = 14;
			this.lblJobsCaption.Text = "Jobs done";
			this.lblJobsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
			// 
			// lblJobsValue
			// 
			this.lblJobsValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.lblJobsValue.Location = new System.Drawing.Point(686, 121);
			this.lblJobsValue.Name = "lblJobsValue";
			this.lblJobsValue.Size = new System.Drawing.Size(105, 24);
			this.lblJobsValue.TabIndex = 15;
			this.lblJobsValue.Text = "-";
			this.lblJobsValue.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblJobsValue.AutoEllipsis = true;
			// 
			// pnlSeparator
			// 
			this.pnlSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSeparator.Location = new System.Drawing.Point(20, 156);
			this.pnlSeparator.Name = "pnlSeparator";
			this.pnlSeparator.Size = new System.Drawing.Size(780, 1);
			this.pnlSeparator.TabIndex = 16;
			this.pnlSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(222)))));
			// 
			// tabJobs
			// 
			this.tabJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.tabJobs.Location = new System.Drawing.Point(20, 168);
			this.tabJobs.Name = "tabJobs";
			this.tabJobs.Size = new System.Drawing.Size(780, 386);
			this.tabJobs.TabIndex = 17;
			this.tabJobs.SelectedIndex = 0;
			this.tabJobs.Padding = new System.Drawing.Point(10, 5);
			this.tabJobs.SelectedIndexChanged += new System.EventHandler(this.tabJobs_SelectedIndexChanged);
			// 
			// bnLogs
			// 
			this.bnLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnLogs.Location = new System.Drawing.Point(20, 566);
			this.bnLogs.Name = "bnLogs";
			this.bnLogs.Size = new System.Drawing.Size(130, 30);
			this.bnLogs.TabIndex = 18;
			this.bnLogs.Text = "Open log folder";
			this.bnLogs.Click += new System.EventHandler(this.bnLogs_Click);
			// 
			// bnCommand
			// 
			this.bnCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnCommand.Location = new System.Drawing.Point(160, 566);
			this.bnCommand.Name = "bnCommand";
			this.bnCommand.Size = new System.Drawing.Size(130, 30);
			this.bnCommand.TabIndex = 19;
			this.bnCommand.Text = "View command...";
			this.bnCommand.Enabled = false;
			this.bnCommand.Click += new System.EventHandler(this.bnCommand_Click);
			// 
			// bnErrors
			// 
			this.bnErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnErrors.Location = new System.Drawing.Point(570, 566);
			this.bnErrors.Name = "bnErrors";
			this.bnErrors.Size = new System.Drawing.Size(110, 30);
			this.bnErrors.TabIndex = 19;
			this.bnErrors.Text = "Show errors";
			this.bnErrors.Visible = false;
			this.bnErrors.Click += new System.EventHandler(this.bnErrors_Click);
			// 
			// bnClose
			// 
			this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bnClose.Location = new System.Drawing.Point(690, 566);
			this.bnClose.Name = "bnClose";
			this.bnClose.Size = new System.Drawing.Size(110, 30);
			this.bnClose.TabIndex = 20;
			this.bnClose.Text = "Close";
			this.bnClose.Tag = "primary";
			this.bnClose.Visible = false;
			this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
			// 
			// frmMainStatusStrip
			// 
			this.frmMainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStripStatus});
			this.frmMainStatusStrip.Location = new System.Drawing.Point(0, 608);
			this.frmMainStatusStrip.Name = "frmMainStatusStrip";
			this.frmMainStatusStrip.Size = new System.Drawing.Size(820, 22);
			this.frmMainStatusStrip.SizingGrip = true;
			this.frmMainStatusStrip.TabIndex = 99;
			// 
			// lblStripStatus
			// 
			this.lblStripStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblStripStatus.Name = "lblStripStatus";
			this.lblStripStatus.Size = new System.Drawing.Size(790, 17);
			this.lblStripStatus.Spring = true;
			this.lblStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(820, 630);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.OverallProgress);
			this.Controls.Add(this.lblProgressText);
			this.Controls.Add(this.lblFilesCaption);
			this.Controls.Add(this.lblFilesValue);
			this.Controls.Add(this.lblSpeedCaption);
			this.Controls.Add(this.lblSpeedValue);
			this.Controls.Add(this.lblFileRateCaption);
			this.Controls.Add(this.lblFileRateValue);
			this.Controls.Add(this.lblElapsedCaption);
			this.Controls.Add(this.lblElapsedValue);
			this.Controls.Add(this.lblRemainingCaption);
			this.Controls.Add(this.lblRemainingValue);
			this.Controls.Add(this.lblErrorsCaption);
			this.Controls.Add(this.lblErrorsValue);
			this.Controls.Add(this.lblJobsCaption);
			this.Controls.Add(this.lblJobsValue);
			this.Controls.Add(this.pnlSeparator);
			this.Controls.Add(this.tabJobs);
			this.Controls.Add(this.bnLogs);
			this.Controls.Add(this.bnCommand);
			this.Controls.Add(this.bnErrors);
			this.Controls.Add(this.bnClose);
			this.Controls.Add(this.frmMainStatusStrip);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(660, 520);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RoboG";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHaupt_FormClosing);
			this.Shown += new System.EventHandler(this.frmMain_Shown);
			this.frmMainStatusStrip.ResumeLayout(false);
			this.frmMainStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ProgressBar OverallProgress;
		private System.Windows.Forms.Label lblProgressText;
		private System.Windows.Forms.Label lblFilesCaption;
		private System.Windows.Forms.Label lblFilesValue;
		private System.Windows.Forms.Label lblSpeedCaption;
		private System.Windows.Forms.Label lblSpeedValue;
		private System.Windows.Forms.Label lblFileRateCaption;
		private System.Windows.Forms.Label lblFileRateValue;
		private System.Windows.Forms.Label lblElapsedCaption;
		private System.Windows.Forms.Label lblElapsedValue;
		private System.Windows.Forms.Label lblRemainingCaption;
		private System.Windows.Forms.Label lblRemainingValue;
		private System.Windows.Forms.Label lblErrorsCaption;
		private System.Windows.Forms.Label lblErrorsValue;
		private System.Windows.Forms.Label lblJobsCaption;
		private System.Windows.Forms.Label lblJobsValue;
		private System.Windows.Forms.Panel pnlSeparator;
		private System.Windows.Forms.TabControl tabJobs;
		private System.Windows.Forms.Button bnLogs;
		private System.Windows.Forms.Button bnCommand;
		private System.Windows.Forms.Button bnErrors;
		private System.Windows.Forms.Button bnClose;
		private System.Windows.Forms.Timer DotTimer;
		private System.Windows.Forms.Timer CloseTimer;
		private System.Windows.Forms.StatusStrip frmMainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel lblStripStatus;
	}
}
