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
    partial class frmSettings : BackupForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
        this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        this.lblTitle = new System.Windows.Forms.Label();
        this.lblSubtitle = new System.Windows.Forms.Label();
        this.bnAddSource = new System.Windows.Forms.Button();
        this.bnEdit = new System.Windows.Forms.Button();
        this.bnDuplicate = new System.Windows.Forms.Button();
        this.bnDeleteSource = new System.Windows.Forms.Button();
        this.bnUp = new System.Windows.Forms.Button();
        this.bnDown = new System.Windows.Forms.Button();
        this.dgvSources = new System.Windows.Forms.DataGridView();
        this.lblEmpty = new System.Windows.Forms.Label();
        this.panel1 = new System.Windows.Forms.Panel();
        this.pnlBarLine = new System.Windows.Forms.Panel();
        this.lblJobCount = new System.Windows.Forms.Label();
        this.btnSettingsCancel = new System.Windows.Forms.Button();
        this.bnBackup = new System.Windows.Forms.Button();
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
        ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).BeginInit();
        this.panel1.SuspendLayout();
        this.menuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // lblTitle
        // 
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTitle.Location = new System.Drawing.Point(16, 40);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(84, 25);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Sync jobs";
        // 
        // lblSubtitle
        // 
        this.lblSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lblSubtitle.AutoEllipsis = true;
        this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
        this.lblSubtitle.Location = new System.Drawing.Point(18, 68);
        this.lblSubtitle.Name = "lblSubtitle";
        this.lblSubtitle.Size = new System.Drawing.Size(820, 18);
        this.lblSubtitle.TabIndex = 1;
        this.lblSubtitle.Text = "Each job copies a source folder to a target folder with robocopy. Double-click a job to edit it.";
        // 
        // bnAddSource
        // 
        this.bnAddSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.bnAddSource.Location = new System.Drawing.Point(20, 100);
        this.bnAddSource.Name = "bnAddSource";
        this.bnAddSource.Size = new System.Drawing.Size(104, 30);
        this.bnAddSource.TabIndex = 2;
        this.bnAddSource.Text = "+  Add job";
        this.bnAddSource.Click += new System.EventHandler(this.bnAddSource_Click);
        // 
        // bnEdit
        // 
        this.bnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.bnEdit.Location = new System.Drawing.Point(130, 100);
        this.bnEdit.Name = "bnEdit";
        this.bnEdit.Size = new System.Drawing.Size(72, 30);
        this.bnEdit.TabIndex = 3;
        this.bnEdit.Text = "Edit";
        this.bnEdit.Click += new System.EventHandler(this.bnEdit_Click);
        // 
        // bnDuplicate
        // 
        this.bnDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.bnDuplicate.Location = new System.Drawing.Point(208, 100);
        this.bnDuplicate.Name = "bnDuplicate";
        this.bnDuplicate.Size = new System.Drawing.Size(88, 30);
        this.bnDuplicate.TabIndex = 4;
        this.bnDuplicate.Text = "Duplicate";
        this.bnDuplicate.Click += new System.EventHandler(this.bnDuplicate_Click);
        // 
        // bnDeleteSource
        // 
        this.bnDeleteSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.bnDeleteSource.Location = new System.Drawing.Point(302, 100);
        this.bnDeleteSource.Name = "bnDeleteSource";
        this.bnDeleteSource.Size = new System.Drawing.Size(80, 30);
        this.bnDeleteSource.TabIndex = 5;
        this.bnDeleteSource.Text = "Remove";
        this.bnDeleteSource.Click += new System.EventHandler(this.bnDeleteSource_Click);
        // 
        // bnUp
        // 
        this.bnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.bnUp.Location = new System.Drawing.Point(388, 100);
        this.bnUp.Name = "bnUp";
        this.bnUp.Size = new System.Drawing.Size(84, 30);
        this.bnUp.TabIndex = 6;
        this.bnUp.Text = "Move up";
        this.bnUp.Click += new System.EventHandler(this.bnUp_Click);
        // 
        // bnDown
        // 
        this.bnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
        this.bnDown.Location = new System.Drawing.Point(478, 100);
        this.bnDown.Name = "bnDown";
        this.bnDown.Size = new System.Drawing.Size(94, 30);
        this.bnDown.TabIndex = 7;
        this.bnDown.Text = "Move down";
        this.bnDown.Click += new System.EventHandler(this.bnDown_Click);
        // 
        // dgvSources
        // 
        this.dgvSources.AllowUserToAddRows = false;
        this.dgvSources.AllowUserToDeleteRows = false;
        this.dgvSources.AllowUserToOrderColumns = true;
        this.dgvSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dgvSources.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvSources.Location = new System.Drawing.Point(20, 142);
        this.dgvSources.MultiSelect = false;
        this.dgvSources.Name = "dgvSources";
        this.dgvSources.ReadOnly = true;
        this.dgvSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvSources.Size = new System.Drawing.Size(820, 330);
        this.dgvSources.TabIndex = 8;
        this.dgvSources.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSources_CellDoubleClick);
        // 
        // lblEmpty
        // 
        this.lblEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.lblEmpty.BackColor = System.Drawing.Color.White;
        this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
        this.lblEmpty.Location = new System.Drawing.Point(21, 177);
        this.lblEmpty.Name = "lblEmpty";
        this.lblEmpty.Size = new System.Drawing.Size(818, 294);
        this.lblEmpty.TabIndex = 9;
        this.lblEmpty.Text = "No sync jobs yet.\r\nClick \"Add job\" to choose a source and target folder.";
        this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblEmpty.Visible = false;
        // 
        // panel1
        // 
        this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
        this.panel1.Controls.Add(this.pnlBarLine);
        this.panel1.Controls.Add(this.lblJobCount);
        this.panel1.Controls.Add(this.btnSettingsCancel);
        this.panel1.Controls.Add(this.bnBackup);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panel1.Location = new System.Drawing.Point(0, 492);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(860, 64);
        this.panel1.TabIndex = 10;
        // 
        // pnlBarLine
        // 
        this.pnlBarLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(222)))));
        this.pnlBarLine.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlBarLine.Location = new System.Drawing.Point(0, 0);
        this.pnlBarLine.Name = "pnlBarLine";
        this.pnlBarLine.Size = new System.Drawing.Size(860, 1);
        this.pnlBarLine.TabIndex = 0;
        // 
        // lblJobCount
        // 
        this.lblJobCount.AutoSize = true;
        this.lblJobCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(100)))));
        this.lblJobCount.Location = new System.Drawing.Point(18, 24);
        this.lblJobCount.Name = "lblJobCount";
        this.lblJobCount.Size = new System.Drawing.Size(40, 15);
        this.lblJobCount.TabIndex = 1;
        this.lblJobCount.Text = "0 jobs";
        // 
        // btnSettingsCancel
        // 
        this.btnSettingsCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnSettingsCancel.Location = new System.Drawing.Point(604, 16);
        this.btnSettingsCancel.Name = "btnSettingsCancel";
        this.btnSettingsCancel.Size = new System.Drawing.Size(100, 32);
        this.btnSettingsCancel.TabIndex = 11;
        this.btnSettingsCancel.Text = "Close";
        this.btnSettingsCancel.Click += new System.EventHandler(this.btnSettingsCancel_Click);
        // 
        // bnBackup
        // 
        this.bnBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.bnBackup.Location = new System.Drawing.Point(712, 16);
        this.bnBackup.Name = "bnBackup";
        this.bnBackup.Size = new System.Drawing.Size(128, 32);
        this.bnBackup.TabIndex = 12;
        this.bnBackup.Tag = "primary";
        this.bnBackup.Text = "Start sync";
        this.bnBackup.Click += new System.EventHandler(this.bnBackup_Click);
        // 
        // menuStrip1
        // 
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
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.White;
        this.ClientSize = new System.Drawing.Size(860, 556);
        this.Controls.Add(this.lblEmpty);
        this.Controls.Add(this.dgvSources);
        this.Controls.Add(this.bnDown);
        this.Controls.Add(this.bnUp);
        this.Controls.Add(this.bnDeleteSource);
        this.Controls.Add(this.bnDuplicate);
        this.Controls.Add(this.bnEdit);
        this.Controls.Add(this.bnAddSource);
        this.Controls.Add(this.lblSubtitle);
        this.Controls.Add(this.lblTitle);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.menuStrip1);
        this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MainMenuStrip = this.menuStrip1;
        this.MinimumSize = new System.Drawing.Size(720, 420);
        this.Name = "frmSettings";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "RoboG";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
        this.Load += new System.EventHandler(this.frmSettings_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).EndInit();
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.Label lblJobCount;
        private System.Windows.Forms.Panel pnlBarLine;
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
