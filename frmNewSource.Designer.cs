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
    public partial class frmNewSource : System.Windows.Forms.Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewSource));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.lblDirectory = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblTargetDirectory = new System.Windows.Forms.Label();
			this.txtSourceDir = new System.Windows.Forms.TextBox();
			this.txtTargetDir = new System.Windows.Forms.TextBox();
			this.chkShadowCopy = new System.Windows.Forms.CheckBox();
			this.bnSelectDirectory = new System.Windows.Forms.Button();
			this.bnOK = new System.Windows.Forms.Button();
			this.bnCancel = new System.Windows.Forms.Button();
			this.bnAdvanced = new System.Windows.Forms.Button();
			this.btnTargetdir = new System.Windows.Forms.Button();
			this.chkLogOnly = new System.Windows.Forms.CheckBox();
			this.chkDisableJob = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblDirectory
			// 
			this.lblDirectory.AutoSize = true;
			this.lblDirectory.Location = new System.Drawing.Point(9, 41);
			this.lblDirectory.Name = "lblDirectory";
			this.lblDirectory.Size = new System.Drawing.Size(60, 13);
			this.lblDirectory.TabIndex = 8;
			this.lblDirectory.Text = "Source Dir:";
			this.toolTip.SetToolTip(this.lblDirectory, "The directory to backup");
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(9, 15);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(38, 13);
			this.lblName.TabIndex = 14;
			this.lblName.Text = "Name:";
			this.toolTip.SetToolTip(this.lblName, "Name of the backup source. This will be used as the folder name on the backup dri" +
					"ve.");
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.SystemColors.Window;
			this.txtName.Location = new System.Drawing.Point(103, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(339, 20);
			this.txtName.TabIndex = 0;
			this.toolTip.SetToolTip(this.txtName, "Name of the backup source. This will be used as the folder name on the backup dri" +
					"ve.");
			this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
			// 
			// lblTargetDirectory
			// 
			this.lblTargetDirectory.AutoSize = true;
			this.lblTargetDirectory.Location = new System.Drawing.Point(9, 67);
			this.lblTargetDirectory.Name = "lblTargetDirectory";
			this.lblTargetDirectory.Size = new System.Drawing.Size(66, 13);
			this.lblTargetDirectory.TabIndex = 18;
			this.lblTargetDirectory.Text = "Target Path:";
			this.toolTip.SetToolTip(this.lblTargetDirectory, "The directory to backup");
			// 
			// txtSourceDir
			// 
			this.txtSourceDir.BackColor = System.Drawing.SystemColors.Window;
			this.txtSourceDir.Location = new System.Drawing.Point(103, 38);
			this.txtSourceDir.Name = "txtSourceDir";
			this.txtSourceDir.Size = new System.Drawing.Size(339, 20);
			this.txtSourceDir.TabIndex = 1;
			this.toolTip.SetToolTip(this.txtSourceDir, "Source directory to be backed up");
			// 
			// txtTargetDir
			// 
			this.txtTargetDir.BackColor = System.Drawing.SystemColors.Window;
			this.txtTargetDir.Location = new System.Drawing.Point(103, 64);
			this.txtTargetDir.Name = "txtTargetDir";
			this.txtTargetDir.Size = new System.Drawing.Size(339, 20);
			this.txtTargetDir.TabIndex = 3;
			this.toolTip.SetToolTip(this.txtTargetDir, "Name of the backup source. This will be used as the folder name on the backup dri" +
					"ve.");
			// 
			// chkShadowCopy
			// 
			this.chkShadowCopy.Location = new System.Drawing.Point(12, 95);
			this.chkShadowCopy.Name = "chkShadowCopy";
			this.chkShadowCopy.Size = new System.Drawing.Size(303, 21);
			this.chkShadowCopy.TabIndex = 5;
			this.chkShadowCopy.Text = "Use shadow copies (to backup files in use, recommended)";
			this.chkShadowCopy.UseVisualStyleBackColor = true;
			this.chkShadowCopy.CheckedChanged += new System.EventHandler(this.chkShadowCopy_CheckedChanged);
			// 
			// bnSelectDirectory
			// 
			this.bnSelectDirectory.Location = new System.Drawing.Point(448, 38);
			this.bnSelectDirectory.Name = "bnSelectDirectory";
			this.bnSelectDirectory.Size = new System.Drawing.Size(28, 21);
			this.bnSelectDirectory.TabIndex = 2;
			this.bnSelectDirectory.Text = "...";
			this.bnSelectDirectory.UseVisualStyleBackColor = true;
			this.bnSelectDirectory.Click += new System.EventHandler(this.bnSelectDirectory_Click);
			// 
			// bnOK
			// 
			this.bnOK.Location = new System.Drawing.Point(154, 181);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(76, 28);
			this.bnOK.TabIndex = 8;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// bnCancel
			// 
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(293, 181);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(76, 28);
			this.bnCancel.TabIndex = 9;
			this.bnCancel.Text = "Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
			// 
			// bnAdvanced
			// 
			this.bnAdvanced.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnAdvanced.Location = new System.Drawing.Point(366, 90);
			this.bnAdvanced.Name = "bnAdvanced";
			this.bnAdvanced.Size = new System.Drawing.Size(76, 28);
			this.bnAdvanced.TabIndex = 8;
			this.bnAdvanced.Text = "Advanced";
			this.bnAdvanced.UseVisualStyleBackColor = true;
			this.bnAdvanced.Click += new System.EventHandler(this.bnAdvanced_Click);
			// 
			// btnTargetdir
			// 
			this.btnTargetdir.Location = new System.Drawing.Point(448, 64);
			this.btnTargetdir.Name = "btnTargetdir";
			this.btnTargetdir.Size = new System.Drawing.Size(28, 21);
			this.btnTargetdir.TabIndex = 4;
			this.btnTargetdir.Text = "...";
			this.btnTargetdir.UseVisualStyleBackColor = true;
			this.btnTargetdir.Click += new System.EventHandler(this.btnTargetdir_Click);
			// 
			// chkLogOnly
			// 
			this.chkLogOnly.Checked = true;
			this.chkLogOnly.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLogOnly.Location = new System.Drawing.Point(12, 141);
			this.chkLogOnly.Name = "chkLogOnly";
			this.chkLogOnly.Size = new System.Drawing.Size(303, 21);
			this.chkLogOnly.TabIndex = 7;
			this.chkLogOnly.Text = "Use \"Log Only\" option - no changes will be made";
			this.chkLogOnly.UseVisualStyleBackColor = true;
			// 
			// chkDisableJob
			// 
			this.chkDisableJob.Location = new System.Drawing.Point(12, 117);
			this.chkDisableJob.Name = "chkDisableJob";
			this.chkDisableJob.Size = new System.Drawing.Size(303, 21);
			this.chkDisableJob.TabIndex = 6;
			this.chkDisableJob.Text = "Disable job (sets job to be inactive for current profile";
			this.chkDisableJob.UseVisualStyleBackColor = true;
			// 
			// frmNewSource
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(519, 217);
			this.Controls.Add(this.txtTargetDir);
			this.Controls.Add(this.txtSourceDir);
			this.Controls.Add(this.chkDisableJob);
			this.Controls.Add(this.chkLogOnly);
			this.Controls.Add(this.btnTargetdir);
			this.Controls.Add(this.lblTargetDirectory);
			this.Controls.Add(this.bnAdvanced);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.bnOK);
			this.Controls.Add(this.bnSelectDirectory);
			this.Controls.Add(this.lblDirectory);
			this.Controls.Add(this.chkShadowCopy);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewSource";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New source";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewSource_FormClosing);
			this.Load += new System.EventHandler(this.frmNewSource_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkShadowCopy;
        private System.Windows.Forms.TextBox txtSourceDir;
        private System.Windows.Forms.TextBox txtTargetDir;
       
        private System.Windows.Forms.Label lblDirectory;
        private System.Windows.Forms.Button bnSelectDirectory;
        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button bnAdvanced;
        private System.Windows.Forms.Button btnTargetdir;
        private System.Windows.Forms.Label lblTargetDirectory;
        private System.Windows.Forms.CheckBox chkLogOnly;
        private System.Windows.Forms.CheckBox chkDisableJob;
        //private PathTextBox txtSourceDir;
        //private PathTextBox txtTargetDir;
    }
}