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
    public partial class frmNewSourceAdvanced : System.Windows.Forms.Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewSourceAdvanced));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.bnDeleteExcludedFolder = new System.Windows.Forms.Button();
			this.bnExcludeFolder = new System.Windows.Forms.Button();
			this.bnDeleteExcludedFile = new System.Windows.Forms.Button();
			this.bnExcluceFile = new System.Windows.Forms.Button();
			this.bnMINLAD = new System.Windows.Forms.Button();
			this.lblMINLAD = new System.Windows.Forms.Label();
			this.bnMAXLAD = new System.Windows.Forms.Button();
			this.lblMAXLAD = new System.Windows.Forms.Label();
			this.bnMINAGE = new System.Windows.Forms.Button();
			this.lblMINAGE = new System.Windows.Forms.Label();
			this.bnMAXAGE = new System.Windows.Forms.Button();
			this.lblMAXAGE = new System.Windows.Forms.Label();
			this.chkM = new System.Windows.Forms.CheckBox();
			this.lblBInfo1 = new System.Windows.Forms.Label();
			this.chkB = new System.Windows.Forms.CheckBox();
			this.lstExcludedFiles = new System.Windows.Forms.ListBox();
			this.gbExcludeFiles = new System.Windows.Forms.GroupBox();
			this.lblLADINFO = new System.Windows.Forms.Label();
			this.lblMINFormattedSize = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtMIN = new RoboG.NumericTextBox();
			this.lblMIN = new System.Windows.Forms.Label();
			this.lblMAXFormattedSize = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMAX = new RoboG.NumericTextBox();
			this.lblMAX = new System.Windows.Forms.Label();
			this.chkIT = new System.Windows.Forms.CheckBox();
			this.chkXL = new System.Windows.Forms.CheckBox();
			this.chkXN = new System.Windows.Forms.CheckBox();
			this.chkXC = new System.Windows.Forms.CheckBox();
			this.gbExcludeFolders = new System.Windows.Forms.GroupBox();
			this.lstExcludedFolders = new System.Windows.Forms.ListBox();
			this.bnOK = new System.Windows.Forms.Button();
			this.bnCancel = new System.Windows.Forms.Button();
			this.gbAttributes = new System.Windows.Forms.GroupBox();
			this.gbRemoveAttributes = new System.Windows.Forms.GroupBox();
			this.chkAME = new System.Windows.Forms.CheckBox();
			this.chkAMT = new System.Windows.Forms.CheckBox();
			this.chkAMN = new System.Windows.Forms.CheckBox();
			this.chkAMC = new System.Windows.Forms.CheckBox();
			this.chkAMH = new System.Windows.Forms.CheckBox();
			this.chkAMS = new System.Windows.Forms.CheckBox();
			this.chkAMA = new System.Windows.Forms.CheckBox();
			this.chkAMR = new System.Windows.Forms.CheckBox();
			this.gbAddAttributes = new System.Windows.Forms.GroupBox();
			this.chkAPE = new System.Windows.Forms.CheckBox();
			this.chkAPN = new System.Windows.Forms.CheckBox();
			this.chkAPC = new System.Windows.Forms.CheckBox();
			this.chkAPH = new System.Windows.Forms.CheckBox();
			this.chkAPS = new System.Windows.Forms.CheckBox();
			this.chkAPA = new System.Windows.Forms.CheckBox();
			this.chkAPR = new System.Windows.Forms.CheckBox();
			this.gbExcludeAttributes = new System.Windows.Forms.GroupBox();
			this.chkXAE = new System.Windows.Forms.CheckBox();
			this.chkXAT = new System.Windows.Forms.CheckBox();
			this.chkXAN = new System.Windows.Forms.CheckBox();
			this.chkXAC = new System.Windows.Forms.CheckBox();
			this.chkXAO = new System.Windows.Forms.CheckBox();
			this.chkXAH = new System.Windows.Forms.CheckBox();
			this.chkXAS = new System.Windows.Forms.CheckBox();
			this.chkXAA = new System.Windows.Forms.CheckBox();
			this.chkXAR = new System.Windows.Forms.CheckBox();
			this.gbIncludeAttributes = new System.Windows.Forms.GroupBox();
			this.chkIAE = new System.Windows.Forms.CheckBox();
			this.chkIAT = new System.Windows.Forms.CheckBox();
			this.chkIAN = new System.Windows.Forms.CheckBox();
			this.chkIAC = new System.Windows.Forms.CheckBox();
			this.chkIAO = new System.Windows.Forms.CheckBox();
			this.chkIAH = new System.Windows.Forms.CheckBox();
			this.chkIAS = new System.Windows.Forms.CheckBox();
			this.chkIAA = new System.Windows.Forms.CheckBox();
			this.chkIAR = new System.Windows.Forms.CheckBox();
			this.gbFileOptions = new System.Windows.Forms.GroupBox();
			this.chkMIR = new System.Windows.Forms.CheckBox();
			this.chkCOPY_S = new System.Windows.Forms.CheckBox();
			this.lblBInfo2 = new System.Windows.Forms.Label();
			this.chkCOPY_U = new System.Windows.Forms.CheckBox();
			this.chkCOPY_O = new System.Windows.Forms.CheckBox();
			this.gbExcludeFiles.SuspendLayout();
			this.gbExcludeFolders.SuspendLayout();
			this.gbAttributes.SuspendLayout();
			this.gbRemoveAttributes.SuspendLayout();
			this.gbAddAttributes.SuspendLayout();
			this.gbExcludeAttributes.SuspendLayout();
			this.gbIncludeAttributes.SuspendLayout();
			this.gbFileOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// bnDeleteExcludedFolder
			// 
			this.bnDeleteExcludedFolder.Image = global::RoboG.Properties.Resources.button_delete_blue;
			this.bnDeleteExcludedFolder.Location = new System.Drawing.Point(47, 172);
			this.bnDeleteExcludedFolder.Name = "bnDeleteExcludedFolder";
			this.bnDeleteExcludedFolder.Size = new System.Drawing.Size(35, 32);
			this.bnDeleteExcludedFolder.TabIndex = 2;
			this.toolTip.SetToolTip(this.bnDeleteExcludedFolder, "Remove excluded folder");
			this.bnDeleteExcludedFolder.UseVisualStyleBackColor = true;
			this.bnDeleteExcludedFolder.Click += new System.EventHandler(this.bnDeleteExcludedFolder_Click);
			// 
			// bnExcludeFolder
			// 
			this.bnExcludeFolder.Image = global::RoboG.Properties.Resources.button_plus_blue;
			this.bnExcludeFolder.Location = new System.Drawing.Point(6, 172);
			this.bnExcludeFolder.Name = "bnExcludeFolder";
			this.bnExcludeFolder.Size = new System.Drawing.Size(35, 32);
			this.bnExcludeFolder.TabIndex = 1;
			this.toolTip.SetToolTip(this.bnExcludeFolder, "Add folder to exclude");
			this.bnExcludeFolder.UseVisualStyleBackColor = true;
			this.bnExcludeFolder.Click += new System.EventHandler(this.bnExcludeFolder_Click);
			// 
			// bnDeleteExcludedFile
			// 
			this.bnDeleteExcludedFile.Image = global::RoboG.Properties.Resources.button_delete_blue;
			this.bnDeleteExcludedFile.Location = new System.Drawing.Point(47, 172);
			this.bnDeleteExcludedFile.Name = "bnDeleteExcludedFile";
			this.bnDeleteExcludedFile.Size = new System.Drawing.Size(35, 32);
			this.bnDeleteExcludedFile.TabIndex = 2;
			this.toolTip.SetToolTip(this.bnDeleteExcludedFile, "Remove excluded file");
			this.bnDeleteExcludedFile.UseVisualStyleBackColor = true;
			this.bnDeleteExcludedFile.Click += new System.EventHandler(this.bnDeleteExcludedFile_Click);
			// 
			// bnExcluceFile
			// 
			this.bnExcluceFile.Image = global::RoboG.Properties.Resources.button_plus_blue;
			this.bnExcluceFile.Location = new System.Drawing.Point(6, 172);
			this.bnExcluceFile.Name = "bnExcluceFile";
			this.bnExcluceFile.Size = new System.Drawing.Size(35, 32);
			this.bnExcluceFile.TabIndex = 1;
			this.toolTip.SetToolTip(this.bnExcluceFile, "Add file to exlude");
			this.bnExcluceFile.UseVisualStyleBackColor = true;
			this.bnExcluceFile.Click += new System.EventHandler(this.bnExcludeFile_Click);
			// 
			// bnMINLAD
			// 
			this.bnMINLAD.Location = new System.Drawing.Point(179, 337);
			this.bnMINLAD.Name = "bnMINLAD";
			this.bnMINLAD.Size = new System.Drawing.Size(91, 22);
			this.bnMINLAD.TabIndex = 8;
			this.bnMINLAD.Tag = -1;
			this.bnMINLAD.Text = "Click to set";
			this.toolTip.SetToolTip(this.bnMINLAD, "Excludes files with a Last Access Date newer than n days or specified date.");
			this.bnMINLAD.UseVisualStyleBackColor = true;
			this.bnMINLAD.Click += new System.EventHandler(this.bnMINLAD_Click);
			// 
			// lblMINLAD
			// 
			this.lblMINLAD.AutoSize = true;
			this.lblMINLAD.Location = new System.Drawing.Point(6, 341);
			this.lblMINLAD.Name = "lblMINLAD";
			this.lblMINLAD.Size = new System.Drawing.Size(120, 13);
			this.lblMINLAD.TabIndex = 39;
			this.lblMINLAD.Text = "Exclude files used since";
			this.toolTip.SetToolTip(this.lblMINLAD, "Excludes files with a Last Access Date newer than n days or specified date.");
			// 
			// bnMAXLAD
			// 
			this.bnMAXLAD.Location = new System.Drawing.Point(179, 311);
			this.bnMAXLAD.Name = "bnMAXLAD";
			this.bnMAXLAD.Size = new System.Drawing.Size(91, 22);
			this.bnMAXLAD.TabIndex = 7;
			this.bnMAXLAD.Tag = -1;
			this.bnMAXLAD.Text = "Click to set";
			this.toolTip.SetToolTip(this.bnMAXLAD, "Excludes files with a Last Access Date older than n days or specified date.");
			this.bnMAXLAD.UseVisualStyleBackColor = true;
			this.bnMAXLAD.Click += new System.EventHandler(this.bnMAXLAD_Click);
			// 
			// lblMAXLAD
			// 
			this.lblMAXLAD.AutoSize = true;
			this.lblMAXLAD.Location = new System.Drawing.Point(6, 315);
			this.lblMAXLAD.Name = "lblMAXLAD";
			this.lblMAXLAD.Size = new System.Drawing.Size(132, 13);
			this.lblMAXLAD.TabIndex = 37;
			this.lblMAXLAD.Text = "Exclude files unused since";
			this.toolTip.SetToolTip(this.lblMAXLAD, "Excludes files with a Last Access Date older than n days or specified date.");
			// 
			// bnMINAGE
			// 
			this.bnMINAGE.Location = new System.Drawing.Point(179, 285);
			this.bnMINAGE.Name = "bnMINAGE";
			this.bnMINAGE.Size = new System.Drawing.Size(91, 22);
			this.bnMINAGE.TabIndex = 6;
			this.bnMINAGE.Tag = -1;
			this.bnMINAGE.Text = "Click to set";
			this.toolTip.SetToolTip(this.bnMINAGE, "Excludes files with a Last Modified Date newer than n days or specified date.");
			this.bnMINAGE.UseVisualStyleBackColor = true;
			this.bnMINAGE.Click += new System.EventHandler(this.bnMINAGE_Click);
			// 
			// lblMINAGE
			// 
			this.lblMINAGE.AutoSize = true;
			this.lblMINAGE.Location = new System.Drawing.Point(6, 289);
			this.lblMINAGE.Name = "lblMINAGE";
			this.lblMINAGE.Size = new System.Drawing.Size(122, 13);
			this.lblMINAGE.TabIndex = 35;
			this.lblMINAGE.Text = "Exclude files newer than";
			this.toolTip.SetToolTip(this.lblMINAGE, "Excludes files with a Last Modified Date newer than n days or specified date.");
			// 
			// bnMAXAGE
			// 
			this.bnMAXAGE.Location = new System.Drawing.Point(179, 259);
			this.bnMAXAGE.Name = "bnMAXAGE";
			this.bnMAXAGE.Size = new System.Drawing.Size(91, 22);
			this.bnMAXAGE.TabIndex = 5;
			this.bnMAXAGE.Tag = -1;
			this.bnMAXAGE.Text = "Click to set";
			this.toolTip.SetToolTip(this.bnMAXAGE, "Exludes files with a Last Modified Date older than n days or specified date.");
			this.bnMAXAGE.UseVisualStyleBackColor = true;
			this.bnMAXAGE.Click += new System.EventHandler(this.bnMAXAGE_Click);
			// 
			// lblMAXAGE
			// 
			this.lblMAXAGE.AutoSize = true;
			this.lblMAXAGE.Location = new System.Drawing.Point(6, 263);
			this.lblMAXAGE.Name = "lblMAXAGE";
			this.lblMAXAGE.Size = new System.Drawing.Size(116, 13);
			this.lblMAXAGE.TabIndex = 33;
			this.lblMAXAGE.Text = "Exclude files older than";
			this.toolTip.SetToolTip(this.lblMAXAGE, "Exludes files with a Last Modified Date older than n days or specified date.");
			// 
			// chkM
			// 
			this.chkM.AutoSize = true;
			this.chkM.Location = new System.Drawing.Point(12, 91);
			this.chkM.Name = "chkM";
			this.chkM.Size = new System.Drawing.Size(297, 17);
			this.chkM.TabIndex = 2;
			this.chkM.Text = "Backup only files with the archive attribute set and reset it";
			this.toolTip.SetToolTip(this.chkM, "Can only be used when shadow copies are disabled, because shadow copies are read " +
					"only.");
			this.chkM.UseVisualStyleBackColor = true;
			// 
			// lblBInfo1
			// 
			this.lblBInfo1.AutoSize = true;
			this.lblBInfo1.Location = new System.Drawing.Point(255, 104);
			this.lblBInfo1.Name = "lblBInfo1";
			this.lblBInfo1.Size = new System.Drawing.Size(192, 13);
			this.lblBInfo1.TabIndex = 8;
			this.lblBInfo1.Text = "(requires \"back up files and directories\"";
			this.toolTip.SetToolTip(this.lblBInfo1, "Backup files you otherwise wouldn\'t have access to. To use this, make the backup " +
					"user a member of the group \"Backup Operators\".");
			// 
			// chkB
			// 
			this.chkB.AutoSize = true;
			this.chkB.Location = new System.Drawing.Point(239, 87);
			this.chkB.Name = "chkB";
			this.chkB.Size = new System.Drawing.Size(123, 17);
			this.chkB.TabIndex = 7;
			this.chkB.Text = "Use \"backup mode\"";
			this.toolTip.SetToolTip(this.chkB, "Backup files you otherwise wouldn\'t have access to. To use this, make the backup " +
					"user a member of the group \"Backup Operators\".");
			this.chkB.UseVisualStyleBackColor = true;
			// 
			// lstExcludedFiles
			// 
			this.lstExcludedFiles.FormattingEnabled = true;
			this.lstExcludedFiles.Location = new System.Drawing.Point(6, 19);
			this.lstExcludedFiles.Name = "lstExcludedFiles";
			this.lstExcludedFiles.Size = new System.Drawing.Size(441, 147);
			this.lstExcludedFiles.Sorted = true;
			this.lstExcludedFiles.TabIndex = 0;
			// 
			// gbExcludeFiles
			// 
			this.gbExcludeFiles.Controls.Add(this.lblLADINFO);
			this.gbExcludeFiles.Controls.Add(this.bnMINLAD);
			this.gbExcludeFiles.Controls.Add(this.lblMINLAD);
			this.gbExcludeFiles.Controls.Add(this.bnMAXLAD);
			this.gbExcludeFiles.Controls.Add(this.lblMAXLAD);
			this.gbExcludeFiles.Controls.Add(this.bnMINAGE);
			this.gbExcludeFiles.Controls.Add(this.lblMINAGE);
			this.gbExcludeFiles.Controls.Add(this.bnMAXAGE);
			this.gbExcludeFiles.Controls.Add(this.lblMAXAGE);
			this.gbExcludeFiles.Controls.Add(this.lblMINFormattedSize);
			this.gbExcludeFiles.Controls.Add(this.label4);
			this.gbExcludeFiles.Controls.Add(this.txtMIN);
			this.gbExcludeFiles.Controls.Add(this.lblMIN);
			this.gbExcludeFiles.Controls.Add(this.lblMAXFormattedSize);
			this.gbExcludeFiles.Controls.Add(this.label2);
			this.gbExcludeFiles.Controls.Add(this.txtMAX);
			this.gbExcludeFiles.Controls.Add(this.lblMAX);
			this.gbExcludeFiles.Controls.Add(this.bnDeleteExcludedFile);
			this.gbExcludeFiles.Controls.Add(this.bnExcluceFile);
			this.gbExcludeFiles.Controls.Add(this.lstExcludedFiles);
			this.gbExcludeFiles.Location = new System.Drawing.Point(12, 207);
			this.gbExcludeFiles.Name = "gbExcludeFiles";
			this.gbExcludeFiles.Size = new System.Drawing.Size(453, 387);
			this.gbExcludeFiles.TabIndex = 1;
			this.gbExcludeFiles.TabStop = false;
			this.gbExcludeFiles.Text = "Exclude files";
			// 
			// lblLADINFO
			// 
			this.lblLADINFO.Location = new System.Drawing.Point(276, 315);
			this.lblLADINFO.Name = "lblLADINFO";
			this.lblLADINFO.Size = new System.Drawing.Size(142, 52);
			this.lblLADINFO.TabIndex = 40;
			this.lblLADINFO.Text = "These two options evaluate \"Last Access Date\". Be careful using it.";
			// 
			// lblMINFormattedSize
			// 
			this.lblMINFormattedSize.AutoSize = true;
			this.lblMINFormattedSize.Location = new System.Drawing.Point(338, 237);
			this.lblMINFormattedSize.Name = "lblMINFormattedSize";
			this.lblMINFormattedSize.Size = new System.Drawing.Size(19, 13);
			this.lblMINFormattedSize.TabIndex = 31;
			this.lblMINFormattedSize.Text = "    ";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(299, 237);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 13);
			this.label4.TabIndex = 30;
			this.label4.Text = "Bytes";
			// 
			// txtMIN
			// 
			this.txtMIN.Location = new System.Drawing.Point(179, 234);
			this.txtMIN.MaxLength = 18;
			this.txtMIN.Name = "txtMIN";
			this.txtMIN.Size = new System.Drawing.Size(114, 20);
			this.txtMIN.TabIndex = 4;
			this.txtMIN.TextChanged += new System.EventHandler(this.txtMIN_TextChanged);
			// 
			// lblMIN
			// 
			this.lblMIN.AutoSize = true;
			this.lblMIN.Location = new System.Drawing.Point(6, 237);
			this.lblMIN.Name = "lblMIN";
			this.lblMIN.Size = new System.Drawing.Size(125, 13);
			this.lblMIN.TabIndex = 28;
			this.lblMIN.Text = "Exclude files smaller than";
			// 
			// lblMAXFormattedSize
			// 
			this.lblMAXFormattedSize.AutoSize = true;
			this.lblMAXFormattedSize.Location = new System.Drawing.Point(338, 211);
			this.lblMAXFormattedSize.Name = "lblMAXFormattedSize";
			this.lblMAXFormattedSize.Size = new System.Drawing.Size(19, 13);
			this.lblMAXFormattedSize.TabIndex = 27;
			this.lblMAXFormattedSize.Text = "    ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(299, 211);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 26;
			this.label2.Text = "Bytes";
			// 
			// txtMAX
			// 
			this.txtMAX.Location = new System.Drawing.Point(179, 208);
			this.txtMAX.MaxLength = 18;
			this.txtMAX.Name = "txtMAX";
			this.txtMAX.Size = new System.Drawing.Size(114, 20);
			this.txtMAX.TabIndex = 3;
			this.txtMAX.TextChanged += new System.EventHandler(this.txtMAX_TextChanged);
			// 
			// lblMAX
			// 
			this.lblMAX.AutoSize = true;
			this.lblMAX.Location = new System.Drawing.Point(6, 211);
			this.lblMAX.Name = "lblMAX";
			this.lblMAX.Size = new System.Drawing.Size(122, 13);
			this.lblMAX.TabIndex = 24;
			this.lblMAX.Text = "Exclude files bigger than";
			// 
			// chkIT
			// 
			this.chkIT.AutoSize = true;
			this.chkIT.Location = new System.Drawing.Point(8, 87);
			this.chkIT.Name = "chkIT";
			this.chkIT.Size = new System.Drawing.Size(195, 17);
			this.chkIT.TabIndex = 3;
			this.chkIT.Text = "Include files with changed attributes";
			this.chkIT.UseVisualStyleBackColor = true;
			// 
			// chkXL
			// 
			this.chkXL.AutoSize = true;
			this.chkXL.Location = new System.Drawing.Point(6, 65);
			this.chkXL.Name = "chkXL";
			this.chkXL.Size = new System.Drawing.Size(197, 17);
			this.chkXL.TabIndex = 2;
			this.chkXL.Text = "Exclude files not in destination folder";
			this.chkXL.UseVisualStyleBackColor = true;
			// 
			// chkXN
			// 
			this.chkXN.AutoSize = true;
			this.chkXN.Location = new System.Drawing.Point(6, 42);
			this.chkXN.Name = "chkXN";
			this.chkXN.Size = new System.Drawing.Size(117, 17);
			this.chkXN.TabIndex = 1;
			this.chkXN.Text = "Exclude newer files";
			this.chkXN.UseVisualStyleBackColor = true;
			// 
			// chkXC
			// 
			this.chkXC.AutoSize = true;
			this.chkXC.Location = new System.Drawing.Point(6, 19);
			this.chkXC.Name = "chkXC";
			this.chkXC.Size = new System.Drawing.Size(159, 17);
			this.chkXC.TabIndex = 0;
			this.chkXC.Text = "Exclude files of different size";
			this.chkXC.UseVisualStyleBackColor = true;
			// 
			// gbExcludeFolders
			// 
			this.gbExcludeFolders.Controls.Add(this.bnDeleteExcludedFolder);
			this.gbExcludeFolders.Controls.Add(this.bnExcludeFolder);
			this.gbExcludeFolders.Controls.Add(this.lstExcludedFolders);
			this.gbExcludeFolders.Location = new System.Drawing.Point(471, 207);
			this.gbExcludeFolders.Name = "gbExcludeFolders";
			this.gbExcludeFolders.Size = new System.Drawing.Size(491, 212);
			this.gbExcludeFolders.TabIndex = 3;
			this.gbExcludeFolders.TabStop = false;
			this.gbExcludeFolders.Text = "Exclude folders";
			// 
			// lstExcludedFolders
			// 
			this.lstExcludedFolders.FormattingEnabled = true;
			this.lstExcludedFolders.Location = new System.Drawing.Point(6, 19);
			this.lstExcludedFolders.Name = "lstExcludedFolders";
			this.lstExcludedFolders.Size = new System.Drawing.Size(479, 147);
			this.lstExcludedFolders.Sorted = true;
			this.lstExcludedFolders.TabIndex = 0;
			// 
			// bnOK
			// 
			this.bnOK.Location = new System.Drawing.Point(389, 600);
			this.bnOK.Name = "bnOK";
			this.bnOK.Size = new System.Drawing.Size(76, 29);
			this.bnOK.TabIndex = 4;
			this.bnOK.Text = "OK";
			this.bnOK.UseVisualStyleBackColor = true;
			this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
			// 
			// bnCancel
			// 
			this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bnCancel.Location = new System.Drawing.Point(471, 600);
			this.bnCancel.Name = "bnCancel";
			this.bnCancel.Size = new System.Drawing.Size(76, 29);
			this.bnCancel.TabIndex = 5;
			this.bnCancel.Text = "Cancel";
			this.bnCancel.UseVisualStyleBackColor = true;
			this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
			// 
			// gbAttributes
			// 
			this.gbAttributes.Controls.Add(this.gbRemoveAttributes);
			this.gbAttributes.Controls.Add(this.gbAddAttributes);
			this.gbAttributes.Controls.Add(this.gbExcludeAttributes);
			this.gbAttributes.Controls.Add(this.gbIncludeAttributes);
			this.gbAttributes.Controls.Add(this.chkM);
			this.gbAttributes.Location = new System.Drawing.Point(12, 12);
			this.gbAttributes.Name = "gbAttributes";
			this.gbAttributes.Size = new System.Drawing.Size(950, 189);
			this.gbAttributes.TabIndex = 0;
			this.gbAttributes.TabStop = false;
			this.gbAttributes.Text = "Attributes";
			// 
			// gbRemoveAttributes
			// 
			this.gbRemoveAttributes.Controls.Add(this.chkAME);
			this.gbRemoveAttributes.Controls.Add(this.chkAMT);
			this.gbRemoveAttributes.Controls.Add(this.chkAMN);
			this.gbRemoveAttributes.Controls.Add(this.chkAMC);
			this.gbRemoveAttributes.Controls.Add(this.chkAMH);
			this.gbRemoveAttributes.Controls.Add(this.chkAMS);
			this.gbRemoveAttributes.Controls.Add(this.chkAMA);
			this.gbRemoveAttributes.Controls.Add(this.chkAMR);
			this.gbRemoveAttributes.Location = new System.Drawing.Point(459, 114);
			this.gbRemoveAttributes.Name = "gbRemoveAttributes";
			this.gbRemoveAttributes.Size = new System.Drawing.Size(485, 66);
			this.gbRemoveAttributes.TabIndex = 10;
			this.gbRemoveAttributes.TabStop = false;
			this.gbRemoveAttributes.Text = "Remove these attributes from backed up files:";
			// 
			// chkAME
			// 
			this.chkAME.AutoSize = true;
			this.chkAME.Location = new System.Drawing.Point(275, 42);
			this.chkAME.Name = "chkAME";
			this.chkAME.Size = new System.Drawing.Size(74, 17);
			this.chkAME.TabIndex = 8;
			this.chkAME.Text = "Encrypted";
			this.chkAME.UseVisualStyleBackColor = true;
			// 
			// chkAMT
			// 
			this.chkAMT.AutoSize = true;
			this.chkAMT.Location = new System.Drawing.Point(195, 42);
			this.chkAMT.Name = "chkAMT";
			this.chkAMT.Size = new System.Drawing.Size(76, 17);
			this.chkAMT.TabIndex = 7;
			this.chkAMT.Text = "Temporary";
			this.chkAMT.UseVisualStyleBackColor = true;
			// 
			// chkAMN
			// 
			this.chkAMN.AutoSize = true;
			this.chkAMN.Location = new System.Drawing.Point(110, 42);
			this.chkAMN.Name = "chkAMN";
			this.chkAMN.Size = new System.Drawing.Size(83, 17);
			this.chkAMN.TabIndex = 6;
			this.chkAMN.Text = "Not indexed";
			this.chkAMN.UseVisualStyleBackColor = true;
			// 
			// chkAMC
			// 
			this.chkAMC.AutoSize = true;
			this.chkAMC.Location = new System.Drawing.Point(6, 42);
			this.chkAMC.Name = "chkAMC";
			this.chkAMC.Size = new System.Drawing.Size(84, 17);
			this.chkAMC.TabIndex = 5;
			this.chkAMC.Text = "Compressed";
			this.chkAMC.UseVisualStyleBackColor = true;
			// 
			// chkAMH
			// 
			this.chkAMH.AutoSize = true;
			this.chkAMH.Location = new System.Drawing.Point(275, 19);
			this.chkAMH.Name = "chkAMH";
			this.chkAMH.Size = new System.Drawing.Size(60, 17);
			this.chkAMH.TabIndex = 3;
			this.chkAMH.Text = "Hidden";
			this.chkAMH.UseVisualStyleBackColor = true;
			// 
			// chkAMS
			// 
			this.chkAMS.AutoSize = true;
			this.chkAMS.Location = new System.Drawing.Point(195, 19);
			this.chkAMS.Name = "chkAMS";
			this.chkAMS.Size = new System.Drawing.Size(60, 17);
			this.chkAMS.TabIndex = 2;
			this.chkAMS.Text = "System";
			this.chkAMS.UseVisualStyleBackColor = true;
			// 
			// chkAMA
			// 
			this.chkAMA.AutoSize = true;
			this.chkAMA.Location = new System.Drawing.Point(110, 19);
			this.chkAMA.Name = "chkAMA";
			this.chkAMA.Size = new System.Drawing.Size(62, 17);
			this.chkAMA.TabIndex = 1;
			this.chkAMA.Text = "Archive";
			this.chkAMA.UseVisualStyleBackColor = true;
			// 
			// chkAMR
			// 
			this.chkAMR.AutoSize = true;
			this.chkAMR.Location = new System.Drawing.Point(6, 19);
			this.chkAMR.Name = "chkAMR";
			this.chkAMR.Size = new System.Drawing.Size(74, 17);
			this.chkAMR.TabIndex = 0;
			this.chkAMR.Text = "Read only";
			this.chkAMR.UseVisualStyleBackColor = true;
			// 
			// gbAddAttributes
			// 
			this.gbAddAttributes.Controls.Add(this.chkAPE);
			this.gbAddAttributes.Controls.Add(this.chkAPN);
			this.gbAddAttributes.Controls.Add(this.chkAPC);
			this.gbAddAttributes.Controls.Add(this.chkAPH);
			this.gbAddAttributes.Controls.Add(this.chkAPS);
			this.gbAddAttributes.Controls.Add(this.chkAPA);
			this.gbAddAttributes.Controls.Add(this.chkAPR);
			this.gbAddAttributes.Location = new System.Drawing.Point(6, 114);
			this.gbAddAttributes.Name = "gbAddAttributes";
			this.gbAddAttributes.Size = new System.Drawing.Size(447, 66);
			this.gbAddAttributes.TabIndex = 9;
			this.gbAddAttributes.TabStop = false;
			this.gbAddAttributes.Text = "Add these attributes to backed up files:";
			// 
			// chkAPE
			// 
			this.chkAPE.AutoSize = true;
			this.chkAPE.Location = new System.Drawing.Point(195, 42);
			this.chkAPE.Name = "chkAPE";
			this.chkAPE.Size = new System.Drawing.Size(74, 17);
			this.chkAPE.TabIndex = 8;
			this.chkAPE.Text = "Encrypted";
			this.chkAPE.UseVisualStyleBackColor = true;
			this.chkAPE.Click += new System.EventHandler(this.chkAPE_Click);
			// 
			// chkAPN
			// 
			this.chkAPN.AutoSize = true;
			this.chkAPN.Location = new System.Drawing.Point(110, 42);
			this.chkAPN.Name = "chkAPN";
			this.chkAPN.Size = new System.Drawing.Size(83, 17);
			this.chkAPN.TabIndex = 6;
			this.chkAPN.Text = "Not indexed";
			this.chkAPN.UseVisualStyleBackColor = true;
			// 
			// chkAPC
			// 
			this.chkAPC.AutoSize = true;
			this.chkAPC.Location = new System.Drawing.Point(6, 42);
			this.chkAPC.Name = "chkAPC";
			this.chkAPC.Size = new System.Drawing.Size(84, 17);
			this.chkAPC.TabIndex = 5;
			this.chkAPC.Text = "Compressed";
			this.chkAPC.UseVisualStyleBackColor = true;
			this.chkAPC.Click += new System.EventHandler(this.chkAPC_Click);
			// 
			// chkAPH
			// 
			this.chkAPH.AutoSize = true;
			this.chkAPH.Location = new System.Drawing.Point(275, 19);
			this.chkAPH.Name = "chkAPH";
			this.chkAPH.Size = new System.Drawing.Size(60, 17);
			this.chkAPH.TabIndex = 3;
			this.chkAPH.Text = "Hidden";
			this.chkAPH.UseVisualStyleBackColor = true;
			// 
			// chkAPS
			// 
			this.chkAPS.AutoSize = true;
			this.chkAPS.Location = new System.Drawing.Point(195, 19);
			this.chkAPS.Name = "chkAPS";
			this.chkAPS.Size = new System.Drawing.Size(60, 17);
			this.chkAPS.TabIndex = 2;
			this.chkAPS.Text = "System";
			this.chkAPS.UseVisualStyleBackColor = true;
			this.chkAPS.Click += new System.EventHandler(this.chkAPS_Click);
			// 
			// chkAPA
			// 
			this.chkAPA.AutoSize = true;
			this.chkAPA.Location = new System.Drawing.Point(110, 19);
			this.chkAPA.Name = "chkAPA";
			this.chkAPA.Size = new System.Drawing.Size(62, 17);
			this.chkAPA.TabIndex = 1;
			this.chkAPA.Text = "Archive";
			this.chkAPA.UseVisualStyleBackColor = true;
			// 
			// chkAPR
			// 
			this.chkAPR.AutoSize = true;
			this.chkAPR.Location = new System.Drawing.Point(6, 19);
			this.chkAPR.Name = "chkAPR";
			this.chkAPR.Size = new System.Drawing.Size(74, 17);
			this.chkAPR.TabIndex = 0;
			this.chkAPR.Text = "Read only";
			this.chkAPR.UseVisualStyleBackColor = true;
			this.chkAPR.Click += new System.EventHandler(this.chkAPR_Click);
			// 
			// gbExcludeAttributes
			// 
			this.gbExcludeAttributes.Controls.Add(this.chkXAE);
			this.gbExcludeAttributes.Controls.Add(this.chkXAT);
			this.gbExcludeAttributes.Controls.Add(this.chkXAN);
			this.gbExcludeAttributes.Controls.Add(this.chkXAC);
			this.gbExcludeAttributes.Controls.Add(this.chkXAO);
			this.gbExcludeAttributes.Controls.Add(this.chkXAH);
			this.gbExcludeAttributes.Controls.Add(this.chkXAS);
			this.gbExcludeAttributes.Controls.Add(this.chkXAA);
			this.gbExcludeAttributes.Controls.Add(this.chkXAR);
			this.gbExcludeAttributes.Location = new System.Drawing.Point(459, 19);
			this.gbExcludeAttributes.Name = "gbExcludeAttributes";
			this.gbExcludeAttributes.Size = new System.Drawing.Size(485, 66);
			this.gbExcludeAttributes.TabIndex = 1;
			this.gbExcludeAttributes.TabStop = false;
			this.gbExcludeAttributes.Text = "Exclude files with any of the given attributes set:";
			// 
			// chkXAE
			// 
			this.chkXAE.AutoSize = true;
			this.chkXAE.Location = new System.Drawing.Point(275, 42);
			this.chkXAE.Name = "chkXAE";
			this.chkXAE.Size = new System.Drawing.Size(74, 17);
			this.chkXAE.TabIndex = 8;
			this.chkXAE.Text = "Encrypted";
			this.chkXAE.UseVisualStyleBackColor = true;
			// 
			// chkXAT
			// 
			this.chkXAT.AutoSize = true;
			this.chkXAT.Location = new System.Drawing.Point(195, 42);
			this.chkXAT.Name = "chkXAT";
			this.chkXAT.Size = new System.Drawing.Size(76, 17);
			this.chkXAT.TabIndex = 7;
			this.chkXAT.Text = "Temporary";
			this.chkXAT.UseVisualStyleBackColor = true;
			// 
			// chkXAN
			// 
			this.chkXAN.AutoSize = true;
			this.chkXAN.Location = new System.Drawing.Point(110, 42);
			this.chkXAN.Name = "chkXAN";
			this.chkXAN.Size = new System.Drawing.Size(83, 17);
			this.chkXAN.TabIndex = 6;
			this.chkXAN.Text = "Not indexed";
			this.chkXAN.UseVisualStyleBackColor = true;
			// 
			// chkXAC
			// 
			this.chkXAC.AutoSize = true;
			this.chkXAC.Location = new System.Drawing.Point(6, 42);
			this.chkXAC.Name = "chkXAC";
			this.chkXAC.Size = new System.Drawing.Size(84, 17);
			this.chkXAC.TabIndex = 5;
			this.chkXAC.Text = "Compressed";
			this.chkXAC.UseVisualStyleBackColor = true;
			// 
			// chkXAO
			// 
			this.chkXAO.AutoSize = true;
			this.chkXAO.Location = new System.Drawing.Point(355, 19);
			this.chkXAO.Name = "chkXAO";
			this.chkXAO.Size = new System.Drawing.Size(56, 17);
			this.chkXAO.TabIndex = 4;
			this.chkXAO.Text = "Offline";
			this.chkXAO.UseVisualStyleBackColor = true;
			// 
			// chkXAH
			// 
			this.chkXAH.AutoSize = true;
			this.chkXAH.Location = new System.Drawing.Point(275, 19);
			this.chkXAH.Name = "chkXAH";
			this.chkXAH.Size = new System.Drawing.Size(60, 17);
			this.chkXAH.TabIndex = 3;
			this.chkXAH.Text = "Hidden";
			this.chkXAH.UseVisualStyleBackColor = true;
			// 
			// chkXAS
			// 
			this.chkXAS.AutoSize = true;
			this.chkXAS.Location = new System.Drawing.Point(195, 19);
			this.chkXAS.Name = "chkXAS";
			this.chkXAS.Size = new System.Drawing.Size(60, 17);
			this.chkXAS.TabIndex = 2;
			this.chkXAS.Text = "System";
			this.chkXAS.UseVisualStyleBackColor = true;
			// 
			// chkXAA
			// 
			this.chkXAA.AutoSize = true;
			this.chkXAA.Location = new System.Drawing.Point(110, 19);
			this.chkXAA.Name = "chkXAA";
			this.chkXAA.Size = new System.Drawing.Size(62, 17);
			this.chkXAA.TabIndex = 1;
			this.chkXAA.Text = "Archive";
			this.chkXAA.UseVisualStyleBackColor = true;
			// 
			// chkXAR
			// 
			this.chkXAR.AutoSize = true;
			this.chkXAR.Location = new System.Drawing.Point(6, 19);
			this.chkXAR.Name = "chkXAR";
			this.chkXAR.Size = new System.Drawing.Size(74, 17);
			this.chkXAR.TabIndex = 0;
			this.chkXAR.Text = "Read only";
			this.chkXAR.UseVisualStyleBackColor = true;
			// 
			// gbIncludeAttributes
			// 
			this.gbIncludeAttributes.Controls.Add(this.chkIAE);
			this.gbIncludeAttributes.Controls.Add(this.chkIAT);
			this.gbIncludeAttributes.Controls.Add(this.chkIAN);
			this.gbIncludeAttributes.Controls.Add(this.chkIAC);
			this.gbIncludeAttributes.Controls.Add(this.chkIAO);
			this.gbIncludeAttributes.Controls.Add(this.chkIAH);
			this.gbIncludeAttributes.Controls.Add(this.chkIAS);
			this.gbIncludeAttributes.Controls.Add(this.chkIAA);
			this.gbIncludeAttributes.Controls.Add(this.chkIAR);
			this.gbIncludeAttributes.Location = new System.Drawing.Point(6, 19);
			this.gbIncludeAttributes.Name = "gbIncludeAttributes";
			this.gbIncludeAttributes.Size = new System.Drawing.Size(447, 66);
			this.gbIncludeAttributes.TabIndex = 0;
			this.gbIncludeAttributes.TabStop = false;
			this.gbIncludeAttributes.Text = "Include only files with any of the given attributes set:";
			// 
			// chkIAE
			// 
			this.chkIAE.AutoSize = true;
			this.chkIAE.Location = new System.Drawing.Point(275, 42);
			this.chkIAE.Name = "chkIAE";
			this.chkIAE.Size = new System.Drawing.Size(74, 17);
			this.chkIAE.TabIndex = 8;
			this.chkIAE.Text = "Encrypted";
			this.chkIAE.UseVisualStyleBackColor = true;
			// 
			// chkIAT
			// 
			this.chkIAT.AutoSize = true;
			this.chkIAT.Location = new System.Drawing.Point(195, 42);
			this.chkIAT.Name = "chkIAT";
			this.chkIAT.Size = new System.Drawing.Size(76, 17);
			this.chkIAT.TabIndex = 7;
			this.chkIAT.Text = "Temporary";
			this.chkIAT.UseVisualStyleBackColor = true;
			// 
			// chkIAN
			// 
			this.chkIAN.AutoSize = true;
			this.chkIAN.Location = new System.Drawing.Point(110, 42);
			this.chkIAN.Name = "chkIAN";
			this.chkIAN.Size = new System.Drawing.Size(83, 17);
			this.chkIAN.TabIndex = 6;
			this.chkIAN.Text = "Not indexed";
			this.chkIAN.UseVisualStyleBackColor = true;
			// 
			// chkIAC
			// 
			this.chkIAC.AutoSize = true;
			this.chkIAC.Location = new System.Drawing.Point(6, 42);
			this.chkIAC.Name = "chkIAC";
			this.chkIAC.Size = new System.Drawing.Size(84, 17);
			this.chkIAC.TabIndex = 5;
			this.chkIAC.Text = "Compressed";
			this.chkIAC.UseVisualStyleBackColor = true;
			// 
			// chkIAO
			// 
			this.chkIAO.AutoSize = true;
			this.chkIAO.Location = new System.Drawing.Point(355, 19);
			this.chkIAO.Name = "chkIAO";
			this.chkIAO.Size = new System.Drawing.Size(56, 17);
			this.chkIAO.TabIndex = 4;
			this.chkIAO.Text = "Offline";
			this.chkIAO.UseVisualStyleBackColor = true;
			// 
			// chkIAH
			// 
			this.chkIAH.AutoSize = true;
			this.chkIAH.Location = new System.Drawing.Point(275, 19);
			this.chkIAH.Name = "chkIAH";
			this.chkIAH.Size = new System.Drawing.Size(60, 17);
			this.chkIAH.TabIndex = 3;
			this.chkIAH.Text = "Hidden";
			this.chkIAH.UseVisualStyleBackColor = true;
			// 
			// chkIAS
			// 
			this.chkIAS.AutoSize = true;
			this.chkIAS.Location = new System.Drawing.Point(195, 19);
			this.chkIAS.Name = "chkIAS";
			this.chkIAS.Size = new System.Drawing.Size(60, 17);
			this.chkIAS.TabIndex = 2;
			this.chkIAS.Text = "System";
			this.chkIAS.UseVisualStyleBackColor = true;
			// 
			// chkIAA
			// 
			this.chkIAA.AutoSize = true;
			this.chkIAA.Location = new System.Drawing.Point(110, 19);
			this.chkIAA.Name = "chkIAA";
			this.chkIAA.Size = new System.Drawing.Size(62, 17);
			this.chkIAA.TabIndex = 1;
			this.chkIAA.Text = "Archive";
			this.chkIAA.UseVisualStyleBackColor = true;
			// 
			// chkIAR
			// 
			this.chkIAR.AutoSize = true;
			this.chkIAR.Location = new System.Drawing.Point(6, 19);
			this.chkIAR.Name = "chkIAR";
			this.chkIAR.Size = new System.Drawing.Size(74, 17);
			this.chkIAR.TabIndex = 0;
			this.chkIAR.Text = "Read only";
			this.chkIAR.UseVisualStyleBackColor = true;
			// 
			// gbFileOptions
			// 
			this.gbFileOptions.Controls.Add(this.chkMIR);
			this.gbFileOptions.Controls.Add(this.chkCOPY_S);
			this.gbFileOptions.Controls.Add(this.lblBInfo2);
			this.gbFileOptions.Controls.Add(this.lblBInfo1);
			this.gbFileOptions.Controls.Add(this.chkCOPY_U);
			this.gbFileOptions.Controls.Add(this.chkCOPY_O);
			this.gbFileOptions.Controls.Add(this.chkB);
			this.gbFileOptions.Controls.Add(this.chkIT);
			this.gbFileOptions.Controls.Add(this.chkXC);
			this.gbFileOptions.Controls.Add(this.chkXL);
			this.gbFileOptions.Controls.Add(this.chkXN);
			this.gbFileOptions.Location = new System.Drawing.Point(471, 425);
			this.gbFileOptions.Name = "gbFileOptions";
			this.gbFileOptions.Size = new System.Drawing.Size(491, 169);
			this.gbFileOptions.TabIndex = 2;
			this.gbFileOptions.TabStop = false;
			this.gbFileOptions.Text = "Files";
			// 
			// chkMIR
			// 
			this.chkMIR.Location = new System.Drawing.Point(8, 104);
			this.chkMIR.Name = "chkMIR";
			this.chkMIR.Size = new System.Drawing.Size(195, 54);
			this.chkMIR.TabIndex = 4;
			this.chkMIR.Text = "Use \"mirror mode\" (destination files will be deleted if they do not exist in the " +
				"source";
			this.chkMIR.UseVisualStyleBackColor = true;
			// 
			// chkCOPY_S
			// 
			this.chkCOPY_S.AutoSize = true;
			this.chkCOPY_S.Location = new System.Drawing.Point(239, 19);
			this.chkCOPY_S.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.chkCOPY_S.Name = "chkCOPY_S";
			this.chkCOPY_S.Size = new System.Drawing.Size(224, 17);
			this.chkCOPY_S.TabIndex = 5;
			this.chkCOPY_S.Text = "Back up NTFS security information (ACLs)";
			this.chkCOPY_S.UseVisualStyleBackColor = true;
			// 
			// lblBInfo2
			// 
			this.lblBInfo2.AutoSize = true;
			this.lblBInfo2.Location = new System.Drawing.Point(300, 121);
			this.lblBInfo2.Name = "lblBInfo2";
			this.lblBInfo2.Size = new System.Drawing.Size(52, 13);
			this.lblBInfo2.TabIndex = 12;
			this.lblBInfo2.Text = " privilege)";
			// 
			// chkCOPY_U
			// 
			this.chkCOPY_U.AutoSize = true;
			this.chkCOPY_U.Location = new System.Drawing.Point(239, 64);
			this.chkCOPY_U.Name = "chkCOPY_U";
			this.chkCOPY_U.Size = new System.Drawing.Size(191, 17);
			this.chkCOPY_U.TabIndex = 7;
			this.chkCOPY_U.Text = "Back up NTFS auditing information";
			this.chkCOPY_U.UseVisualStyleBackColor = true;
			// 
			// chkCOPY_O
			// 
			this.chkCOPY_O.AutoSize = true;
			this.chkCOPY_O.Location = new System.Drawing.Point(239, 41);
			this.chkCOPY_O.Name = "chkCOPY_O";
			this.chkCOPY_O.Size = new System.Drawing.Size(202, 17);
			this.chkCOPY_O.TabIndex = 6;
			this.chkCOPY_O.Text = "Back up NTFS ownership information";
			this.chkCOPY_O.UseVisualStyleBackColor = true;
			// 
			// frmNewSourceAdvanced
			// 
			this.AcceptButton = this.bnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bnCancel;
			this.ClientSize = new System.Drawing.Size(974, 641);
			this.Controls.Add(this.gbFileOptions);
			this.Controls.Add(this.gbAttributes);
			this.Controls.Add(this.bnCancel);
			this.Controls.Add(this.bnOK);
			this.Controls.Add(this.gbExcludeFolders);
			this.Controls.Add(this.gbExcludeFiles);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewSourceAdvanced";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Advanced Backup Options";
			this.Load += new System.EventHandler(this.frmNewSourceAdvanced_Load);
			this.gbExcludeFiles.ResumeLayout(false);
			this.gbExcludeFiles.PerformLayout();
			this.gbExcludeFolders.ResumeLayout(false);
			this.gbAttributes.ResumeLayout(false);
			this.gbAttributes.PerformLayout();
			this.gbRemoveAttributes.ResumeLayout(false);
			this.gbRemoveAttributes.PerformLayout();
			this.gbAddAttributes.ResumeLayout(false);
			this.gbAddAttributes.PerformLayout();
			this.gbExcludeAttributes.ResumeLayout(false);
			this.gbExcludeAttributes.PerformLayout();
			this.gbIncludeAttributes.ResumeLayout(false);
			this.gbIncludeAttributes.PerformLayout();
			this.gbFileOptions.ResumeLayout(false);
			this.gbFileOptions.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ListBox lstExcludedFiles;
        private System.Windows.Forms.GroupBox gbExcludeFiles;
        private System.Windows.Forms.Button bnDeleteExcludedFile;
        private System.Windows.Forms.Button bnExcluceFile;
        private System.Windows.Forms.GroupBox gbExcludeFolders;
        private System.Windows.Forms.Button bnDeleteExcludedFolder;
        private System.Windows.Forms.Button bnExcludeFolder;
        private System.Windows.Forms.ListBox lstExcludedFolders;
        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.GroupBox gbExcludeAttributes;
        private System.Windows.Forms.CheckBox chkXAE;
        private System.Windows.Forms.CheckBox chkXAT;
        private System.Windows.Forms.CheckBox chkXAN;
        private System.Windows.Forms.CheckBox chkXAC;
        private System.Windows.Forms.CheckBox chkXAO;
        private System.Windows.Forms.CheckBox chkXAH;
        private System.Windows.Forms.CheckBox chkXAS;
        private System.Windows.Forms.CheckBox chkXAA;
        private System.Windows.Forms.CheckBox chkXAR;
        private System.Windows.Forms.GroupBox gbIncludeAttributes;
        private System.Windows.Forms.CheckBox chkIAE;
        private System.Windows.Forms.CheckBox chkIAT;
        private System.Windows.Forms.CheckBox chkIAN;
        private System.Windows.Forms.CheckBox chkIAC;
        private System.Windows.Forms.CheckBox chkIAO;
        private System.Windows.Forms.CheckBox chkIAH;
        private System.Windows.Forms.CheckBox chkIAS;
        private System.Windows.Forms.CheckBox chkIAA;
        private System.Windows.Forms.CheckBox chkIAR;
        private System.Windows.Forms.CheckBox chkM;
        private System.Windows.Forms.CheckBox chkIT;
        private System.Windows.Forms.CheckBox chkXL;
        private System.Windows.Forms.CheckBox chkXN;
        private System.Windows.Forms.CheckBox chkXC;
        private System.Windows.Forms.GroupBox gbFileOptions;
        private System.Windows.Forms.Button bnMINLAD;
        private System.Windows.Forms.Label lblMINLAD;
        private System.Windows.Forms.Button bnMAXLAD;
        private System.Windows.Forms.Label lblMAXLAD;
        private System.Windows.Forms.Button bnMINAGE;
        private System.Windows.Forms.Label lblMINAGE;
        private System.Windows.Forms.Button bnMAXAGE;
        private System.Windows.Forms.Label lblMAXAGE;
        private System.Windows.Forms.Label lblMINFormattedSize;
        private System.Windows.Forms.Label label4;
        private NumericTextBox txtMIN;
        private System.Windows.Forms.Label lblMIN;
        private System.Windows.Forms.Label lblMAXFormattedSize;
        private System.Windows.Forms.Label label2;
        private NumericTextBox txtMAX;
        private System.Windows.Forms.Label lblMAX;
        private System.Windows.Forms.Label lblBInfo2;
        private System.Windows.Forms.Label lblBInfo1;
        private System.Windows.Forms.CheckBox chkCOPY_U;
        private System.Windows.Forms.CheckBox chkCOPY_O;
        private System.Windows.Forms.CheckBox chkB;
        private System.Windows.Forms.CheckBox chkCOPY_S;
        private System.Windows.Forms.Label lblLADINFO;
        private System.Windows.Forms.GroupBox gbRemoveAttributes;
        private System.Windows.Forms.CheckBox chkAME;
        private System.Windows.Forms.CheckBox chkAMT;
        private System.Windows.Forms.CheckBox chkAMN;
        private System.Windows.Forms.CheckBox chkAMC;
        private System.Windows.Forms.CheckBox chkAMH;
        private System.Windows.Forms.CheckBox chkAMS;
        private System.Windows.Forms.CheckBox chkAMA;
        private System.Windows.Forms.CheckBox chkAMR;
        private System.Windows.Forms.GroupBox gbAddAttributes;
        private System.Windows.Forms.CheckBox chkAPE;
        private System.Windows.Forms.CheckBox chkAPN;
        private System.Windows.Forms.CheckBox chkAPC;
        private System.Windows.Forms.CheckBox chkAPH;
        private System.Windows.Forms.CheckBox chkAPS;
        private System.Windows.Forms.CheckBox chkAPA;
        private System.Windows.Forms.CheckBox chkAPR;
        private System.Windows.Forms.CheckBox chkMIR;
    }
}