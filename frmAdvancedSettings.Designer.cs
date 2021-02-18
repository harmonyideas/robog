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
			this.gbProgramBeforeBackup.Controls.Add(this.txtArgumentsProgramBeforeBackup);
			this.gbProgramBeforeBackup.Controls.Add(this.lblArguments1);
			this.gbProgramBeforeBackup.Controls.Add(this.lblProgram1);
			this.gbProgramBeforeBackup.Controls.Add(this.bnSelectProgramBeforeBackup);
			this.gbProgramBeforeBackup.Controls.Add(this.txtProgramBeforeBackup);
			this.gbProgramBeforeBackup.Location = new System.Drawing.Point(18, 187);
			this.gbProgramBeforeBackup.Name = "gbProgramBeforeBackup";
			this.gbProgramBeforeBackup.Size = new System.Drawing.Size(444, 94);
			this.gbProgramBeforeBackup.TabIndex = 3;
			this.gbProgramBeforeBackup.TabStop = false;
			this.gbProgramBeforeBackup.Text = "Run program or script before backup";
			// 
			// chkProgramBeforeBackupAdmin
			// 
			this.chkProgramBeforeBackupAdmin.AutoSize = true;
			this.chkProgramBeforeBackupAdmin.Location = new System.Drawing.Point(315, 70);
			this.chkProgramBeforeBackupAdmin.Name = "chkProgramBeforeBackupAdmin";
			this.chkProgramBeforeBackupAdmin.Size = new System.Drawing.Size(124, 17);
			this.chkProgramBeforeBackupAdmin.TabIndex = 2;
			this.chkProgramBeforeBackupAdmin.Text = "Start as administrator";
			this.chkProgramBeforeBackupAdmin.UseVisualStyleBackColor = true;
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
			this.lblProgram1.Location = new System.Drawing.Point(6, -32);
			this.lblProgram1.Name = "lblProgram1";
			this.lblProgram1.Size = new System.Drawing.Size(81, 13);
			this.lblProgram1.TabIndex = 5;
			this.lblProgram1.Text = "Program/Script:";
			// 
			// bnSelectProgramBeforeBackup
			// 
			this.bnSelectProgramBeforeBackup.Location = new System.Drawing.Point(407, -36);
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
			this.gbProgramAfterBackup.Location = new System.Drawing.Point(18, 280);
			this.gbProgramAfterBackup.Name = "gbProgramAfterBackup";
			this.gbProgramAfterBackup.Size = new System.Drawing.Size(444, 94);
			this.gbProgramAfterBackup.TabIndex = 4;
			this.gbProgramAfterBackup.TabStop = false;
			this.gbProgramAfterBackup.Text = "Run program or script after backup";
			// 
			// chkProgramAfterBackupAdmin
			// 
			this.chkProgramAfterBackupAdmin.AutoSize = true;
			this.chkProgramAfterBackupAdmin.Location = new System.Drawing.Point(315, 70);
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
			this.lblProgram2.Location = new System.Drawing.Point(6, -32);
			this.lblProgram2.Name = "lblProgram2";
			this.lblProgram2.Size = new System.Drawing.Size(81, 13);
			this.lblProgram2.TabIndex = 5;
			this.lblProgram2.Text = "Program/Script:";
			// 
			// bnSelectProgramAfterBackup
			// 
			this.bnSelectProgramAfterBackup.Location = new System.Drawing.Point(407, -36);
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
			this.bnCancel.Location = new System.Drawing.Point(243, 480);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(76, 28);
			this.bnCancel.TabIndex = 6;
			this.bnCancel.Text = "Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			// 
			// bnOK
			// 
			this.bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.bnOK.Location = new System.Drawing.Point(161, 480);
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
			this.gbProgramOnError.Location = new System.Drawing.Point(18, 380);
			this.gbProgramOnError.Name = "gbProgramOnError";
			this.gbProgramOnError.Size = new System.Drawing.Size(444, 94);
			this.gbProgramOnError.TabIndex = 12;
			this.gbProgramOnError.TabStop = false;
			this.gbProgramOnError.Text = "Run program or script on error";
			// 
			// chkProgramOnErrorAdmin
			// 
			this.chkProgramOnErrorAdmin.AutoSize = true;
			this.chkProgramOnErrorAdmin.Location = new System.Drawing.Point(315, 71);
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
			this.lblProgram3.Location = new System.Drawing.Point(6, -32);
			this.lblProgram3.Name = "lblProgram3";
			this.lblProgram3.Size = new System.Drawing.Size(81, 13);
			this.lblProgram3.TabIndex = 5;
			this.lblProgram3.Text = "Program/Script:";
			// 
			// bnSelectProgramOnError
			// 
			this.bnSelectProgramOnError.Location = new System.Drawing.Point(407, -36);
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
			this.chkScan.Text = "Scan directories to synchronize. (Turn off for large file sets)\r\n";
			this.chkScan.UseVisualStyleBackColor = true;
			// 
			// txtProgramOnError
			// 
			this.txtProgramOnError.AllowColon = true;
			this.txtProgramOnError.AllowFullStop = true;
			this.txtProgramOnError.BackColor = System.Drawing.SystemColors.Window;
			this.txtProgramOnError.Location = new System.Drawing.Point(105, -35);
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
			this.txtProgramAfterBackup.Location = new System.Drawing.Point(105, -35);
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
			this.txtProgramBeforeBackup.Location = new System.Drawing.Point(105, -35);
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
			this.ClientSize = new System.Drawing.Size(480, 549);
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
			this.Name = "frmAdvancedSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Advanced Settings";
			this.Load += new System.EventHandler(this.frmAdvancedSettings_Load);
			this.gbProgramBeforeBackup.ResumeLayout(false);
			this.gbProgramBeforeBackup.PerformLayout();
			this.gbProgramAfterBackup.ResumeLayout(false);
			this.gbProgramAfterBackup.PerformLayout();
			this.gbProgramOnError.ResumeLayout(false);
			this.gbProgramOnError.PerformLayout();
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
    }
}