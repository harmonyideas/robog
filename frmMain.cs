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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RoboG
{
	public partial class frmMain : BackupForm
	{
		// The copy thread never touches the UI for per-file progress. It updates counters in IO,
		// and this timer reads them a few times a second. With billions of files, marshalling
		// every robocopy line to the UI thread would cap throughput at a few thousand files/s.
		private const int RefreshIntervalMs = 500;
		private const int LogFlushIntervalMs = 2000;

		private volatile bool _Aborting = false;
		private volatile bool _BackupFinished = false;
		private volatile bool _Copying = false;
		private volatile bool _CreatingSnapshot = false;
		private bool _CycleDots = false;
		private long _BytesToCopy = 0;
		private ShadowCopy _ShadowCopy = null;
		private Thread _BackupThread;

		private readonly System.Windows.Forms.Timer _RefreshTimer = new System.Windows.Forms.Timer();
		private readonly Stopwatch _JobTime = new Stopwatch();
		private long _RateBytes = 0;
		private TimeSpan _RateTime = TimeSpan.Zero;
		private double _TransferRate = 0;
		private int _CollapsedHeight = 0;
		private long _ShownLogVersion = -1;
		private TimeSpan _LastLogFlush = TimeSpan.Zero;

		private delegate void UpdateStatusDelegate(String Parameter, bool SetCycleDots);

		public frmMain()
		{
			SetGlobalData();
			InitializeComponent();
			HelperFunctions.CheckLanguage();
			SetText();
			_RefreshTimer.Interval = RefreshIntervalMs;
			_RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
		}

		private void frmHaupt_Load(object sender, EventArgs e)
		{
			try
			{
				// Only the width can change until the log is shown.
				_CollapsedHeight = this.Height;
				this.MinimumSize = new Size(this.MinimumSize.Width, _CollapsedHeight);
				this.MaximumSize = new Size(Screen.AllScreens.Max(s => s.Bounds.Width) + 50, _CollapsedHeight);
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

		#region Backup thread

		private void DoBackup()
		{
			try
			{
				RunProgram(Settings.ProgramBeforeBackup, Settings.ArgumentsProgramBeforeBackup, Settings.ProgramBeforeBackupAdmin);

				bool Error = false;
				if (Settings.ShadowCopy)
				{
					HelperFunctions.UpdateStatus(Translations.Get("StartingVSS") + "...");
					IO.CheckSourceDrives();
					Error = !CreateSnapshot(); // On failure nothing is copied, but the job still finishes normally below.
				}

				if (!Error)
				{
					Backup.Initialize();
					for (int i = 0; i < Settings.Sources.Count; i++)
					{
						if (Settings.ShadowCopy && Settings.Sources[i].ShadowCopy)
						{
							Settings.Sources[i].Mount = Settings.Sources[i].SourcePath.Replace(Settings.Sources[i].Volume, GlobalData.ShadowCopyMounts[Settings.Sources[i].Volume]);
						}
					}
					Error = !Backup.DoBackup();
				}

				ClearFields();
				if (_ShadowCopy != null)
				{
					if (!_Aborting && !_ShadowCopy.IsStillValid())
					{
						// Windows drops a snapshot when its shadow storage fills up; anything read after that failed.
						Error = true;
						HelperFunctions.ShowError("Windows discarded the shadow copy during the sync, most likely because shadow storage on the source drive filled up. Files read after that point were not copied.\n\nIncrease shadow storage (vssadmin resize shadowstorage) or sync while the drive is less busy, then run the job again.");
					}
					HelperFunctions.UpdateStatus(Translations.Get("DeletingSnapshots") + "...");
					_ShadowCopy.Dispose();
					_ShadowCopy = null;
				}

				HelperFunctions.WriteLog(Translations.Get("Done"));
				FinishLog();

				RunProgram(Settings.ProgramAfterBackup, Settings.ArgumentsProgramAfterBackup, Settings.ProgramAfterBackupAdmin);

				if (Error) HelperFunctions.UpdateStatus(Translations.Get("CompletedWithErrors")); else HelperFunctions.UpdateStatus(Translations.Get("BackupCompleted"));
				SetProgresssBarStyle(ProgressBarStyle.Blocks);
				SetProgressBarValue(_Aborting ? 0 : int.MaxValue);

				bool RobocopyErrors = IO.ErrorCount > 0;
				if ((Error || RobocopyErrors) && !_Aborting)
				{
					RunProgram(Settings.ProgramOnError, Settings.ArgumentsProgramOnError, Settings.ProgramOnErrorAdmin);
				}

				_BackupFinished = true; //Don't delete or move. Necessary for this.Close() to work
				ClearEventHandlers();

				if (Settings.AutoExitAlways || GlobalData.AutoExitCmd)
				{
					Application.Exit();
					return;
				}
				SetVisibility(bnClose, true);
				if (RobocopyErrors && !_Aborting)
				{
					SetVisibility(bnErrors, true);
					RunOnUIThread(ShowErrorSummary);
				}
				else if (!Error && Settings.AutoExit) Application.Exit();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
				FinishLog();
			}
		}

		private void RunProgram(String Program, String Arguments, bool Admin)
		{
			if (String.IsNullOrEmpty(Program)) return;
			try
			{
				HelperFunctions.UpdateStatus(Translations.Get("StartingProgram") + Program + "...");
				using (Process Process = new Process())
				{
					Process.StartInfo.FileName = Program;
					Process.StartInfo.Arguments = Arguments;
					if (Admin) Process.StartInfo.Verb = "runas";
					Process.Start();
					Process.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void FinishLog()
		{
			IO.WriteErrorLog();
			RunLog.Stop();
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
			finally
			{
				_CreatingSnapshot = false;
			}
			return true;
		}

		void Backup_StartingBackup()
		{
			_Copying = true;
			// With a scan the total is known and the bar shows real progress; without one it keeps animating.
			if (Interlocked.Read(ref _BytesToCopy) > 0) SetProgresssBarStyle(ProgressBarStyle.Blocks);
		}

		void Backup_SizeDetermined(Int64 Size)
		{
			Interlocked.Exchange(ref _BytesToCopy, Size);
		}

		private void ClearFields()
		{
			_Copying = false;
			RunOnUIThread(delegate
			{
				lblDirectory.Text = "";
				lblFile.Text = "";
				lblSize.Text = "";
			});
		}

		private void ClearEventHandlers()
		{
			Backup.StartingBackup -= new Backup.StartingBackupEventHandler(Backup_StartingBackup);
			Backup.SizeDetermined -= new Backup.SizeDeterminedEventHandler(Backup_SizeDetermined);
		}

		#endregion

		#region UI updates

		// Runs an action on the UI thread without blocking the caller.
		private void RunOnUIThread(MethodInvoker Action)
		{
			try
			{
				if (this.IsDisposed || !this.IsHandleCreated) return;
				if (this.InvokeRequired) this.BeginInvoke(Action); else Action();
			}
			catch (ObjectDisposedException) { }
			catch (InvalidOperationException) { } // Form is closing.
		}

		private void SetVisibility(Control Control, bool Visible)
		{
			RunOnUIThread(delegate { if (!Control.IsDisposed) Control.Visible = Visible; });
		}

		void SetProgresssBarStyle(ProgressBarStyle Style)
		{
			RunOnUIThread(delegate { if (!OverallProgress.IsDisposed) OverallProgress.Style = Style; });
		}

		void SetProgressBarValue(int Value)
		{
			RunOnUIThread(delegate { if (!OverallProgress.IsDisposed) OverallProgress.Value = Math.Max(OverallProgress.Minimum, Math.Min(OverallProgress.Maximum, Value)); });
		}

		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.IsDisposed) return;
				TimeSpan Now = _JobTime.Elapsed;
				long Copied = IO.CopiedBytes;
				long ToCopy = Interlocked.Read(ref _BytesToCopy);

				if (_Copying)
				{
					lblDirectory.Text = IO.CurrentDirectory ?? "";
					lblFile.Text = IO.CurrentFile ?? "";
					long Size = IO.CurrentFileSize;
					lblSize.Text = Size > 0 ? HelperFunctions.SizeSuffix(Size) : "";
					if (ToCopy > 0 && OverallProgress.Style == ProgressBarStyle.Blocks) OverallProgress.Value = (int)Math.Min(OverallProgress.Maximum, Copied * OverallProgress.Maximum / ToCopy);
				}

				// Transfer rate averaged over at least two seconds, so it doesn't jump from file to file.
				double Seconds = (Now - _RateTime).TotalSeconds;
				if (Seconds >= 2)
				{
					_TransferRate = Math.Max(0, (Copied - _RateBytes) / Seconds);
					_RateBytes = Copied;
					_RateTime = Now;
				}

				if (!Settings.LogFiles)
				{
					// robocopy runs with /NFL /NDL in this mode, so it reports no per-file progress.
					lblFilesValue.Text = "-";
					lblProgressText.Text = "Live progress is off (fastest mode). See the summary in the log when done.";
				}
				else lblFilesValue.Text = IO.FilesCopied.ToString("N0");
				lblSpeedValue.Text = _Copying && Settings.LogFiles ? HelperFunctions.SizeSuffix((long)_TransferRate) + "/s" : "-";
				lblElapsedValue.Text = PrintTimeSpan(Now.TotalSeconds);
				lblErrorsValue.Text = IO.ErrorCount.ToString("N0");
				lblErrorsValue.ForeColor = IO.ErrorCount > 0 ? Color.Firebrick : lblFilesValue.ForeColor;
				if (_Copying && ToCopy > Copied && _TransferRate > 0) lblRemainingValue.Text = PrintTimeSpan((ToCopy - Copied) / _TransferRate);
				else if (!_BackupFinished) lblRemainingValue.Text = "-";
				else lblRemainingValue.Text = "0s";

				if (!Settings.LogFiles) { }
				else if (ToCopy > 0) lblProgressText.Text = HelperFunctions.SizeSuffix(Copied) + " of " + HelperFunctions.SizeSuffix(ToCopy) + " copied (" + Math.Min(100, Copied * 100 / ToCopy) + "%)";
				else if (Copied > 0) lblProgressText.Text = HelperFunctions.SizeSuffix(Copied) + " copied";
				lblTargetDir.Text = IO.GetTargetDir() ?? "";

				if (txtLog.Visible) ShowRecentLog();

				if ((Now - _LastLogFlush).TotalMilliseconds >= LogFlushIntervalMs)
				{
					RunLog.Flush(); // Keeps the log file readable while a long job runs.
					_LastLogFlush = Now;
				}

				if (_BackupFinished)
				{
					_RefreshTimer.Stop();
					_JobTime.Stop();
				}
			}
			catch (Exception ex)
			{
				_RefreshTimer.Stop();
				HelperFunctions.ShowError(ex);
			}
		}

		private void ShowRecentLog()
		{
			long Version = RunLog.Version;
			if (Version == _ShownLogVersion) return;
			_ShownLogVersion = Version;
			txtLog.Text = RunLog.GetRecentText();
			txtLog.SelectionStart = txtLog.TextLength;
			txtLog.ScrollToCaret();
		}

		public static string PrintTimeSpan(double secs)
		{
			TimeSpan t = TimeSpan.FromSeconds(secs);
			if (t.TotalMinutes < 1.0) return String.Format("{0}s", t.Seconds);
			if (t.TotalHours < 1.0) return String.Format("{0}m:{1:D2}s", t.Minutes, t.Seconds);
			return String.Format("{0}h:{1:D2}m:{2:D2}s", (int)t.TotalHours, t.Minutes, t.Seconds);
		}

		private void UpdateStatus(String Text)
		{
			UpdateStatus(Text, true);
		}

		// Called from the backup thread; never waits for the UI thread.
		private void UpdateStatus(String Text, bool SetCycleDots)
		{
			try
			{
				if (this.IsDisposed || lblStatus.IsDisposed) return;
				if (lblStatus.InvokeRequired)
				{
					BeginInvoke(new UpdateStatusDelegate(UpdateStatus), Text, SetCycleDots);
					return;
				}
				if (SetCycleDots)
				{
					_CycleDots = Text.EndsWith(".");
					if (!_Copying)
					{
						lblFile.Text = "";
						lblSize.Text = "";
						lblDirectory.Text = "";
					}
				}
				lblStatus.Text = Text;
			}
			catch { }
		}

		private void DotTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (!_CycleDots) return;
				String Text = lblStatus.Text;
				String Base = Text.TrimEnd('.');
				int Dots = (Text.Length - Base.Length + 1) % 4;
				UpdateStatus(Base + new String('.', Dots), false);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
				DotTimer.Enabled = false;
			}
		}

		private void ShowErrorSummary()
		{
			try
			{
				using (frmErrorSummary ES = new frmErrorSummary())
				{
					ES.ShowDialog(this);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		#endregion

		#region Form events

		private void frmMain_Shown(object sender, EventArgs e)
		{
			try
			{
				String RobocopyVersion = "";
				if (!IO.RobocopyInstalled(out RobocopyVersion))
				{
					HelperFunctions.ShowError(Translations.Get(RobocopyVersion == "" ? "NoRobocopy" : "RobocopyTooOld"));
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

				if (GlobalData.BackupVolume == null)
				{
					_BackupFinished = true;
					this.Close();
					return;
				}

				if (GlobalData.LogPath != null)
				{
					String LogFile = GlobalData.LogPath + "\\RoboG-Log.txt";
					if (RunLog.Start(LogFile)) lblStripStatus.Text = "Log: " + LogFile;
				}
				HelperFunctions.StatusCallback = new HelperFunctions.LogDelegate(UpdateStatus);
				Backup.StartingBackup += new Backup.StartingBackupEventHandler(Backup_StartingBackup);
				Backup.SizeDetermined += new Backup.SizeDeterminedEventHandler(Backup_SizeDetermined);

				_JobTime.Start();
				_RefreshTimer.Start();
				_BackupThread = new Thread(new ThreadStart(DoBackup));
				_BackupThread.IsBackground = true;
				_BackupThread.Start();
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
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
				_RefreshTimer.Stop();
				// Snapshots are released by the backup thread; if it is gone, Windows deletes them when RoboG exits.
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private void bnDetails_Click(object sender, EventArgs e)
		{
			if (!txtLog.Visible)
			{
				this.MaximumSize = new Size(0, 0);
				this.Height = _CollapsedHeight + 300;
				txtLog.Top = bnDetails.Bottom + 10;
				txtLog.Height = frmMainStatusStrip.Top - txtLog.Top - 10;
				txtLog.Visible = true;
				_ShownLogVersion = -1;
				ShowRecentLog();
				bnDetails.Text = Translations.Get("bnDetails2");
			}
			else
			{
				txtLog.Visible = false;
				this.Height = _CollapsedHeight;
				bnDetails.Text = Translations.Get("bnDetails");
				this.MaximumSize = new Size(Screen.AllScreens.Max(s => s.Bounds.Width) + 50, _CollapsedHeight);
			}
		}

		private void CloseTimer_Tick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void bnErrors_Click(object sender, EventArgs e)
		{
			ShowErrorSummary();
		}

		private void bnClose_Click(object sender, EventArgs e)
		{
			this.CloseForm();
		}

		#endregion
	}
}
