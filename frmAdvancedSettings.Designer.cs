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
    public partial class frmAdvancedSettings : System.Windows.Forms.Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdvancedSettings));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.txtArgumentsProgramBeforeBackup = new System.Windows.Forms.TextBox();
			this.txtArgumentsProgramAfterBackup = new System.Windows.Forms.TextBox();
			this.txtArgumentsProgramOnError = new System.Windows.Forms.TextBox();
			this.lblRetries = new System.Windows.Forms.Label();
			this.lblWaitBetweenRetries = new System.Windows.Forms.Label();
			this.lblSecond = new System.Windows.Forms.Label();
			this.gbProgramBeforeBackup = new System.Windows.Forms.GroupBox();
			this.chkProgramBeforeBackupAdmin = new System.Windows.Forms.CheckBox();
			this.chkStopIfBeforeFails = new System.Windows.Forms.CheckBox();
			this.lblArguments1 = new System.Windows.Forms.Label();
			this.lblProgram1 = new System.Windows.Forms.Label();
			this.bnSelectProgramBeforeBackup = new System.Windows.Forms.Button();
			this.gbProgramAfterBackup = new System.Windows.Forms.GroupBox();
			this.chkProgramAfterBackupAdmin = new System.Windows.Forms.CheckBox();
			this.lblArguments2 = new System.Windows.Forms.Label();
			this.lblProgram2 = new System.Windows.Forms.Label();
			this.bnSelectProgramAfterBackup = new System.Windows.Forms.Button();
			this.bnCancel = new System.Windows.Forms.Button();
			this.bnOK = new System.Windows.Forms.Button();
			this.chkAutoExit = new System.Windows.Forms.CheckBox();
			this.chkAutoExit2 = new System.Windows.Forms.CheckBox();
			this.chkSilentMode = new System.Windows.Forms.CheckBox();
			this.gbProgramOnError = new System.Windows.Forms.GroupBox();
			this.chkProgramOnErrorAdmin = new System.Windows.Forms.CheckBox();
			this.lblArguments3 = new System.Windows.Forms.Label();
			this.lblProgram3 = new System.Windows.Forms.Label();
			this.bnSelectProgramOnError = new System.Windows.Forms.Button();
			this.chkScan = new System.Windows.Forms.CheckBox();
			this.chkLogFiles = new System.Windows.Forms.CheckBox();
			this.lblThreads = new System.Windows.Forms.Label();
			this.numThreads = new System.Windows.Forms.NumericUpDown();
			this.lblThreadsHint = new System.Windows.Forms.Label();
			this.lblParallelJobs = new System.Windows.Forms.Label();
			this.gbLogs = new System.Windows.Forms.GroupBox();
			this.chkLogCopiedFiles = new System.Windows.Forms.CheckBox();
			this.lblLogFolder = new System.Windows.Forms.Label();
			this.txtLogFolder = new System.Windows.Forms.TextBox();
			this.bnSelectLogFolder = new System.Windows.Forms.Button();
			this.lblKeepRuns = new System.Windows.Forms.Label();
			this.numKeepRuns = new System.Windows.Forms.NumericUpDown();
			this.lblKeepRunsUnit = new System.Windows.Forms.Label();
			this.numParallelJobs = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numParallelJobs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numKeepRuns)).BeginInit();
			this.gbLogs.SuspendLayout();
			this.txtProgramOnError = new RoboG.PathTextBox();
			this.txtProgramAfterBackup = new RoboG.PathTextBox();
			this.txtProgramBeforeBackup = new RoboG.PathTextBox();
			this.txtWaitBetweenRetries = new RoboG.NumericTextBox();
			this.txtRetries = new RoboG.NumericTextBox();
			this.gbProgramBeforeBackup.SuspendLayout();
			this.gbProgramAfterBackup.SuspendLayout();
			this.gbProgramOnError.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtArgumentsProgramBeforeBackup
			// 
			this.txtArgumentsProgramBeforeBackup.BackColor = System.Drawing.SystemColors.Window;
			this.txtArgumentsProgramBeforeBackup.Location = new System.Drawing.Point(142, 44);
			this.txtArgumentsProgramBeforeBackup.Name = "txtArgumentsProgramBeforeBackup";
			this.txtArgumentsProgramBeforeBackup.Size = new System.Drawing.Size(296, 20);
			this.txtArgumentsProgramBeforeBackup.TabIndex = 1;
			this.toolTip.SetToolTip(this.txtArgumentsProgramBeforeBackup, "Arguments");
			// 
			// txtArgumentsProgramAfterBackup
			// 
			this.txtArgumentsProgramAfterBackup.BackColor = System.Drawing.SystemColors.Window;
			this.txtArgumentsProgramAfterBackup.Location = new System.Drawing.Point(142, 44);
			this.txtArgumentsProgramAfterBackup.Name = "txtArgumentsProgramAfterBackup";
			this.txtArgumentsProgramAfterBackup.Size = new System.Drawing.Size(296, 20);
			this.txtArgumentsProgramAfterBackup.TabIndex = 1;
			this.toolTip.SetToolTip(this.txtArgumentsProgramAfterBackup, "Arguments");
			// 
			// txtArgumentsProgramOnError
			// 
			this.txtArgumentsProgramOnError.BackColor = System.Drawing.SystemColors.Window;
			this.txtArgumentsProgramOnError.Location = new System.Drawing.Point(142, 45);
			this.txtArgumentsProgramOnError.Name = "txtArgumentsProgramOnError";
			this.txtArgumentsProgramOnError.Size = new System.Drawing.Size(296, 20);
			this.txtArgumentsProgramOnError.TabIndex = 1;
			this.toolTip.SetToolTip(this.txtArgumentsProgramOnError, "Arguments");
			// 
			// lblRetries
			// 
			this.lblRetries.AutoSize = true;
			this.lblRetries.Location = new System.Drawing.Point(12, 25);
			this.lblRetries.Name = "lblRetries";
			this.lblRetries.Size = new System.Drawing.Size(185, 13);
			this.lblRetries.TabIndex = 0;
			this.lblRetries.Text = "Number of retries on failed operations:";
			// 
			// lblWaitBetweenRetries
			// 
			this.lblWaitBetweenRetries.AutoSize = true;
			this.lblWaitBetweenRetries.Location = new System.Drawing.Point(12, 51);
			this.lblWaitBetweenRetries.Name = "lblWaitBetweenRetries";
			this.lblWaitBetweenRetries.Size = new System.Drawing.Size(129, 13);
			this.lblWaitBetweenRetries.TabIndex = 2;
			this.lblWaitBetweenRetries.Text = "Wait time between retries:";
			// 
			// lblSecond
			// 
			this.lblSecond.AutoSize = true;
			this.lblSecond.Location = new System.Drawing.Point(246, 51);
			this.lblSecond.Name = "lblSecond";
			this.lblSecond.Size = new System.Drawing.Size(27, 13);
			this.lblSecond.TabIndex = 2;
			this.lblSecond.Text = "sec.";
			// 
			// gbProgramBeforeBackup
			// 
			this.gbProgramBeforeBackup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.gbProgramBeforeBackup.Controls.Add(this.chkProgramBeforeBackupAdmin);
			this.gbProgramBeforeBackup.Controls.Add(this.chkStopIfBeforeFails);
			this.gbProgramBeforeBackup.Controls.Add(this.txtArgumentsProgramBeforeBackup);
			this.gbProgramBeforeBackup.Controls.Add(this.lblArguments1);
			this.gbProgramBeforeBackup.Controls.Add(this.lblProgram1);
			this.gbProgramBeforeBackup.Controls.Add(this.bnSelectProgramBeforeBackup);
			this.gbProgramBeforeBackup.Controls.Add(this.txtProgramBeforeBackup);
			this.gbProgramBeforeBackup.Location = new System.Drawing.Point(18, 291);
			this.gbProgramBeforeBackup.Name = "gbProgramBeforeBackup";
			this.gbProgramBeforeBackup.Size = new System.Drawing.Size(464, 94);
			this.gbProgramBeforeBackup.TabIndex = 3;
			this.gbProgramBeforeBackup.TabStop = false;
			this.gbProgramBeforeBackup.Text = "Run program or script before backup";
			// 
			// chkProgramBeforeBackupAdmin
			// 
			this.chkProgramBeforeBackupAdmin.AutoSize = true;
			this.chkProgramBeforeBackupAdmin.Location = new System.Drawing.Point(290, 70);
			this.chkProgramBeforeBackupAdmin.AutoSize = true;
			this.chkProgramBeforeBackupAdmin.Name = "chkProgramBeforeBackupAdmin";
			this.chkProgramBeforeBackupAdmin.Size = new System.Drawing.Size(124, 17);
			this.chkProgramBeforeBackupAdmin.TabIndex = 2;
			this.chkProgramBeforeBackupAdmin.Text = "Start as administrator";
			this.chkProgramBeforeBackupAdmin.UseVisualStyleBackColor = true;
			// 
			// chkStopIfBeforeFails
			// 
			this.chkStopIfBeforeFails.AutoSize = true;
			this.chkStopIfBeforeFails.Location = new System.Drawing.Point(9, 70);
			this.chkStopIfBeforeFails.Name = "chkStopIfBeforeFails";
			this.chkStopIfBeforeFails.Size = new System.Drawing.Size(150, 17);
			this.chkStopIfBeforeFails.TabIndex = 4;
			this.chkStopIfBeforeFails.Text = "Don't sync if it fails";
			this.chkStopIfBeforeFails.UseVisualStyleBackColor = true;
			// 
			// lblArguments1
			// 
			this.lblArguments1.AutoSize = true;
			this.lblArguments1.Location = new System.Drawing.Point(6, 45);
			this.lblArguments1.Name = "lblArguments1";
			this.lblArguments1.Size = new System.Drawing.Size(60, 13);
			this.lblArguments1.TabIndex = 6;
			this.lblArguments1.Text = "Arguments:";
			// 
			// lblProgram1
			// 
			this.lblProgram1.AutoSize = true;
			this.lblProgram1.Location = new System.Drawing.Point(6, 22);
			this.lblProgram1.Name = "lblProgram1";
			this.lblProgram1.Size = new System.Drawing.Size(81, 13);
			this.lblProgram1.TabIndex = 5;
			this.lblProgram1.Text = "Program/Script:";
			// 
			// bnSelectProgramBeforeBackup
			// 
			this.bnSelectProgramBeforeBackup.Location = new System.Drawing.Point(407, 17);
			this.bnSelectProgramBeforeBackup.Name = "bnSelectProgramBeforeBackup";
			this.bnSelectProgramBeforeBackup.Size = new System.Drawing.Size(28, 21);
			this.bnSelectProgramBeforeBackup.TabIndex = 4;
			this.bnSelectProgramBeforeBackup.Text = "...";
			this.bnSelectProgramBeforeBackup.UseVisualStyleBackColor = true;
			this.bnSelectProgramBeforeBackup.Click += new System.EventHandler(this.bnSelectProgramBeforeBackup_Click);
			// 
			// gbProgramAfterBackup
			// 
			this.gbProgramAfterBackup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.gbProgramAfterBackup.Controls.Add(this.chkProgramAfterBackupAdmin);
			this.gbProgramAfterBackup.Controls.Add(this.txtArgumentsProgramAfterBackup);
			this.gbProgramAfterBackup.Controls.Add(this.lblArguments2);
			this.gbProgramAfterBackup.Controls.Add(this.lblProgram2);
			this.gbProgramAfterBackup.Controls.Add(this.bnSelectProgramAfterBackup);
			this.gbProgramAfterBackup.Controls.Add(this.txtProgramAfterBackup);
			this.gbProgramAfterBackup.Location = new System.Drawing.Point(18, 384);
			this.gbProgramAfterBackup.Name = "gbProgramAfterBackup";
			this.gbProgramAfterBackup.Size = new System.Drawing.Size(464, 94);
			this.gbProgramAfterBackup.TabIndex = 4;
			this.gbProgramAfterBackup.TabStop = false;
			this.gbProgramAfterBackup.Text = "Run program or script after backup";
			// 
			// chkProgramAfterBackupAdmin
			// 
			this.chkProgramAfterBackupAdmin.AutoSize = true;
			this.chkProgramAfterBackupAdmin.Location = new System.Drawing.Point(290, 70);
			this.chkProgramAfterBackupAdmin.AutoSize = true;
			this.chkProgramAfterBackupAdmin.Name = "chkProgramAfterBackupAdmin";
			this.chkProgramAfterBackupAdmin.Size = new System.Drawing.Size(124, 17);
			this.chkProgramAfterBackupAdmin.TabIndex = 2;
			this.chkProgramAfterBackupAdmin.Text = "Start as administrator";
			this.chkProgramAfterBackupAdmin.UseVisualStyleBackColor = true;
			// 
			// lblArguments2
			// 
			this.lblArguments2.AutoSize = true;
			this.lblArguments2.Location = new System.Drawing.Point(6, 44);
			this.lblArguments2.Name = "lblArguments2";
			this.lblArguments2.Size = new System.Drawing.Size(60, 13);
			this.lblArguments2.TabIndex = 6;
			this.lblArguments2.Text = "Arguments:";
			// 
			// lblProgram2
			// 
			this.lblProgram2.AutoSize = true;
			this.lblProgram2.Location = new System.Drawing.Point(6, 22);
			this.lblProgram2.Name = "lblProgram2";
			this.lblProgram2.Size = new System.Drawing.Size(81, 13);
			this.lblProgram2.TabIndex = 5;
			this.lblProgram2.Text = "Program/Script:";
			// 
			// bnSelectProgramAfterBackup
			// 
			this.bnSelectProgramAfterBackup.Location = new System.Drawing.Point(407, 17);
			this.bnSelectProgramAfterBackup.Name = "bnSelectProgramAfterBackup";
			this.bnSelectProgramAfterBackup.Size = new System.Drawing.Size(28, 21);
			this.bnSelectProgramAfterBackup.TabIndex = 4;
			this.bnSelectProgramAfterBackup.Text = "...";
			this.bnSelectProgramAfterBackup.UseVisualStyleBackColor = true;
			this.bnSelectProgramAfterBackup.Click += new System.EventHandler(this.bnSelectProgramAfterBackup_Click);
			// 
			// bnCancel
			// 
			this.bnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(408, 584);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(76, 28);
			this.bnCancel.TabIndex = 6;
			this.bnCancel.Text = "Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			// 
			// bnOK
			// 
			this.bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.bnOK.Location = new System.Drawing.Point(326, 584);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(76, 28);
			this.bnOK.TabIndex = 5;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// chkAutoExit
			// 
			this.chkAutoExit.AutoSize = true;
			this.chkAutoExit.Location = new System.Drawing.Point(15, 76);
			this.chkAutoExit.Name = "chkAutoExit";
			this.chkAutoExit.Size = new System.Drawing.Size(333, 17);
			this.chkAutoExit.TabIndex = 7;
			this.chkAutoExit.Text = "Close program automatically, when backup finished without errors";
			this.chkAutoExit.UseVisualStyleBackColor = true;
			this.chkAutoExit.Click += new System.EventHandler(this.chkAutoExit_Click);
			// 
			// chkAutoExit2
			// 
			this.chkAutoExit2.AutoSize = true;
			this.chkAutoExit2.Location = new System.Drawing.Point(15, 99);
			this.chkAutoExit2.Name = "chkAutoExit2";
			this.chkAutoExit2.Size = new System.Drawing.Size(312, 17);
			this.chkAutoExit2.TabIndex = 8;
			this.chkAutoExit2.Text = "Always close program automatically, when backup is finished";
			this.chkAutoExit2.UseVisualStyleBackColor = true;
			this.chkAutoExit2.Click += new System.EventHandler(this.chkAutoExit2_Click);
			// 
			// chkSilentMode
			// 
			this.chkSilentMode.AutoSize = true;
			this.chkSilentMode.Location = new System.Drawing.Point(15, 122);
			this.chkSilentMode.Name = "chkSilentMode";
			this.chkSilentMode.Size = new System.Drawing.Size(280, 17);
			this.chkSilentMode.TabIndex = 10;
			this.chkSilentMode.Text = "Silent mode (don\'t show error messages, just log them)";
			this.chkSilentMode.UseVisualStyleBackColor = true;
			// 
			// gbProgramOnError
			// 
			this.gbProgramOnError.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.gbProgramOnError.Controls.Add(this.chkProgramOnErrorAdmin);
			this.gbProgramOnError.Controls.Add(this.txtArgumentsProgramOnError);
			this.gbProgramOnError.Controls.Add(this.lblArguments3);
			this.gbProgramOnError.Controls.Add(this.lblProgram3);
			this.gbProgramOnError.Controls.Add(this.bnSelectProgramOnError);
			this.gbProgramOnError.Controls.Add(this.txtProgramOnError);
			this.gbProgramOnError.Location = new System.Drawing.Point(18, 484);
			this.gbProgramOnError.Name = "gbProgramOnError";
			this.gbProgramOnError.Size = new System.Drawing.Size(464, 94);
			this.gbProgramOnError.TabIndex = 12;
			this.gbProgramOnError.TabStop = false;
			this.gbProgramOnError.Text = "Run program or script on error";
			// 
			// chkProgramOnErrorAdmin
			// 
			this.chkProgramOnErrorAdmin.AutoSize = true;
			this.chkProgramOnErrorAdmin.Location = new System.Drawing.Point(290, 71);
			this.chkProgramOnErrorAdmin.AutoSize = true;
			this.chkProgramOnErrorAdmin.Name = "chkProgramOnErrorAdmin";
			this.chkProgramOnErrorAdmin.Size = new System.Drawing.Size(124, 17);
			this.chkProgramOnErrorAdmin.TabIndex = 2;
			this.chkProgramOnErrorAdmin.Text = "Start as administrator";
			this.chkProgramOnErrorAdmin.UseVisualStyleBackColor = true;
			// 
			// lblArguments3
			// 
			this.lblArguments3.AutoSize = true;
			this.lblArguments3.Location = new System.Drawing.Point(6, 45);
			this.lblArguments3.Name = "lblArguments3";
			this.lblArguments3.Size = new System.Drawing.Size(60, 13);
			this.lblArguments3.TabIndex = 6;
			this.lblArguments3.Text = "Arguments:";
			// 
			// lblProgram3
			// 
			this.lblProgram3.AutoSize = true;
			this.lblProgram3.Location = new System.Drawing.Point(6, 22);
			this.lblProgram3.Name = "lblProgram3";
			this.lblProgram3.Size = new System.Drawing.Size(81, 13);
			this.lblProgram3.TabIndex = 5;
			this.lblProgram3.Text = "Program/Script:";
			// 
			// bnSelectProgramOnError
			// 
			this.bnSelectProgramOnError.Location = new System.Drawing.Point(407, 17);
			this.bnSelectProgramOnError.Name = "bnSelectProgramOnError";
			this.bnSelectProgramOnError.Size = new System.Drawing.Size(28, 21);
			this.bnSelectProgramOnError.TabIndex = 4;
			this.bnSelectProgramOnError.Text = "...";
			this.bnSelectProgramOnError.UseVisualStyleBackColor = true;
			this.bnSelectProgramOnError.Click += new System.EventHandler(this.bnSelectProgramOnError_Click);
			// 
			// chkScan
			// 
			this.chkScan.AutoSize = true;
			this.chkScan.Checked = true;
			this.chkScan.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkScan.Location = new System.Drawing.Point(15, 145);
			this.chkScan.Name = "chkScan";
			this.chkScan.Size = new System.Drawing.Size(351, 17);
			this.chkScan.TabIndex = 13;
			this.chkScan.Text = "Scan first to show overall progress (adds a full pass; turn off for very large jobs)";
			// 
			// chkLogFiles
			// 
			this.chkLogFiles.AutoSize = true;
			this.chkLogFiles.Checked = true;
			this.chkLogFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLogFiles.Location = new System.Drawing.Point(15, 166);
			this.chkLogFiles.Name = "chkLogFiles";
			this.chkLogFiles.Size = new System.Drawing.Size(351, 17);
			this.chkLogFiles.TabIndex = 14;
			this.chkLogFiles.Text = "Show live progress (turn off for fastest speed)";
			this.chkLogFiles.UseVisualStyleBackColor = true;
			// 
			// lblThreads
			// 
			this.lblThreads.AutoSize = true;
			this.lblThreads.Location = new System.Drawing.Point(300, 25);
			this.lblThreads.Name = "lblThreads";
			this.lblThreads.Size = new System.Drawing.Size(74, 13);
			this.lblThreads.TabIndex = 15;
			this.lblThreads.Text = "Copy threads:";
			// 
			// numThreads
			// 
			this.numThreads.Location = new System.Drawing.Point(400, 22);
			this.numThreads.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.numThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numThreads.Name = "numThreads";
			this.numThreads.Size = new System.Drawing.Size(50, 20);
			this.numThreads.TabIndex = 16;
			this.numThreads.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// lblThreadsHint
			// 
			this.lblThreadsHint.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblThreadsHint.Location = new System.Drawing.Point(300, 46);
			this.lblThreadsHint.Name = "lblThreadsHint";
			this.lblThreadsHint.Size = new System.Drawing.Size(190, 28);
			this.lblThreadsHint.TabIndex = 17;
			this.lblThreadsHint.Text = "16-32 suits most jobs.";
			this.lblThreadsHint.Visible = false;
			// 
			// gbLogs
			// 
			this.gbLogs.Controls.Add(this.lblLogFolder);
			this.gbLogs.Controls.Add(this.txtLogFolder);
			this.gbLogs.Controls.Add(this.bnSelectLogFolder);
			this.gbLogs.Controls.Add(this.lblKeepRuns);
			this.gbLogs.Controls.Add(this.numKeepRuns);
			this.gbLogs.Controls.Add(this.lblKeepRunsUnit);
			this.gbLogs.Controls.Add(this.chkLogCopiedFiles);
			this.gbLogs.Location = new System.Drawing.Point(18, 187);
			this.gbLogs.Name = "gbLogs";
			this.gbLogs.Size = new System.Drawing.Size(464, 98);
			this.gbLogs.TabIndex = 20;
			this.gbLogs.TabStop = false;
			this.gbLogs.Text = "Logs";
			// 
			// lblLogFolder
			// 
			this.lblLogFolder.AutoSize = true;
			this.lblLogFolder.Location = new System.Drawing.Point(6, 22);
			this.lblLogFolder.Name = "lblLogFolder";
			this.lblLogFolder.Size = new System.Drawing.Size(39, 13);
			this.lblLogFolder.TabIndex = 0;
			this.lblLogFolder.Text = "Folder:";
			// 
			// txtLogFolder
			// 
			this.txtLogFolder.Location = new System.Drawing.Point(105, 18);
			this.txtLogFolder.Name = "txtLogFolder";
			this.txtLogFolder.Size = new System.Drawing.Size(296, 20);
			this.txtLogFolder.TabIndex = 1;
			// 
			// bnSelectLogFolder
			// 
			this.bnSelectLogFolder.Location = new System.Drawing.Point(407, 17);
			this.bnSelectLogFolder.Name = "bnSelectLogFolder";
			this.bnSelectLogFolder.Size = new System.Drawing.Size(28, 21);
			this.bnSelectLogFolder.TabIndex = 2;
			this.bnSelectLogFolder.Text = "...";
			this.bnSelectLogFolder.UseVisualStyleBackColor = true;
			this.bnSelectLogFolder.Click += new System.EventHandler(this.bnSelectLogFolder_Click);
			// 
			// chkLogCopiedFiles
			// 
			this.chkLogCopiedFiles.AutoSize = true;
			this.chkLogCopiedFiles.Checked = true;
			this.chkLogCopiedFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLogCopiedFiles.Location = new System.Drawing.Point(9, 72);
			this.chkLogCopiedFiles.Name = "chkLogCopiedFiles";
			this.chkLogCopiedFiles.Size = new System.Drawing.Size(300, 17);
			this.chkLogCopiedFiles.TabIndex = 6;
			this.chkLogCopiedFiles.Text = "List every copied file (off: errors, deletions and totals only)";
			this.chkLogCopiedFiles.UseVisualStyleBackColor = true;
			// 
			// lblKeepRuns
			// 
			this.lblKeepRuns.AutoSize = true;
			this.lblKeepRuns.Location = new System.Drawing.Point(6, 48);
			this.lblKeepRuns.Name = "lblKeepRuns";
			this.lblKeepRuns.Size = new System.Drawing.Size(120, 13);
			this.lblKeepRuns.TabIndex = 3;
			this.lblKeepRuns.Text = "Keep the logs of the last";
			// 
			// numKeepRuns
			// 
			this.numKeepRuns.Location = new System.Drawing.Point(150, 45);
			this.numKeepRuns.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.numKeepRuns.Name = "numKeepRuns";
			this.numKeepRuns.Size = new System.Drawing.Size(56, 20);
			this.numKeepRuns.TabIndex = 4;
			this.numKeepRuns.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// lblKeepRunsUnit
			// 
			this.lblKeepRunsUnit.AutoSize = true;
			this.lblKeepRunsUnit.Location = new System.Drawing.Point(212, 48);
			this.lblKeepRunsUnit.Name = "lblKeepRunsUnit";
			this.lblKeepRunsUnit.Size = new System.Drawing.Size(80, 13);
			this.lblKeepRunsUnit.TabIndex = 5;
			this.lblKeepRunsUnit.Text = "runs (0 = keep all)";
			// 
			// lblParallelJobs
			// 
			this.lblParallelJobs.AutoSize = true;
			this.lblParallelJobs.Location = new System.Drawing.Point(300, 51);
			this.lblParallelJobs.Name = "lblParallelJobs";
			this.lblParallelJobs.Size = new System.Drawing.Size(74, 13);
			this.lblParallelJobs.TabIndex = 18;
			this.lblParallelJobs.Text = "Parallel jobs:";
			// 
			// numParallelJobs
			// 
			this.numParallelJobs.Location = new System.Drawing.Point(400, 48);
			this.numParallelJobs.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numParallelJobs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numParallelJobs.Name = "numParallelJobs";
			this.numParallelJobs.Size = new System.Drawing.Size(50, 20);
			this.numParallelJobs.TabIndex = 19;
			this.numParallelJobs.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.chkScan.UseVisualStyleBackColor = true;
			// 
			// txtProgramOnError
			// 
			this.txtProgramOnError.AllowColon = true;
			this.txtProgramOnError.AllowFullStop = true;
			this.txtProgramOnError.BackColor = System.Drawing.SystemColors.Window;
			this.txtProgramOnError.Location = new System.Drawing.Point(105, 18);
			this.txtProgramOnError.Name = "txtProgramOnError";
			this.txtProgramOnError.Size = new System.Drawing.Size(296, 20);
			this.txtProgramOnError.TabIndex = 0;
			this.toolTip.SetToolTip(this.txtProgramOnError, "The program or script to run (without arguments)");
			// 
			// txtProgramAfterBackup
			// 
			this.txtProgramAfterBackup.AllowColon = true;
			this.txtProgramAfterBackup.AllowFullStop = true;
			this.txtProgramAfterBackup.BackColor = System.Drawing.SystemColors.Window;
			this.txtProgramAfterBackup.Location = new System.Drawing.Point(105, 18);
			this.txtProgramAfterBackup.Name = "txtProgramAfterBackup";
			this.txtProgramAfterBackup.Size = new System.Drawing.Size(296, 20);
			this.txtProgramAfterBackup.TabIndex = 0;
			this.toolTip.SetToolTip(this.txtProgramAfterBackup, "The program or script to run (without arguments)");
			// 
			// txtProgramBeforeBackup
			// 
			this.txtProgramBeforeBackup.AllowColon = true;
			this.txtProgramBeforeBackup.AllowFullStop = true;
			this.txtProgramBeforeBackup.BackColor = System.Drawing.SystemColors.Window;
			this.txtProgramBeforeBackup.Location = new System.Drawing.Point(105, 18);
			this.txtProgramBeforeBackup.Name = "txtProgramBeforeBackup";
			this.txtProgramBeforeBackup.Size = new System.Drawing.Size(296, 20);
			this.txtProgramBeforeBackup.TabIndex = 0;
			this.toolTip.SetToolTip(this.txtProgramBeforeBackup, "The program or script to run (without arguments)");
			// 
			// txtWaitBetweenRetries
			// 
			this.txtWaitBetweenRetries.Location = new System.Drawing.Point(203, 48);
			this.txtWaitBetweenRetries.Name = "txtWaitBetweenRetries";
			this.txtWaitBetweenRetries.Size = new System.Drawing.Size(37, 20);
			this.txtWaitBetweenRetries.TabIndex = 1;
			this.txtWaitBetweenRetries.Text = "3";
			this.txtWaitBetweenRetries.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtRetries
			// 
			this.txtRetries.Location = new System.Drawing.Point(203, 22);
			this.txtRetries.Name = "txtRetries";
			this.txtRetries.Size = new System.Drawing.Size(37, 20);
			this.txtRetries.TabIndex = 0;
			this.txtRetries.Text = "3";
			this.txtRetries.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// frmAdvancedSettings
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(500, 653);
			this.Controls.Add(this.lblThreadsHint);
			this.Controls.Add(this.numParallelJobs);
			this.Controls.Add(this.lblParallelJobs);
			this.Controls.Add(this.gbLogs);
			this.Controls.Add(this.numThreads);
			this.Controls.Add(this.lblThreads);
			this.Controls.Add(this.chkLogFiles);
			this.Controls.Add(this.chkScan);
			this.Controls.Add(this.gbProgramOnError);
			this.Controls.Add(this.chkSilentMode);
			this.Controls.Add(this.chkAutoExit2);
			this.Controls.Add(this.chkAutoExit);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.bnOK);
			this.Controls.Add(this.gbProgramAfterBackup);
			this.Controls.Add(this.gbProgramBeforeBackup);
			this.Controls.Add(this.lblSecond);
			this.Controls.Add(this.txtWaitBetweenRetries);
			this.Controls.Add(this.lblWaitBetweenRetries);
			this.Controls.Add(this.txtRetries);
			this.Controls.Add(this.lblRetries);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "frmAdvancedSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Profile options";
			this.Load += new System.EventHandler(this.frmAdvancedSettings_Load);
			this.gbProgramBeforeBackup.ResumeLayout(false);
			this.gbProgramBeforeBackup.PerformLayout();
			this.gbProgramAfterBackup.ResumeLayout(false);
			this.gbProgramAfterBackup.PerformLayout();
			this.gbProgramOnError.ResumeLayout(false);
			this.gbProgramOnError.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numThreads)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numParallelJobs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numKeepRuns)).EndInit();
			this.gbLogs.ResumeLayout(false);
			this.gbLogs.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblRetries;
        private NumericTextBox txtRetries;
        private System.Windows.Forms.Label lblWaitBetweenRetries;
        private NumericTextBox txtWaitBetweenRetries;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.GroupBox gbProgramBeforeBackup;
        private System.Windows.Forms.TextBox txtArgumentsProgramBeforeBackup;
        private System.Windows.Forms.Label lblArguments1;
        private System.Windows.Forms.Label lblProgram1;
        private System.Windows.Forms.Button bnSelectProgramBeforeBackup;
        private PathTextBox txtProgramBeforeBackup;
        private System.Windows.Forms.GroupBox gbProgramAfterBackup;
        private System.Windows.Forms.TextBox txtArgumentsProgramAfterBackup;
        private System.Windows.Forms.Label lblArguments2;
        private System.Windows.Forms.Label lblProgram2;
        private System.Windows.Forms.Button bnSelectProgramAfterBackup;
        private PathTextBox txtProgramAfterBackup;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.CheckBox chkProgramBeforeBackupAdmin;
        private System.Windows.Forms.CheckBox chkStopIfBeforeFails;
        private System.Windows.Forms.CheckBox chkProgramAfterBackupAdmin;
        private System.Windows.Forms.CheckBox chkAutoExit;
        private System.Windows.Forms.CheckBox chkAutoExit2;
        private System.Windows.Forms.CheckBox chkSilentMode;
        private System.Windows.Forms.GroupBox gbProgramOnError;
        private System.Windows.Forms.CheckBox chkProgramOnErrorAdmin;
        private System.Windows.Forms.TextBox txtArgumentsProgramOnError;
        private System.Windows.Forms.Label lblArguments3;
        private System.Windows.Forms.Label lblProgram3;
        private System.Windows.Forms.Button bnSelectProgramOnError;
        private PathTextBox txtProgramOnError;
		private System.Windows.Forms.CheckBox chkScan;
		private System.Windows.Forms.CheckBox chkLogFiles;
		private System.Windows.Forms.Label lblThreads;
		private System.Windows.Forms.NumericUpDown numThreads;
		private System.Windows.Forms.Label lblThreadsHint;
		private System.Windows.Forms.Label lblParallelJobs;
		private System.Windows.Forms.NumericUpDown numParallelJobs;
		private System.Windows.Forms.GroupBox gbLogs;
		private System.Windows.Forms.CheckBox chkLogCopiedFiles;
		private System.Windows.Forms.Label lblLogFolder;
		private System.Windows.Forms.TextBox txtLogFolder;
		private System.Windows.Forms.Button bnSelectLogFolder;
		private System.Windows.Forms.Label lblKeepRuns;
		private System.Windows.Forms.NumericUpDown numKeepRuns;
		private System.Windows.Forms.Label lblKeepRunsUnit;
    }
}