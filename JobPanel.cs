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
using System.Drawing;
using System.Windows.Forms;

namespace RoboG {
  /// <summary>
  /// Progress of one job, shown on its own tab in the progress window. It only reads the job's counters
  /// when the window's timer asks it to, so a job's copy thread never waits for the screen.
  /// </summary>
  class JobPanel : UserControl {
    public readonly JobRun Run;

    private readonly Label lblState = new Label();
    private readonly ProgressBar Bar = new ProgressBar();
    private readonly Label lblProgress = new Label();
    private readonly Label lblFiles, lblSpeed, lblFileRate, lblElapsed, lblRemaining, lblErrors;
    private readonly Label lblSource = new Label();
    private readonly Label lblTarget = new Label();
    private readonly Label lblFile = new Label();
    private readonly Label lblSize = new Label();
    private readonly TextBox txtLog = new TextBox();

    // Transfer rate, averaged over at least two seconds so it doesn't jump from file to file.
    private long _RateBytes = 0;
    private long _RateFiles = 0;
    private double _FileRate = 0;
    private TimeSpan _RateTime = TimeSpan.Zero;
    private double _Rate = 0;
    private long _ShownLogVersion = -1;

    public JobPanel(JobRun Run) {
      this.Run = Run;
      SuspendLayout();
      BackColor = Theme.Surface;
      Font = Theme.BaseFont;
      Size = new Size(760, 360);
      Padding = new Padding(0);

      AnchorStyles TLR = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      Place(lblState, 12, 12, 736, 22, TLR);
      lblState.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
      lblState.AutoEllipsis = true;

      Place(Bar, 14, 38, 732, 16, TLR);
      Bar.Maximum = 10000;
      Bar.MarqueeAnimationSpeed = 30;

      Place(lblProgress, 12, 60, 736, 18, TLR);
      lblProgress.ForeColor = Theme.TextMuted;
      lblProgress.AutoEllipsis = true;

      lblFiles = Tile(Translations.Get("TileFiles"), 0);
      lblSpeed = Tile(Translations.Get("TileSpeed"), 1);
      lblFileRate = Tile(Translations.Get("TileFileRate"), 2);
      lblElapsed = Tile(Translations.Get("TileElapsed"), 3);
      lblRemaining = Tile(Translations.Get("TileRemaining"), 4);
      lblErrors = Tile(Translations.Get("TileErrors"), 5);

      Caption(Translations.Get("lblDirectoryCaption"), 136);
      Caption(Translations.Get("lblTargetCaption"), 156);
      Caption(Translations.Get("lblFileCaption"), 176);
      Place(lblSource, 80, 136, 668, 18, TLR);
      Place(lblTarget, 80, 156, 668, 18, TLR);
      Place(lblFile, 80, 176, 520, 18, TLR);
      // Size of the file being copied, labelled like the rows on the left.
      Label lblSizeCaption = new Label();
      lblSizeCaption.Text = Translations.Get("lblSizeCaption");
      lblSizeCaption.ForeColor = Theme.TextMuted;
      lblSizeCaption.TextAlign = ContentAlignment.TopRight;
      Place(lblSizeCaption, 606, 176, 50, 18, AnchorStyles.Top | AnchorStyles.Right);
      Place(lblSize, 660, 176, 88, 18, AnchorStyles.Top | AnchorStyles.Right);
      foreach (Label L in new Label[] { lblSource, lblTarget, lblFile }) {
        L.AutoEllipsis = true;
        L.UseMnemonic = false;
      }
      lblSource.Text = Run.Source.SourcePath;
      lblTarget.Text = Run.Source.TargetPath;

      Place(txtLog, 12, 204, 736, 146, AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);


      txtLog.Multiline = true;
      txtLog.ReadOnly = true;
      txtLog.WordWrap = false;
      txtLog.ScrollBars = ScrollBars.Both;
      txtLog.Font = new Font("Consolas", 9F);
      txtLog.BackColor = Theme.SurfaceAlt;
      txtLog.BorderStyle = BorderStyle.FixedSingle;

      ResumeLayout(false);
      RefreshView(false);
    }

    private void Place(Control Control, int X, int Y, int Width, int Height, AnchorStyles Anchor) {
      Control.Bounds = new Rectangle(X, Y, Width, Height);
      Control.Anchor = Anchor;
      Controls.Add(Control);
    }

    private void Caption(String Text, int Y) {
      Label L = new Label();
      L.Text = Text;
      L.ForeColor = Theme.TextMuted;
      Place(L, 12, Y, 66, 18, AnchorStyles.Top | AnchorStyles.Left);
    }

    private Label Tile(String Caption, int Index) {
      int X = 12 + Index * 122;
      Label C = new Label();
      C.Text = Caption;
      C.ForeColor = Theme.TextMuted;
      Place(C, X, 88, 116, 17, AnchorStyles.Top | AnchorStyles.Left);
      Label V = new Label();
      V.Text = "-";
      V.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
      V.AutoEllipsis = true;
      Place(V, X, 105, 116, 24, AnchorStyles.Top | AnchorStyles.Left);
      return V;
    }

    /// <summary>Bytes per second this job is copying at (0 when not copying).</summary>
    public double TransferRate { get { return _Rate; } }

    /// <summary>Files per second this job is copying (0 when not copying). With many small files this says more than MB/s.</summary>
    public double FileRate { get { return _FileRate; } }

    /// <summary>Updates the transfer rate. Called on every timer tick for every job, visible or not.</summary>
    public void Sample(TimeSpan Now) {
      long Copied = Run.CopiedBytes;
      long Files = Run.FilesCopied;
      if (Run.State != JobState.Copying || !Settings.LogFiles) {
        _Rate = 0;
        _FileRate = 0;
        _RateBytes = Copied;
        _RateFiles = Files;
        _RateTime = Now;
        return;
      }
      double Seconds = (Now - _RateTime).TotalSeconds;
      if (Seconds < 2) return;
      _Rate = Math.Max(0, (Copied - _RateBytes) / Seconds);
      _FileRate = Math.Max(0, (Files - _RateFiles) / Seconds);
      _RateBytes = Copied;
      _RateFiles = Files;
      _RateTime = Now;
    }

    public static String StateText(JobRun Run) {
      switch (Run.State) {
        case JobState.Waiting: return Translations.Get("StateWaiting");
        case JobState.Scanning: return Translations.Get("StateScanning");
        case JobState.Copying: return Translations.Get("StateCopying");
        case JobState.Reconnecting: return Translations.Get("StateReconnecting");
        case JobState.NoChanges: return Translations.Get("StateNoChanges");
        case JobState.Failed: return Translations.Get("StateFailed");
        case JobState.Aborted: return Translations.Get("StateStopped");
        default: return Run.ErrorCount > 0 ? Translations.Format("StateDoneErrors", Run.ErrorCount.ToString("N0")) : Translations.Get("StateDone");
      }
    }

    /// <summary>Whole percent copied, or -1 when unknown.</summary>
    public static int Percent(JobRun Run) {
      long ToCopy = Run.BytesToCopy;
      if (ToCopy <= 0 || !Settings.LogFiles) return -1;
      return (int)Math.Min(100, Run.CopiedBytes * 100 / ToCopy);
    }

    /// <summary>Short text for the job's tab, e.g. "Photos · 45%".</summary>
    public String TabTitle() {
      String Name = Run.Name;
      if (Name.Length > 28) Name = Name.Substring(0, 26) + "…";
      String Suffix;
      switch (Run.State) {
        case JobState.Waiting: Suffix = Translations.Get("TabWaiting"); break;
        case JobState.Scanning: Suffix = Translations.Get("TabScanning"); break;
        case JobState.Copying:
          int P = Percent(Run);
          Suffix = P >= 0 ? P + "%" : Translations.Get("TabCopying");
          break;
        case JobState.Reconnecting: Suffix = Translations.Get("TabReconnecting"); break;
        case JobState.NoChanges: Suffix = Translations.Get("TabNoChanges"); break;
        case JobState.Failed: Suffix = Translations.Get("TabFailed"); break;
        case JobState.Aborted: Suffix = Translations.Get("TabStopped"); break;
        default: Suffix = Run.ErrorCount > 0 ? Translations.Get("TabDoneErrors") : Translations.Get("TabDone"); break;
      }
      return Name + " · " + Suffix;
    }

    /// <summary>Shows the job's current progress. Called by the window's timer for the selected tab only.</summary>
    public void RefreshView(bool IncludeLog) {
      JobState State = Run.State;
      long Copied = Run.CopiedBytes;
      long ToCopy = Run.BytesToCopy;
      bool Copying = State == JobState.Copying;

      String Status = StateText(Run);
      if (Run.Message != "" && (State == JobState.Failed || State == JobState.Aborted)) Status += ": " + Run.Message;
      SetText(lblState, Status);
      lblState.ForeColor = State == JobState.Failed ? Color.Firebrick : State == JobState.Reconnecting ? Color.DarkOrange : Theme.Text;

      // Progress bar: real progress when the scan found the total, animated while the amount is unknown.
      ProgressBarStyle Style = ProgressBarStyle.Blocks;
      int Value = 0;
      if (Run.IsRunning && (State == JobState.Scanning || ToCopy <= 0 || !Settings.LogFiles)) Style = ProgressBarStyle.Marquee;
      else if (Copying) Value = (int)Math.Min(Bar.Maximum, Copied * Bar.Maximum / ToCopy);
      else if (State == JobState.Done || State == JobState.NoChanges) Value = Bar.Maximum;
      if (Bar.Style != Style) Bar.Style = Style;
      if (Style == ProgressBarStyle.Blocks && Bar.Value != Value) Bar.Value = Value;

      String Progress = "";
      if (State == JobState.Waiting) Progress = Translations.Get("ProgressWaiting");
      else if (State == JobState.Scanning) Progress = Translations.Get("ProgressScanning");
      else if (State == JobState.Reconnecting) Progress = Run.Message + " " + Translations.Get("ProgressResumes");
      else if (Copying && !Settings.LogFiles) Progress = Translations.Get("LiveProgressOff") + " " + Translations.Get("TotalsAtEnd");
      else if (ToCopy > 0) Progress = Translations.Format("CopiedOf", HelperFunctions.SizeSuffix(Copied), HelperFunctions.SizeSuffix(ToCopy), Math.Min(100, Copied * 100 / ToCopy));
      else if (Copied > 0) Progress = Translations.Format("CopiedSoFar", HelperFunctions.SizeSuffix(Copied));
      SetText(lblProgress, Progress);

      bool ShowCounts = Settings.LogFiles || Run.IsFinished;
      SetText(lblFiles, ShowCounts && State != JobState.Waiting ? Run.FilesCopied.ToString("N0") : "-");
      SetText(lblSpeed, Copying && Settings.LogFiles ? HelperFunctions.SizeSuffix((long)_Rate) + "/s" : "-");
      SetText(lblFileRate, Copying && Settings.LogFiles ? _FileRate.ToString("N0") : "-");
      SetText(lblElapsed, State == JobState.Waiting ? "-" : frmMain.PrintTimeSpan(Run.Elapsed.TotalSeconds));
      if (Copying && ToCopy > Copied && _Rate > 0) SetText(lblRemaining, frmMain.PrintTimeSpan((ToCopy - Copied) / _Rate));
      else SetText(lblRemaining, Run.IsFinished ? "0s" : "-");
      int Errors = Run.ErrorCount;
      SetText(lblErrors, Errors.ToString("N0"));
      lblErrors.ForeColor = Errors > 0 ? Color.Firebrick : Theme.Text;

      if (Copying) {
        SetText(lblSource, Run.CurrentDirectory ?? Run.Source.SourcePath);
        SetText(lblFile, Run.CurrentFile ?? "");
        long Size = Run.CurrentFileSize;
        SetText(lblSize, Size > 0 ? HelperFunctions.SizeSuffix(Size) : "");
      } else {
        SetText(lblSource, Run.Source.SourcePath);
        SetText(lblFile, "");
        SetText(lblSize, "");
      }
      SetText(lblTarget, Run.Target ?? "");

      if (IncludeLog) ShowRecentLog();
    }

    private void ShowRecentLog() {
      long Version = Run.Log.Version;
      if (Version == _ShownLogVersion) return;
      _ShownLogVersion = Version;
      txtLog.Text = Run.Log.GetRecentText();
      txtLog.SelectionStart = txtLog.TextLength;
      txtLog.ScrollToCaret();
    }

    // Setting a label's text repaints it even when unchanged.
    private static void SetText(Control Control, String Text) {
      if (Control.Text != Text) Control.Text = Text;
    }
  }
}
