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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace RoboG
{
	public partial class frmMain : BackupForm
	{

		private bool _Aborting = false;
		private bool _BackupFinished = false;
		private bool _CycleDots = false;
		private StringBuilder _Log = new StringBuilder();
		private Int64 _CopiedBytes = 0;
		private Int64 _BytesToCopy = 0;
		private Int64 _TotalJobTime = 0;
		private double _TransferRate = 0;
		private delegate void NoParameterDelegate();
		private delegate void OneStringParameterDelegate(String Parameter);
		private delegate void OneSingleParameterDelegate(Single Parameter);
		private delegate void OneBoolParameterDelegate(bool Parameter);
		private delegate void OneLongParameterDelegate(Int64 Parameter);
		private delegate void SetProgressBarStyleDelegate(ProgressBarStyle ProgressBarStyle);
		private delegate void FileChangedDelegate(String File, Int64 Size);
		private delegate void UpdateStatusDelegate(String Parameter, bool SetCycleDots);
		private ShadowCopy _ShadowCopy = null;
		private bool _CreatingSnapshot = false;
		private Thread _BackupThread;

		public frmMain()
		{
			SetGlobalData();
			InitializeComponent();
			HelperFunctions.CheckLanguage();
			SetText();
		}



		private void frmHaupt_Load(object sender, EventArgs e)
		{
			try
			{
				this.MinimumSize = new Size(500, 300);
				this.MaximumSize = new Size(Screen.AllScreens.Max(s => s.Bounds.Width) + 50, 300);


			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void SetText()
		{
			try
			{
				lblStatusCaption.Text = Translations.Get("lblStatusCaption");
				lblDirectoryCaption.Text = Translations.Get("lblDirectoryCaption");
				lblFileCaption.Text = Translations.Get("lblFileCaption");
				lblSizeCaption.Text = Translations.Get("lblSizeCaption");
				bnDetails.Text = Translations.Get("bnDetails");
				bnErrors.Text = Translations.Get("bnErrors");
				bnClose.Text = Translations.Get("Close");
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void SetGlobalData()
		{
			try
			{
				GlobalData.MainWindow = this;
				GlobalData.SourceVolumeCount = Settings.Sources.Count;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void DoBackup()
		{
			try
			{
				IO.StReset();

				if (Settings.ProgramBeforeBackup != "")
				{
					try
					{

						HelperFunctions.UpdateStatus(Translations.Get("StartingProgram") + Settings.ProgramBeforeBackup + "...");
						Process Process = new Process();
						Process.StartInfo.FileName = Settings.ProgramBeforeBackup;
						Process.StartInfo.Arguments = Settings.ArgumentsProgramBeforeBackup;
						if (Settings.ProgramBeforeBackupAdmin) Process.StartInfo.Verb = "runas";
						Process.Start();
						Process.WaitForExit();
					}
					catch (Exception ex)
					{
						HelperFunctions.ShowError(ex);
					}
				}

				if (Settings.ShadowCopy)
				{
					HelperFunctions.UpdateStatus(Translations.Get("StartingVSS") + "...");
					IO.CheckSourceDrives();
					if (Service.StartVSS())
					{
						if (!CreateSnapshot())
						{
							Service.StopVSS();
							_BackupFinished = true;
							return;
						}
					}
					else
					{
						_BackupFinished = true;
						return;
					}
				}

				Backup.Initialize();

				for (int i = 0; i < Settings.Sources.Count; i++)
				{
					if (Settings.ShadowCopy && Settings.Sources[i].ShadowCopy)
					{
						Settings.Sources[i].Mount = Settings.Sources[i].SourcePath.Replace(Settings.Sources[i].Volume, GlobalData.ShadowCopyMounts[Settings.Sources[i].Volume]);
					}
				}

				bool Error = !Backup.DoBackup();

				ClearFields();
				if (_ShadowCopy != null)
				{
					HelperFunctions.UpdateStatus(Translations.Get("DeletingSnapshots") + "...");
					_ShadowCopy.Dispose();
					_ShadowCopy = null;
					HelperFunctions.UpdateStatus(Translations.Get("StoppingVSS") + "...");
					Service.StopVSS();
				}


				if (GlobalData.LogPath != null) // && System.IO.Directory.Exists(IO.GetBackupRoot()))
				{
					try
					{
						String LogFilePath = GlobalData.LogPath;
						if (!System.IO.Directory.Exists(LogFilePath))
						{
							System.IO.Directory.CreateDirectory(LogFilePath);
						}
						LogFilePath += "\\RoboG-Log.txt"; //+ "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
						if (System.IO.File.Exists(LogFilePath)) File.Delete(LogFilePath);
						HelperFunctions.WriteLog(Translations.Get("Done"));
						
						using (StreamWriter SW = new StreamWriter(LogFilePath, false))
						{
							SW.Write(_Log.ToString());
						}
					}
					catch (Exception ex)
					{
						HelperFunctions.ShowError(ex);
					}
				}
				IO.WriteErrorLog();
				

				if (Settings.ProgramAfterBackup != "")
				{
					try
					{
						HelperFunctions.UpdateStatus(Translations.Get("StartingProgram") + Settings.ProgramAfterBackup + "...");
						Process Process = new Process();
						Process.StartInfo.FileName = Settings.ProgramAfterBackup;
						Process.StartInfo.Arguments = Settings.ArgumentsProgramAfterBackup;
						if (Settings.ProgramAfterBackupAdmin) Process.StartInfo.Verb = "runas";
						Process.Start();
						Process.WaitForExit();
					}
					catch (Exception ex)
					{
						HelperFunctions.ShowError(ex);
					}
				}

				if (Error) HelperFunctions.UpdateStatus(Translations.Get("CompletedWithErrors")); else HelperFunctions.UpdateStatus(Translations.Get("BackupCompleted"));
				SetProgresssBarStyle(ProgressBarStyle.Blocks);
				SetProgressBarValue(100);

				if ((Error || IO.RoboCopyErrors.Count > 0) && !_Aborting)
				{
					if (Settings.ProgramOnError != "")
					{
						try
						{
							HelperFunctions.UpdateStatus(Translations.Get("StartingProgram") + Settings.ProgramOnError + "...");
							Process Process = new Process();
							Process.StartInfo.FileName = Settings.ProgramOnError;
							Process.StartInfo.Arguments = Settings.ArgumentsProgramOnError;
							if (Settings.ProgramOnErrorAdmin) Process.StartInfo.Verb = "runas";
							Process.Start();
							Process.WaitForExit();
						}
						catch (Exception ex)
						{
							HelperFunctions.ShowError(ex);
						}
					}
				}

				_BackupFinished = true; //Don't delete or move. Necessary for this.Close() to work
				ClearEventHandlers();

				if (Settings.AutoExitAlways || GlobalData.AutoExitCmd)
				{
					Application.Exit();
					return;
				}
				SetBnCloseVisibility(true);
				if (IO.RoboCopyErrors.Count > 0 && !_Aborting)
				{
					SetBnErrorVisibility(true);
					frmErrorSummary ES = new frmErrorSummary();
					ES.ShowDialog();
				}
				else if (!Error && Settings.AutoExit) Application.Exit();
				
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		void FileSystemOperations_FileCopied(String File, Int64 Size)
		{
			try
			{
				if (OverallProgress.IsDisposed) return;
				if (OverallProgress.InvokeRequired)
				{

					Invoke(new FileChangedDelegate(FileSystemOperations_FileCopied), File, Size);

				}
				else
				{
					IO.StStart();
					_TotalJobTime += (Int64)(Double)IO.GetStElapsedTimeSeconds();
					if (IO.GetStElapsedTimeSeconds() > 0 && Size > 0) _TransferRate = ((Size / (IO.GetStElapsedTimeSeconds())));
					lblTransferRate.Text = "[ " + HelperFunctions.SizeSuffix((Int64)(Double)_TransferRate) + " /s ]" + "[ File processing time: " + PrintTimeSpan(IO.GetStElapsedTimeSeconds()) + "] [Job Time: " + PrintTimeSpan(_TotalJobTime) + "]";

					_CopiedBytes += Size;
					Int64 Temp = 0;
					if (_BytesToCopy > 0) Temp = (Int64)((Double)_CopiedBytes / (Double)_BytesToCopy * 1000);
					if (Temp <= 1000) OverallProgress.Value = (int)Temp;
					IO.StReset();
				}

			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}
		public static string PrintTimeSpan(double secs)
		{
			TimeSpan t = TimeSpan.FromSeconds(secs);
			string answer;
			if (t.TotalMinutes < 1.0)
			{
				answer = String.Format("{0}s", t.Seconds);
			}
			else if (t.TotalHours < 1.0)
			{
				answer = String.Format("{0}m:{1:D2}s", t.Minutes, t.Seconds);
			}
			else // more than 1 hour
			{
				answer = String.Format("{0}h:{1:D2}m:{2:D2}s", (int)t.TotalHours, t.Minutes, t.Seconds);
			}

			return answer;
		}

		private void ClearFields()
		{
			FileSystemOperations_DirectoryChanged("");
			SetOverallProgressVisibility(false);
		}

		private void ClearEventHandlers()
		{
			try
			{
				IO.FileChanged -= new IO.FileChangedEventHandler(FileSystemOperations_FileChanged);
				IO.FileCopied -= new IO.FileCopiedEventHandler(FileSystemOperations_FileCopied);
				IO.DirectoryChanged -= new IO.DirectoryChangedEventHandler(FileSystemOperations_DirectoryChanged);
				IO.CopyProgress -= new IO.CopyProgressEventHandler(SetProgressBarValue);
				//IO.RoboCopyError -= new IO.RoboCopyErrorEventHandler(FileSystemOperations_RoboCopyError);
				Backup.StartingBackup -= new Backup.StartingBackupEventHandler(Backup_StartingBackup);
				Backup.SizeDetermined -= new Backup.SizeDeterminedEventHandler(Backup_SizeDetermined);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		void Backup_StartingBackup()
		{
			if (_BytesToCopy > 0)
			{
				SetOverallProgressVisibility(true);
				SetProgresssBarStyle(ProgressBarStyle.Blocks);
			}
		}

		private void SetBnErrorVisibility(bool Visible)
		{
			try
			{
				if (bnErrors.IsDisposed) return;
				if (bnErrors.InvokeRequired)
				{
					Invoke(new OneBoolParameterDelegate(SetBnErrorVisibility), Visible);
				}
				else
				{
					bnErrors.Visible = Visible;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void SetBnCloseVisibility(bool Visible)
		{
			try
			{
				if (bnClose.IsDisposed) return;
				if (bnClose.InvokeRequired)
				{
					Invoke(new OneBoolParameterDelegate(SetBnCloseVisibility), Visible);
				}
				else
				{
					bnClose.Visible = Visible;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		void SetProgresssBarStyle(ProgressBarStyle ProgressBarStyle)
		{
			try
			{
				if (ProgressBar.IsDisposed) return;
				if (ProgressBar.InvokeRequired)
				{
					Invoke(new SetProgressBarStyleDelegate(SetProgresssBarStyle), ProgressBarStyle);
				}
				else
				{
					ProgressBar.Style = ProgressBarStyle;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		void SetOverallProgressVisibility(bool Visibility)
		{
			try
			{
				if (OverallProgress.IsDisposed) return;
				if (OverallProgress.InvokeRequired)
				{
					Invoke(new OneBoolParameterDelegate(SetOverallProgressVisibility), Visibility);
				}
				else
				{
					OverallProgress.Visible = Visibility;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		void FileSystemOperations_RoboCopyError(RoboCopyError Error)
		{
			HelperFunctions.ShowError(Error.Text[0]);
		}

		void Backup_SizeDetermined(Int64 Size)
		{
			_BytesToCopy = Size;
		}

		void SetProgressBarValue(Single Progress)
		{
			try
			{

				if (ProgressBar.IsDisposed) return;
				if (ProgressBar.InvokeRequired)
				{
					Invoke(new OneSingleParameterDelegate(SetProgressBarValue), Progress);

				}
				else
				{
					if (Progress < 0 || Progress > 100) return;
					ProgressBar.Value = (int)(Progress * 10);

				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		void FileSystemOperations_DirectoryChanged(string Directory)
		{
			try
			{
				if (lblDirectory.IsDisposed || lblFile.IsDisposed || lblSize.IsDisposed || ProgressBar.IsDisposed) return;
				if (lblDirectory.InvokeRequired)
				{
					Invoke(new OneStringParameterDelegate(FileSystemOperations_DirectoryChanged), Directory);
				}
				else
				{
					lblDirectory.Text = Directory;
					lblFile.Text = "";
					lblSize.Text = "";
					ProgressBar.Value = 0;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		void FileSystemOperations_FileChanged(String File, Int64 Size)
		{
			try
			{
				if (lblFile.IsDisposed || lblSize.IsDisposed || ProgressBar.IsDisposed) return;
				if (lblFile.InvokeRequired)
				{
					Invoke(new FileChangedDelegate(FileSystemOperations_FileChanged), File, Size);
				}
				else
				{
					lblFile.Text = File;
					if (Size > 0) lblSize.Text = HelperFunctions.SizeSuffix(Size);
					ProgressBar.Value = 0;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private bool CreateSnapshot()
		{
			HelperFunctions.UpdateStatus(Translations.Get("CreatingSnapshots") + "...");
			_CreatingSnapshot = true;
			try
			{
				GlobalData.SourceVolumes.Clear();
				for (int i = 0; i < Settings.Sources.Count; i++)
				{
					if (Settings.Sources[i].ShadowCopy && GlobalData.SourceVolumes.IndexOf(Settings.Sources[i].Volume) == -1) GlobalData.SourceVolumes.Add(Settings.Sources[i].Volume);
				}
				if (GlobalData.SourceVolumes.Count > 0)
				{
					_ShadowCopy = new ShadowCopy();
					if (!_ShadowCopy.Create(GlobalData.SourceVolumes))
					{
						_ShadowCopy = null;
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
				return false;
			}
			_CreatingSnapshot = false;
			return true;
		}


		private void frmHaupt_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (!_BackupFinished)
				{
					if (!_Aborting && HelperFunctions.ShowYesNoQuestion("RoboG", Translations.Get("AbortQuestion"), 2) == System.Windows.Forms.DialogResult.Yes)
					{
						if (Backup.Abort())
						{
							_Aborting = true;
							UpdateStatus(Translations.Get("Aborting") + "...");
							ClearEventHandlers();
							CloseTimer.Enabled = true;
						}
					}
					e.Cancel = true;
					return;
				}
				while (_CreatingSnapshot)
				{
					Thread.Sleep(1000);
				}
				if (_ShadowCopy != null)
				{
					_ShadowCopy.Dispose();
					_ShadowCopy = null;
					Service.StopVSS();
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void WriteLog(String Text)
		{
			try
			{
				if (txtLog.IsDisposed) return;
				if (txtLog.InvokeRequired)
				{
					Invoke(new OneStringParameterDelegate(WriteLog), Text);
				}
				else
				{
					_Log.AppendLine(Text);
					if (txtLog.Visible) txtLog.AppendText(Text + "\r\n");
				}
			}
			catch { }
		}

		private void UpdateStatus(String Text)
		{
			UpdateStatus(Text, true);
		}

		private void UpdateStatus(String Text, bool SetCycleDots)
		{
			try
			{
				if (lblFile.IsDisposed || lblSize.IsDisposed || lblDirectory.IsDisposed || lblStatus.IsDisposed) return;
				if (lblStatus.InvokeRequired)
				{
					Invoke(new UpdateStatusDelegate(UpdateStatus), Text, SetCycleDots);
				}
				else
				{
					if (SetCycleDots)
					{
						if (Text.EndsWith(".")) _CycleDots = true; else _CycleDots = false;
						lblFile.Text = "";
						lblSize.Text = "";
						lblDirectory.Text = "";
						lblTargetDir.Text = "";
					}

					lblStatus.Text = Text;
					lblStripStatus.Text = Text;
					lblStripStatus2.Text = " Transferred: " + HelperFunctions.SizeSuffix(_CopiedBytes) + " / " + (HelperFunctions.SizeSuffix(Math.Abs(_BytesToCopy - _CopiedBytes)));
					lblTargetDir.Text = IO.GetTargetDir();
				}

			}
			catch { }
		}


		private void bnDetails_Click(object sender, EventArgs e)
		{
			if (!txtLog.Visible)
			{
				this.MaximumSize = new Size(0, 0);
				this.Height += 250;
				txtLog.Top = bnDetails.Bottom + 5;
				txtLog.Height = this.Height - bnDetails.Bottom - 70;
				txtLog.Visible = true;
				txtLog.Text = _Log.ToString();
				if (txtLog.Text.Length > 0) txtLog.Select(txtLog.Text.Length - 1, 0);
				txtLog.ScrollToCaret();
				bnDetails.Text = Translations.Get("bnDetails2");
			}
			else
			{
				txtLog.Visible = false;
				this.Height = bnDetails.Bottom + 15;
				bnDetails.Text = Translations.Get("bnDetails");
				this.MaximumSize = new Size(Screen.AllScreens.Max(s => s.Bounds.Width) + 50, 300);
			}
		}

		private void DotTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (_CycleDots)
				{
					String Text = lblStatus.Text;
					int DotIndex = Text.Length;
					while (Text[DotIndex - 1] == '.')
					{
						DotIndex--;
					}
					if (DotIndex == Text.Length) DotIndex = -1;
					String Dots = "";
					if (DotIndex > -1)
					{
						Dots = Text.Substring(DotIndex, Text.Length - DotIndex);
						Text = Text.Substring(0, DotIndex);
					}
					String NewDots = "";
					switch (Dots.Length)
					{
						case 0:
							NewDots = ".";
							break;
						case 1:
							NewDots = "..";
							break;
						case 2:
							NewDots = "...";
							break;
						case 3:
							NewDots = "";
							break;
					}
					UpdateStatus(Text + NewDots, false);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
				DotTimer.Enabled = false;
			}
		}

		private void CloseTimer_Tick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void bnErrors_Click(object sender, EventArgs e)
		{
			try
			{
				frmErrorSummary ES = new frmErrorSummary();
				ES.ShowDialog();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void frmMain_Shown(object sender, EventArgs e)
		{
			try
			{
				String RobocopyVersion = "";
				if (!IO.RobocopyInstalled(out RobocopyVersion))
				{
					if (RobocopyVersion == "")
					{
						HelperFunctions.ShowError(Translations.Get("NoRobocopy"));
					}
					else
					{
						HelperFunctions.ShowError(Translations.Get("RobocopyTooOld"));
					}
					_BackupFinished = true;
					this.Close();
					return;
				}

				if (Settings.Sources.Count == 0)
				{
					HelperFunctions.ShowMessage(Translations.Get("NoSources"), "RoboG");
					_BackupFinished = true;
					this.Close();
					return;
				}

				HelperFunctions.LogCallback = new HelperFunctions.LogDelegate(WriteLog);
				HelperFunctions.StatusCallback = new HelperFunctions.LogDelegate(UpdateStatus);
				IO.FileChanged += new IO.FileChangedEventHandler(FileSystemOperations_FileChanged);
				IO.FileCopied += new IO.FileCopiedEventHandler(FileSystemOperations_FileCopied);
				IO.DirectoryChanged += new IO.DirectoryChangedEventHandler(FileSystemOperations_DirectoryChanged);
				IO.CopyProgress += new IO.CopyProgressEventHandler(SetProgressBarValue);
				//FileSystemOperations.RoboCopyError += new FileSystemOperations.RoboCopyErrorEventHandler(FileSystemOperations_RoboCopyError);
				Backup.StartingBackup += new Backup.StartingBackupEventHandler(Backup_StartingBackup);
				Backup.SizeDetermined += new Backup.SizeDeterminedEventHandler(Backup_SizeDetermined);

				if (GlobalData.BackupVolume != null)
				{
					_BackupThread = new Thread(new ThreadStart(DoBackup));
					_BackupThread.Start();
				}
				else
				{
					_BackupFinished = true;
					this.Close();
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnClose_Click(object sender, EventArgs e)
		{
			
		this.CloseForm();
		}
	}
}
