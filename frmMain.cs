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
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RoboG
{
	/// <summary>
	/// Progress window: the whole run at the top, an Overview tab listing every job with the run log,
	/// and one tab per job with its own progress and log.
	/// </summary>
	public partial class frmMain : BackupForm
	{
		// The copy threads never touch the UI. Each job updates its own counters, and this timer reads
		// them a few times a second. With billions of files, marshalling every robocopy line to the UI
		// thread would cap throughput at a few thousand files/s.
		private const int RefreshIntervalMs = 500;
		private const int LogFlushIntervalMs = 2000;

		private volatile bool _Aborting = false;
		private volatile bool _BackupFinished = false;
		private volatile bool _CreatingSnapshot = false;
		private bool _CycleDots = false;
		private ShadowCopy _ShadowCopy = null;
		private Thread _BackupThread;

		private readonly System.Windows.Forms.Timer _RefreshTimer = new System.Windows.Forms.Timer();
		private readonly Stopwatch _JobTime = new Stopwatch();
		private TimeSpan _LastLogFlush = TimeSpan.Zero;

		private readonly List<JobPanel> _Panels = new List<JobPanel>();
		private readonly List<TabPage> _JobTabs = new List<TabPage>();
		private TabPage _OverviewTab;
		private BufferedListView lstJobs;
		private TextBox txtLog;
		private long _ShownLogVersion = -1;

		private delegate void UpdateStatusDelegate(String Parameter, bool SetCycleDots);

		public frmMain()
		{
			SetGlobalData();
			InitializeComponent();
			BuildOverviewTab();
			Theme.Apply(this);
			HelperFunctions.CheckLanguage();
			SetText();
			_RefreshTimer.Interval = RefreshIntervalMs;
			_RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
		}

		private void SetText()
		{
			try
			{
				bnErrors.Text = Translations.Get("bnErrors");
				bnClose.Text = Translations.Get("Close");
				Translations.Set(lblStatus, "ProgressStarting");
				Translations.Set(lblFilesCaption, "TileFiles");
				Translations.Set(lblSpeedCaption, "TileSpeed");
				Translations.Set(lblFileRateCaption, "TileFileRate");
				Translations.Set(lblElapsedCaption, "TileElapsed");
				Translations.Set(lblRemainingCaption, "TileRemaining");
				Translations.Set(lblErrorsCaption, "TileErrors");
				Translations.Set(lblJobsCaption, "TileJobsDone");
				Translations.Set(bnLogs, "bnOpenLogFolder");
				Translations.Set(bnCommand, "bnViewCommand");
				Theme.FitButtonsLeft(10, bnLogs, bnCommand);
				Theme.FitButtonsRight(10, bnErrors, bnClose);
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

		#region Tabs

		// A ListView that does not flicker when its rows are updated twice a second.
		private class BufferedListView : ListView
		{
			public BufferedListView()
			{
				DoubleBuffered = true;
			}
		}

		private void BuildOverviewTab()
		{
			_OverviewTab = new TabPage(Translations.Get("TabOverview"));
			_OverviewTab.BackColor = Theme.Surface;
			_OverviewTab.Padding = new Padding(8);

			lstJobs = new BufferedListView();
			lstJobs.View = View.Details;
			lstJobs.FullRowSelect = true;
			lstJobs.HideSelection = true;
			lstJobs.MultiSelect = false;
			lstJobs.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			lstJobs.BorderStyle = BorderStyle.FixedSingle;
			lstJobs.Font = Theme.BaseFont;
			lstJobs.Columns.Add(Translations.Get("ColJob"), 165);
			lstJobs.Columns.Add(Translations.Get("ColStatus"), 140);
			lstJobs.Columns.Add(Translations.Get("ColProgress"), 62, HorizontalAlignment.Right);
			lstJobs.Columns.Add(Translations.Get("TileFiles"), 82, HorizontalAlignment.Right);
			lstJobs.Columns.Add(Translations.Get("ColCopied"), 78, HorizontalAlignment.Right);
			lstJobs.Columns.Add(Translations.Get("TileSpeed"), 78, HorizontalAlignment.Right);
			lstJobs.Columns.Add(Translations.Get("TileFileRate"), 62, HorizontalAlignment.Right);
			lstJobs.Columns.Add(Translations.Get("TileErrors"), 56, HorizontalAlignment.Right);
			lstJobs.DoubleClick += new EventHandler(lstJobs_DoubleClick);
			lstJobs.SelectedIndexChanged += delegate { UpdateCommandButton(); };
			lstJobs.Dock = DockStyle.Top;
			lstJobs.Height = 170;

			// Docked rather than placed, so the layout fits whatever size the tab ends up (window size, DPI).
			Label lblLog = new Label();
			lblLog.Text = Translations.Get("RunLogCaption");
			lblLog.ForeColor = Theme.TextMuted;
			lblLog.Dock = DockStyle.Top;
			lblLog.Height = 28;
			lblLog.Padding = new Padding(0, 8, 0, 0);

			txtLog = new TextBox();
			txtLog.Multiline = true;
			txtLog.ReadOnly = true;
			txtLog.WordWrap = false;
			txtLog.ScrollBars = ScrollBars.Both;
			txtLog.Font = new Font("Consolas", 9F);
			txtLog.BorderStyle = BorderStyle.FixedSingle;
			txtLog.Dock = DockStyle.Fill;

			// Docking runs from the last-added control to the first: list at the top, then the caption, then the log fills the rest.
			_OverviewTab.Controls.Add(txtLog);
			_OverviewTab.Controls.Add(lblLog);
			_OverviewTab.Controls.Add(lstJobs);
			tabJobs.TabPages.Add(_OverviewTab);
		}

		// One tab and one Overview row per job. Called once the jobs of this run are known.
		private void BuildJobTabs()
		{
			tabJobs.SuspendLayout();
			lstJobs.BeginUpdate();
			foreach (JobRun Run in Backup.Runs)
			{
				JobPanel Panel = new JobPanel(Run);
				Panel.Dock = DockStyle.Fill;
				TabPage Page = new TabPage(Panel.TabTitle());
				Page.BackColor = Theme.Surface;
				Page.Controls.Add(Panel);
				Page.ToolTipText = Run.Source.SourcePath + " -> " + Run.Source.TargetPath;
				tabJobs.TabPages.Add(Page);
				_Panels.Add(Panel);
				_JobTabs.Add(Page);

				ListViewItem Item = new ListViewItem(new String[] { Run.Number + ". " + Run.Name, "", "", "", "", "", "", "" });
				Item.Tag = Page;
				lstJobs.Items.Add(Item);
			}
			tabJobs.ShowToolTips = true;
			lstJobs.EndUpdate();
			tabJobs.ResumeLayout();
			// With a single job its own tab is the interesting one.
			if (_JobTabs.Count == 1) tabJobs.SelectedTab = _JobTabs[0];
		}

		// The job the user is looking at: the selected job tab, or on the Overview tab the selected row.
		private JobRun SelectedRun()
		{
			int Index = tabJobs.SelectedIndex - 1;
			if (Index < 0 && lstJobs.SelectedIndices.Count > 0) Index = lstJobs.SelectedIndices[0];
			return Index >= 0 && Index < _Panels.Count ? _Panels[Index].Run : null;
		}

		private void UpdateCommandButton()
		{
			JobRun Run = SelectedRun();
			bool Enabled = Run != null && Run.CommandLine != null;
			if (bnCommand.Enabled != Enabled) bnCommand.Enabled = Enabled;
		}

		private void bnCommand_Click(object sender, EventArgs e)
		{
			JobRun Run = SelectedRun();
			if (Run == null || Run.CommandLine == null) return;
			using (frmSwitches Dialog = new frmSwitches(Run.Name, Run.CommandLine)) Dialog.ShowDialog(this);
		}

		private void lstJobs_DoubleClick(object sender, EventArgs e)
		{
			if (lstJobs.SelectedItems.Count == 0) return;
			TabPage Page = lstJobs.SelectedItems[0].Tag as TabPage;
			if (Page != null) tabJobs.SelectedTab = Page;
		}

		private void tabJobs_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ShownLogVersion = -1;
			if (_Panels.Count > 0) RefreshTimer_Tick(this, EventArgs.Empty);
		}

		#endregion

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
					Error = !CreateSnapshot(); // On failure nothing is copied, but the run still finishes normally below.
				}

				if (!Error)
				{
					foreach (JobRun Run in Backup.Runs)
					{
						BackupSource Source = Run.Source;
						String Mount;
						if (Settings.ShadowCopy && Source.ShadowCopy && GlobalData.ShadowCopyMounts.TryGetValue(Source.Volume, out Mount))
						{
							Source.Mount = Source.SourcePath.Replace(Source.Volume, Mount);
						}
					}
					Error = !Backup.DoBackup();
				}
				else
				{
					foreach (JobRun Run in Backup.Runs) Run.Skip();
				}

				if (_ShadowCopy != null)
				{
					if (!_Aborting && !_ShadowCopy.IsStillValid())
					{
						// Windows drops a snapshot when its shadow storage fills up; anything read after that failed.
						Error = true;
						HelperFunctions.ShowError(Translations.Get("ShadowCopyDiscarded1") + "\n\n" + Translations.Get("ShadowCopyDiscarded2"));
					}
					HelperFunctions.UpdateStatus(Translations.Get("DeletingSnapshots") + "...");
					_ShadowCopy.Dispose();
					_ShadowCopy = null;
				}

				// Jobs that failed are reported together, so parallel jobs don't each stop the run with a dialog.
				StringBuilder Failed = new StringBuilder();
				foreach (JobRun Run in Backup.Runs)
				{
					if (Run.State != JobState.Failed) continue;
					Error = true;
					Failed.Append("\n\n").Append(Run.Number).Append(". ").Append(Run.Name).Append(":\n").Append(Run.Message);
				}

				HelperFunctions.WriteLog(Translations.Get("Done"));
				FinishLog();

				RunProgram(Settings.ProgramAfterBackup, Settings.ArgumentsProgramAfterBackup, Settings.ProgramAfterBackupAdmin);

				if (_Aborting) HelperFunctions.UpdateStatus(Translations.Get("Aborted"));
				else if (Error) HelperFunctions.UpdateStatus(Translations.Get("CompletedWithErrors"));
				else HelperFunctions.UpdateStatus(Translations.Get("BackupCompleted"));

				bool RobocopyErrors = IO.ErrorCount > 0;
				if ((Error || RobocopyErrors) && !_Aborting)
				{
					RunProgram(Settings.ProgramOnError, Settings.ArgumentsProgramOnError, Settings.ProgramOnErrorAdmin);
				}

				_BackupFinished = true; //Don't delete or move. Necessary for this.Close() to work
				RunOnUIThread(delegate { RefreshTimer_Tick(this, EventArgs.Empty); });

				if (Settings.AutoExitAlways || GlobalData.AutoExitCmd)
				{
					Application.Exit();
					return;
				}
				if (Failed.Length > 0 && !_Aborting) HelperFunctions.ShowError(Translations.Get("JobsNotCompleted") + Failed.ToString());
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
				_BackupFinished = true;
				SetVisibility(bnClose, true);
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
				foreach (JobRun Run in Backup.Runs)
				{
					if (Run.Source.ShadowCopy && GlobalData.SourceVolumes.IndexOf(Run.Source.Volume) == -1) GlobalData.SourceVolumes.Add(Run.Source.Volume);
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

		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.IsDisposed) return;
				TimeSpan Now = _JobTime.Elapsed;

				long Copied = 0, Total = 0, Files = 0;
				bool TotalKnown = Settings.Scan && Settings.LogFiles;
				double Rate = 0, FileRate = 0;
				int Running = 0, Finished = 0, Waiting = 0, Reconnecting = 0;
				for (int i = 0; i < _Panels.Count; i++)
				{
					JobPanel Panel = _Panels[i];
					JobRun Run = Panel.Run;
					Panel.Sample(Now);
					Rate += Panel.TransferRate;
					FileRate += Panel.FileRate;

					long JobCopied = Run.CopiedBytes;
					Copied += JobCopied;
					Files += Run.FilesCopied;
					if (Run.IsFinished)
					{
						Finished++;
						Total += JobCopied; // A finished job has copied all it is going to.
					}
					else
					{
						if (Run.State == JobState.Reconnecting) Reconnecting++;
						else if (Run.IsRunning) Running++;
						else Waiting++;
						long ToCopy = Run.BytesToCopy;
						if (ToCopy < 0) TotalKnown = false; else Total += Math.Max(ToCopy, JobCopied);
					}

					SetText(_JobTabs[i], Panel.TabTitle());
					UpdateJobRow(lstJobs.Items[i], Panel);
				}

				// Whole run.
				bool Copying = Running > 0;
				if (TotalKnown && Total > 0 && !_BackupFinished)
				{
					if (OverallProgress.Style != ProgressBarStyle.Blocks) OverallProgress.Style = ProgressBarStyle.Blocks;
					OverallProgress.Value = (int)Math.Min(OverallProgress.Maximum, Copied * OverallProgress.Maximum / Total);
				}
				else if (_BackupFinished)
				{
					if (OverallProgress.Style != ProgressBarStyle.Blocks) OverallProgress.Style = ProgressBarStyle.Blocks;
					OverallProgress.Value = _Aborting ? 0 : OverallProgress.Maximum;
				}

				String Progress = "";
				if (_Panels.Count > 0)
				{
					List<String> Parts = new List<String>();
					if (Running > 0) Parts.Add(Translations.Format("JobsRunning", Running));
					if (Reconnecting > 0) Parts.Add(Translations.Format("JobsReconnecting", Reconnecting));
					if (Waiting > 0) Parts.Add(Translations.Format("JobsWaiting", Waiting));
					if (Finished > 0) Parts.Add(Translations.Format("JobsFinished", Finished));
					Progress = Translations.Format("JobsSummary", String.Join(", ", Parts.ToArray()));
					if (Backup.Parallel > 1 && !_BackupFinished) Progress += Translations.Format("JobsAtATime", Backup.Parallel);
					if (!Settings.LogFiles) Progress += ". " + Translations.Get("LiveProgressOff");
					else if (TotalKnown && Total > 0) Progress += ". " + Translations.Format("CopiedOf", HelperFunctions.SizeSuffix(Copied), HelperFunctions.SizeSuffix(Total), Math.Min(100, Copied * 100 / Total));
					else if (Copied > 0) Progress += ". " + Translations.Format("CopiedSoFar", HelperFunctions.SizeSuffix(Copied));
				}
				SetText(lblProgressText, Progress);

				SetText(lblFilesValue, Settings.LogFiles || _BackupFinished ? Files.ToString("N0") : "-");
				SetText(lblSpeedValue, Copying && Settings.LogFiles ? HelperFunctions.SizeSuffix((long)Rate) + "/s" : "-");
				SetText(lblFileRateValue, Copying && Settings.LogFiles ? FileRate.ToString("N0") : "-");
				SetText(lblElapsedValue, PrintTimeSpan(Now.TotalSeconds));
				if (Copying && TotalKnown && Total > Copied && Rate > 0) SetText(lblRemainingValue, PrintTimeSpan((Total - Copied) / Rate));
				else SetText(lblRemainingValue, _BackupFinished ? "0s" : "-");
				int Errors = IO.ErrorCount;
				SetText(lblErrorsValue, Errors.ToString("N0"));
				lblErrorsValue.ForeColor = Errors > 0 ? Color.Firebrick : Theme.Text;
				SetText(lblJobsValue, Finished + " of " + _Panels.Count);

				// Only the visible tab's details and log are drawn.
				UpdateCommandButton();
				int Selected = tabJobs.SelectedIndex - 1;
				if (Selected >= 0 && Selected < _Panels.Count) _Panels[Selected].RefreshView(true);
				else ShowRecentLog();

				if ((Now - _LastLogFlush).TotalMilliseconds >= LogFlushIntervalMs)
				{
					// Keeps the log files readable while a long job runs.
					RunLog.Flush();
					foreach (JobPanel Panel in _Panels) if (Panel.Run.IsRunning) Panel.Run.Log.Flush();
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

		private void UpdateJobRow(ListViewItem Item, JobPanel Panel)
		{
			JobRun Run = Panel.Run;
			JobState State = Run.State;
			int Percent = JobPanel.Percent(Run);
			bool ShowCounts = (Settings.LogFiles || Run.IsFinished) && State != JobState.Waiting;
			SetText(Item.SubItems[1], JobPanel.StateText(Run));
			SetText(Item.SubItems[2], State == JobState.Done || State == JobState.NoChanges ? "100%" : State == JobState.Copying && Percent >= 0 ? Percent + "%" : "");
			SetText(Item.SubItems[3], ShowCounts ? Run.FilesCopied.ToString("N0") : "");
			SetText(Item.SubItems[4], ShowCounts ? HelperFunctions.SizeSuffix(Run.CopiedBytes) : "");
			SetText(Item.SubItems[5], Panel.TransferRate > 0 ? HelperFunctions.SizeSuffix((long)Panel.TransferRate) + "/s" : "");
			SetText(Item.SubItems[6], Panel.FileRate > 0 ? Panel.FileRate.ToString("N0") : "");
			SetText(Item.SubItems[7], Run.ErrorCount > 0 ? Run.ErrorCount.ToString("N0") : "");
			Color Fore = State == JobState.Failed ? Color.Firebrick : State == JobState.Reconnecting ? Color.DarkOrange : State == JobState.Waiting ? Theme.TextMuted : Theme.Text;
			if (Item.ForeColor != Fore) Item.ForeColor = Fore;
		}

		// Setting text repaints even when it is unchanged.
		private static void SetText(Control Control, String Text)
		{
			if (Control.Text != Text) Control.Text = Text;
		}

		private static void SetText(ListViewItem.ListViewSubItem Item, String Text)
		{
			if (Item.Text != Text) Item.Text = Text;
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

		// Called from the backup threads; never waits for the UI thread.
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
				if (SetCycleDots) _CycleDots = Text.EndsWith(".");
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
					if (RunLog.Start(LogFile)) lblStripStatus.Text = Translations.Format("StatusLogFile", LogFile);
				}
				NetworkDrives.ResolveJobs();

				Backup.CreateRuns();
				if (Backup.Runs.Count == 0)
				{
					HelperFunctions.ShowMessage(Translations.Get("AllJobsDisabled"), "RoboG");
					_BackupFinished = true;
					this.Close();
					return;
				}
				BuildJobTabs();

				HelperFunctions.StatusCallback = new HelperFunctions.LogDelegate(UpdateStatus);
				_JobTime.Start();
				_RefreshTimer.Start();
				_BackupThread = new Thread(new ThreadStart(DoBackup));
				_BackupThread.IsBackground = true;
				_BackupThread.Name = "RoboG run";
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

		private void CloseTimer_Tick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void bnLogs_Click(object sender, EventArgs e)
		{
			try
			{
				if (GlobalData.LogPath != null && System.IO.Directory.Exists(GlobalData.LogPath)) Process.Start("explorer.exe", "\"" + GlobalData.LogPath + "\"");
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
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
