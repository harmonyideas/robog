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
    public partial class frmSettings : BackupForm
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
        this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        this.btnSettingsCancel = new System.Windows.Forms.Button();
        this.bnBackup = new System.Windows.Forms.Button();
        this.bnDeleteSource = new System.Windows.Forms.Button();
        this.bnUp = new System.Windows.Forms.Button();
        this.bnDown = new System.Windows.Forms.Button();
        this.bnAddSource = new System.Windows.Forms.Button();
        this.bnEdit = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.gbBackupSources = new System.Windows.Forms.GroupBox();
        this.dgvSources = new System.Windows.Forms.DataGridView();
        this.bnDuplicate = new System.Windows.Forms.Button();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.newProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.loadProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.saveProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.profileOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.generateScriptFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.panel1.SuspendLayout();
        this.gbBackupSources.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).BeginInit();
        this.menuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // btnSettingsCancel
        // 
        this.btnSettingsCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnSettingsCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.btnSettingsCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnSettingsCancel.Image = global::RoboG.Properties.Resources.button_red_cancel;
        this.btnSettingsCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnSettingsCancel.Location = new System.Drawing.Point(615, 5);
        this.btnSettingsCancel.Name = "btnSettingsCancel";
        this.btnSettingsCancel.Size = new System.Drawing.Size(116, 55);
        this.btnSettingsCancel.TabIndex = 6;
        this.btnSettingsCancel.Text = "CLOSE";
        this.btnSettingsCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.toolTip.SetToolTip(this.btnSettingsCancel, "Start backup");
        this.btnSettingsCancel.UseVisualStyleBackColor = false;
        this.btnSettingsCancel.Click += new System.EventHandler(this.btnSettingsCancel_Click);
        // 
        // bnBackup
        // 
        this.bnBackup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.bnBackup.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.bnBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.bnBackup.Image = global::RoboG.Properties.Resources.button_blue_play;
        this.bnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.bnBackup.Location = new System.Drawing.Point(111, 5);
        this.bnBackup.Name = "bnBackup";
        this.bnBackup.Size = new System.Drawing.Size(116, 55);
        this.bnBackup.TabIndex = 4;
        this.bnBackup.Text = "START";
        this.bnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.toolTip.SetToolTip(this.bnBackup, "Start backup");
        this.bnBackup.UseVisualStyleBackColor = false;
        this.bnBackup.Click += new System.EventHandler(this.bnBackup_Click);
        // 
        // bnDeleteSource
        // 
        this.bnDeleteSource.Image = global::RoboG.Properties.Resources.button_delete_blue;
        this.bnDeleteSource.Location = new System.Drawing.Point(394, 399);
        this.bnDeleteSource.Name = "bnDeleteSource";
        this.bnDeleteSource.Size = new System.Drawing.Size(35, 32);
        this.bnDeleteSource.TabIndex = 3;
        this.toolTip.SetToolTip(this.bnDeleteSource, "Delete backup source");
        this.bnDeleteSource.UseVisualStyleBackColor = true;
        this.bnDeleteSource.Click += new System.EventHandler(this.bnDeleteSource_Click);
        // 
        // bnUp
        // 
        this.bnUp.Image = global::RoboG.Properties.Resources.Up;
        this.bnUp.Location = new System.Drawing.Point(435, 399);
        this.bnUp.Name = "bnUp";
        this.bnUp.Size = new System.Drawing.Size(35, 32);
        this.bnUp.TabIndex = 4;
        this.toolTip.SetToolTip(this.bnUp, "Move backup source up");
        this.bnUp.UseVisualStyleBackColor = true;
        this.bnUp.Click += new System.EventHandler(this.bnUp_Click);
        // 
        // bnDown
        // 
        this.bnDown.Image = global::RoboG.Properties.Resources.Down;
        this.bnDown.Location = new System.Drawing.Point(476, 399);
        this.bnDown.Name = "bnDown";
        this.bnDown.Size = new System.Drawing.Size(35, 32);
        this.bnDown.TabIndex = 5;
        this.toolTip.SetToolTip(this.bnDown, "Move backup source down");
        this.bnDown.UseVisualStyleBackColor = true;
        this.bnDown.Click += new System.EventHandler(this.bnDown_Click);
        // 
        // bnAddSource
        // 
        this.bnAddSource.Image = global::RoboG.Properties.Resources.button_plus_blue;
        this.bnAddSource.Location = new System.Drawing.Point(312, 399);
        this.bnAddSource.Name = "bnAddSource";
        this.bnAddSource.Size = new System.Drawing.Size(35, 32);
        this.bnAddSource.TabIndex = 1;
        this.toolTip.SetToolTip(this.bnAddSource, "Add backup source");
        this.bnAddSource.UseVisualStyleBackColor = true;
        this.bnAddSource.Click += new System.EventHandler(this.bnAddSource_Click);
        // 
        // bnEdit
        // 
        this.bnEdit.Image = global::RoboG.Properties.Resources.Edit;
        this.bnEdit.Location = new System.Drawing.Point(353, 399);
        this.bnEdit.Name = "bnEdit";
        this.bnEdit.Size = new System.Drawing.Size(35, 32);
        this.bnEdit.TabIndex = 2;
        this.toolTip.SetToolTip(this.bnEdit, "Edit backup source");
        this.bnEdit.UseVisualStyleBackColor = true;
        this.bnEdit.Click += new System.EventHandler(this.bnEdit_Click);
        // 
        // panel1
        // 
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.panel1.Controls.Add(this.btnSettingsCancel);
        this.panel1.Controls.Add(this.bnBackup);
        this.panel1.Location = new System.Drawing.Point(-1, 530);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(860, 65);
        this.panel1.TabIndex = 9;
        // 
        // gbBackupSources
        // 
        this.gbBackupSources.Controls.Add(this.bnDeleteSource);
        this.gbBackupSources.Controls.Add(this.dgvSources);
        this.gbBackupSources.Controls.Add(this.bnDuplicate);
        this.gbBackupSources.Controls.Add(this.bnUp);
        this.gbBackupSources.Controls.Add(this.bnDown);
        this.gbBackupSources.Controls.Add(this.bnAddSource);
        this.gbBackupSources.Controls.Add(this.bnEdit);
        this.gbBackupSources.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.gbBackupSources.Location = new System.Drawing.Point(1, 65);
        this.gbBackupSources.Name = "gbBackupSources";
        this.gbBackupSources.Size = new System.Drawing.Size(853, 433);
        this.gbBackupSources.TabIndex = 2;
        this.gbBackupSources.TabStop = false;
        this.gbBackupSources.Text = "Backup sources";
        // 
        // dgvSources
        // 
        this.dgvSources.AllowUserToAddRows = false;
        this.dgvSources.AllowUserToDeleteRows = false;
        this.dgvSources.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSlateGray;
        this.dgvSources.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        this.dgvSources.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvSources.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.dgvSources.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSlateGray;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvSources.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        this.dgvSources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.BackColor = System.Drawing.Color.Transparent;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SlateGray;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dgvSources.DefaultCellStyle = dataGridViewCellStyle3;
        this.dgvSources.Location = new System.Drawing.Point(7, 22);
        this.dgvSources.Name = "dgvSources";
        this.dgvSources.ReadOnly = true;
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSlateGray;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvSources.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        this.dgvSources.RowHeadersVisible = false;
        dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
        dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSlateGray;
        this.dgvSources.RowsDefaultCellStyle = dataGridViewCellStyle5;
        this.dgvSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvSources.Size = new System.Drawing.Size(836, 361);
        this.dgvSources.TabIndex = 1;
        this.dgvSources.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSources_CellDoubleClick);
        // 
        // bnDuplicate
        // 
        this.bnDuplicate.Image = global::RoboG.Properties.Resources.Duplicate;
        this.bnDuplicate.Location = new System.Drawing.Point(517, 399);
        this.bnDuplicate.Name = "bnDuplicate";
        this.bnDuplicate.Size = new System.Drawing.Size(35, 32);
        this.bnDuplicate.TabIndex = 6;
        this.bnDuplicate.UseVisualStyleBackColor = true;
        this.bnDuplicate.Click += new System.EventHandler(this.bnDuplicate_Click);
        // 
        // menuStrip1
        // 
        this.menuStrip1.BackColor = System.Drawing.Color.LightGray;
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(862, 28);
        this.menuStrip1.TabIndex = 10;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProfileToolStripMenuItem,
            this.loadProfileToolStripMenuItem,
            this.saveProfileToolStripMenuItem,
            this.exitToolStripMenuItem});
        this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
        this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
        this.fileToolStripMenuItem.Text = "&File";
        // 
        // newProfileToolStripMenuItem
        // 
        this.newProfileToolStripMenuItem.Name = "newProfileToolStripMenuItem";
        this.newProfileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
        this.newProfileToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
        this.newProfileToolStripMenuItem.Text = "&New Profile";
        this.newProfileToolStripMenuItem.Click += new System.EventHandler(this.newProfileToolStripMenuItem_Click);
        // 
        // loadProfileToolStripMenuItem
        // 
        this.loadProfileToolStripMenuItem.Name = "loadProfileToolStripMenuItem";
        this.loadProfileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
        this.loadProfileToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
        this.loadProfileToolStripMenuItem.Text = "&Load Profile";
        this.loadProfileToolStripMenuItem.Click += new System.EventHandler(this.loadProfileToolStripMenuItem_Click);
        // 
        // saveProfileToolStripMenuItem
        // 
        this.saveProfileToolStripMenuItem.Name = "saveProfileToolStripMenuItem";
        this.saveProfileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
        this.saveProfileToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
        this.saveProfileToolStripMenuItem.Text = "&Save Profile";
        this.saveProfileToolStripMenuItem.Click += new System.EventHandler(this.saveProfileToolStripMenuItem_Click);
        // 
        // exitToolStripMenuItem
        // 
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
        this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
        this.exitToolStripMenuItem.Text = "&Exit";
        // 
        // editToolStripMenuItem
        // 
        this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profileOptionsToolStripMenuItem,
            this.generateScriptFileToolStripMenuItem});
        this.editToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.editToolStripMenuItem.Name = "editToolStripMenuItem";
        this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
        this.editToolStripMenuItem.Text = "Edit";
        // 
        // profileOptionsToolStripMenuItem
        // 
        this.profileOptionsToolStripMenuItem.Name = "profileOptionsToolStripMenuItem";
        this.profileOptionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
        this.profileOptionsToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
        this.profileOptionsToolStripMenuItem.Text = "&Profile Options";
        this.profileOptionsToolStripMenuItem.Click += new System.EventHandler(this.profileOptionsToolStripMenuItem_Click);
        // 
        // generateScriptFileToolStripMenuItem
        // 
        this.generateScriptFileToolStripMenuItem.Name = "generateScriptFileToolStripMenuItem";
        this.generateScriptFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
        this.generateScriptFileToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
        this.generateScriptFileToolStripMenuItem.Text = "&Generate Script File";
        this.generateScriptFileToolStripMenuItem.Click += new System.EventHandler(this.generateScriptFileToolStripMenuItem_Click);
        // 
        // helpToolStripMenuItem
        // 
        this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
        this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
        this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
        this.helpToolStripMenuItem.Text = "&Help";
        // 
        // aboutToolStripMenuItem
        // 
        this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
        this.aboutToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
        this.aboutToolStripMenuItem.Text = "&About RoboG";
        this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
        // 
        // frmSettings
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(862, 608);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.gbBackupSources);
        this.Controls.Add(this.menuStrip1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MainMenuStrip = this.menuStrip1;
        this.MaximizeBox = false;
        this.Name = "frmSettings";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "RoboG";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
        this.Load += new System.EventHandler(this.frmSettings_Load);
        this.panel1.ResumeLayout(false);
        this.gbBackupSources.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).EndInit();
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox gbBackupSources;
        private System.Windows.Forms.Button bnDeleteSource;
        private System.Windows.Forms.Button bnAddSource;
        private System.Windows.Forms.Button bnDown;
        private System.Windows.Forms.Button bnUp;
        private System.Windows.Forms.Button bnBackup;
        private System.Windows.Forms.Button bnEdit;
        private System.Windows.Forms.Button bnDuplicate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSettingsCancel;
        private System.Windows.Forms.DataGridView dgvSources;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileOptionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateScriptFileToolStripMenuItem;
    }
}