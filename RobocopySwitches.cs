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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RoboG {
  /// <summary>Explains the parts of a robocopy command line, switch by switch.</summary>
  static class RobocopySwitches {
    private static readonly Dictionary<String, String> _Help = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase) {
      // Copy options
      { "/S", "Copy subfolders, but not empty ones." },
      { "/E", "Copy subfolders, including empty ones." },
      { "/LEV", "Copy only the top n levels of the source tree." },
      { "/Z", "Restartable mode: a file interrupted mid-copy continues where it stopped. Slower for normal copies." },
      { "/B", "Backup mode: read files regardless of permissions (needs the \"back up files and directories\" privilege)." },
      { "/ZB", "Restartable mode; if access is denied, use backup mode." },
      { "/J", "Unbuffered I/O. Faster for very large files." },
      { "/EFSRAW", "Copy EFS-encrypted files in raw (encrypted) form. Cannot be combined with /MT." },
      { "/COPY", "What to copy for each file." },
      { "/DCOPY", "What to copy for each folder." },
      { "/SEC", "Copy files with security (same as /COPY:DATS)." },
      { "/COPYALL", "Copy everything (same as /COPY:DATSOU)." },
      { "/NOCOPY", "Copy no file information (useful with /PURGE)." },
      { "/SECFIX", "Fix security on all files, even skipped ones." },
      { "/TIMFIX", "Fix timestamps on all files, even skipped ones." },
      { "/PURGE", "Delete files and folders in the target that no longer exist in the source." },
      { "/MIR", "Mirror the tree: copy subfolders (/E) and delete files and folders in the target that no longer exist in the source (/PURGE)." },
      { "/MOV", "Move files: delete them from the source after copying." },
      { "/MOVE", "Move files and folders: delete them from the source after copying." },
      { "/A+", "Add these attributes to copied files." },
      { "/A-", "Remove these attributes from copied files." },
      { "/CREATE", "Create the folder tree and zero-length files only." },
      { "/FAT", "Create target files with 8.3 FAT names only." },
      { "/256", "Turn off support for paths longer than 256 characters." },
      { "/MON", "Monitor the source; run again when more than n changes are seen." },
      { "/MOT", "Monitor the source; run again after n minutes if anything changed." },
      { "/RH", "Run only between these times (hhmm-hhmm)." },
      { "/PF", "Check the run hours per file, not per pass." },
      { "/IPG", "Wait n milliseconds between packets, to leave bandwidth for others on slow lines." },
      { "/SJ", "Copy junctions as junctions instead of copying their targets." },
      { "/SL", "Copy symbolic links as links instead of copying their targets." },
      { "/MT", "Multithreaded copying: copy n files at the same time (1-128, robocopy's default is 8)." },
      { "/NODCOPY", "Copy no folder information." },
      { "/NOOFFLOAD", "Copy without Windows copy offload." },
      { "/COMPRESS", "Request SMB network compression, if the server supports it." },
      // File selection
      { "/A", "Copy only files with the archive attribute set." },
      { "/M", "Copy only files with the archive attribute set, and reset it in the source." },
      { "/IA", "Include only files with any of these attributes." },
      { "/XA", "Exclude files with any of these attributes." },
      { "/XF", "Exclude files matching these names, paths or wildcards." },
      { "/XD", "Exclude folders matching these names or paths." },
      { "/XC", "Exclude changed files (same time, different size)." },
      { "/XN", "Exclude files that are newer in the source than in the target." },
      { "/XO", "Exclude files that are older in the source than in the target." },
      { "/XX", "Exclude extra files (in the target only); they are kept, not deleted." },
      { "/XL", "Exclude \"lonely\" files: files in the source that are not yet in the target." },
      { "/IS", "Include files that are the same in source and target (copy them again)." },
      { "/IT", "Include \"tweaked\" files: same size and time, but different attributes." },
      { "/MAX", "Exclude files bigger than n bytes." },
      { "/MIN", "Exclude files smaller than n bytes." },
      { "/MAXAGE", "Exclude files older than n days (or a date, YYYYMMDD)." },
      { "/MINAGE", "Exclude files newer than n days (or a date, YYYYMMDD)." },
      { "/MAXLAD", "Exclude files not used since n days (or a date)." },
      { "/MINLAD", "Exclude files used since n days (or a date)." },
      { "/XJ", "Exclude junction points, so robocopy doesn't follow links into other folders (or loop forever)." },
      { "/XJD", "Exclude junction points for folders." },
      { "/XJF", "Exclude junction points for files." },
      { "/FFT", "Assume FAT file times: allow a 2-second difference in timestamps (for NAS, Samba and FAT targets)." },
      { "/DST", "Allow for 1-hour differences caused by daylight saving time." },
      // Retry options
      { "/R", "Retry a failed copy n times." },
      { "/W", "Wait n seconds between retries." },
      { "/REG", "Save the /R and /W values as robocopy's defaults in the registry." },
      { "/TBD", "Wait for share names to be defined (retry error 67)." },
      // Logging
      { "/L", "List only: show what would be copied or deleted, but change nothing." },
      { "/X", "Report all extra files, not just selected ones." },
      { "/V", "Verbose: also list skipped files." },
      { "/TS", "Show source file timestamps in the log." },
      { "/FP", "Show full path names in the log." },
      { "/BYTES", "Show sizes in bytes (RoboG reads exact sizes from this)." },
      { "/NS", "Don't log file sizes." },
      { "/NC", "Don't log file classes (New File, Newer, ...)." },
      { "/NFL", "No file list: don't log file names. Fastest; only errors and the summary are logged." },
      { "/NDL", "No folder list: don't log folder names." },
      { "/NP", "No progress: don't log the percentage copied for each file." },
      { "/ETA", "Show the estimated arrival time of copied files." },
      { "/LOG", "Write the log to a file (overwrite)." },
      { "/LOG+", "Write the log to a file (append)." },
      { "/UNILOG", "Write the log in Unicode (overwrite). RoboG points this at a pipe (\\\\.\\pipe\\...) and reads robocopy's progress from it as it is written, in memory and not on disk, so names in any language are shown correctly." },
      { "/UNILOG+", "Write the log to a Unicode file (append)." },
      { "/TEE", "Write the log to the console as well as the log file." },
      { "/NJH", "No job header: don't log the header with the options at the start." },
      { "/NJS", "No job summary: don't log the totals at the end." },
      { "/UNICODE", "Write the console output as Unicode." },
    };

    private static readonly Dictionary<char, String> _Attributes = new Dictionary<char, String> {
      { 'R', "Read-only" }, { 'A', "Archive" }, { 'S', "System" }, { 'H', "Hidden" }, { 'C', "Compressed" },
      { 'N', "Not content indexed" }, { 'E', "Encrypted" }, { 'T', "Temporary" }, { 'O', "Offline" }
    };

    private static readonly Dictionary<char, String> _CopyFlags = new Dictionary<char, String> {
      { 'D', "Data" }, { 'A', "Attributes" }, { 'T', "Timestamps" }, { 'S', "Security (NTFS ACLs)" },
      { 'O', "Owner" }, { 'U', "Auditing" }, { 'X', "Skip alternate data streams" }, { 'E', "Extended attributes" }
    };

    /// <summary>One row of the explanation: the text from the command line and what it does.</summary>
    public class Part {
      public String Text;
      public String Meaning;
      public Part(String Text, String Meaning) { this.Text = Text; this.Meaning = Meaning; }
    }

    /// <summary>Splits a command line into the program, source, target and switches, each with an explanation.</summary>
    public static List<Part> Explain(String CommandLine) {
      List<String> Tokens = Tokenize(CommandLine);
      List<Part> Parts = new List<Part>();
      int Positional = 0;
      Part LastSwitch = null;
      foreach (String Token in Tokens) {
        if (Token.StartsWith("/")) {
          LastSwitch = new Part(Token, Describe(Token));
          Parts.Add(LastSwitch);
          continue;
        }
        if (Positional == 0) Parts.Add(new Part(Token, Translations.Get("SwitchesProgram")));
        else if (Positional == 1 && LastSwitch == null) Parts.Add(new Part(Token, Translations.Get("SwitchesSource")));
        else if (Positional == 2 && LastSwitch == null) Parts.Add(new Part(Token, Translations.Get("SwitchesTarget")));
        else if (LastSwitch != null) LastSwitch.Text += " " + Quote(Token); // Values of /XF, /XD and similar.
        else Parts.Add(new Part(Token, Translations.Get("SwitchesFiles")));
        Positional++;
      }
      return Parts;
    }

    private static String Describe(String Switch) {
      String Name = Switch, Value = "";
      int Colon = Switch.IndexOf(':');
      if (Colon > 0) {
        Name = Switch.Substring(0, Colon);
        Value = Switch.Substring(Colon + 1);
      }
      String Help;
      if (!_Help.TryGetValue(Name, out Help)) return Translations.Get("SwitchesUnknown");

      String Upper = Name.ToUpperInvariant();
      if (Value != "" && (Upper == "/IA" || Upper == "/XA" || Upper == "/A+" || Upper == "/A-")) {
        Help += " " + DecodeLetters(Value, _Attributes) + ".";
      } else if (Value != "" && (Upper == "/COPY" || Upper == "/DCOPY")) {
        Help += " " + DecodeLetters(Value, _CopyFlags) + ".";
      } else if (Value != "" && Upper == "/MT") {
        Help = "Multithreaded copying: copy " + Value + " files at the same time. (RoboG shares its copy threads between the jobs running at once.)";
      } else if (Value != "" && Upper == "/R") {
        Help = "Retry a failed copy " + Value + " times.";
      } else if (Value != "" && Upper == "/W") {
        Help = "Wait " + Value + " seconds between retries.";
      }
      return Help;
    }

    private static String DecodeLetters(String Letters, Dictionary<char, String> Names) {
      List<String> Decoded = new List<String>();
      foreach (char C in Letters.ToUpperInvariant()) {
        String Name;
        Decoded.Add(Names.TryGetValue(C, out Name) ? C + " = " + Name : C.ToString());
      }
      return String.Join(", ", Decoded.ToArray());
    }

    private static String Quote(String Token) {
      return Token.IndexOf(' ') >= 0 ? "\"" + Token + "\"" : Token;
    }

    // Splits like CreateProcess does: spaces separate arguments, double quotes group them.
    private static List<String> Tokenize(String CommandLine) {
      List<String> Tokens = new List<String>();
      StringBuilder Current = new StringBuilder();
      bool InQuotes = false, HasToken = false;
      foreach (char C in CommandLine) {
        if (C == '"') {
          InQuotes = !InQuotes;
          HasToken = true;
        } else if (C == ' ' && !InQuotes) {
          if (HasToken) Tokens.Add(Current.ToString());
          Current.Length = 0;
          HasToken = false;
        } else {
          Current.Append(C);
          HasToken = true;
        }
      }
      if (HasToken) Tokens.Add(Current.ToString());
      return Tokens;
    }
  }

  /// <summary>Shows a robocopy command line with an explanation of every switch.</summary>
  class frmSwitches : Form {
    public frmSwitches(String JobName, String CommandLine) {
      Text = Translations.Format("SwitchesTitle", JobName);
      StartPosition = FormStartPosition.CenterParent;
      Size = new Size(860, 560);
      MinimumSize = new Size(560, 360);
      ShowInTaskbar = false;
      MinimizeBox = false;
      Padding = new Padding(12);

      TextBox txtCommand = new TextBox();
      txtCommand.Multiline = true;
      txtCommand.ReadOnly = true;
      txtCommand.WordWrap = true;
      txtCommand.ScrollBars = ScrollBars.Vertical;
      txtCommand.Font = new Font("Consolas", 9F);
      txtCommand.Text = CommandLine;
      txtCommand.Dock = DockStyle.Top;
      txtCommand.Height = 64;

      Label lblList = new Label();
      lblList.Text = Translations.Get("SwitchesCaption");
      lblList.Dock = DockStyle.Top;
      lblList.Height = 30;
      lblList.Padding = new Padding(0, 10, 0, 0);
      lblList.ForeColor = Theme.TextMuted;

      ListView lstParts = new ListView();
      lstParts.View = View.Details;
      lstParts.FullRowSelect = true;
      lstParts.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      lstParts.BorderStyle = BorderStyle.FixedSingle;
      lstParts.Dock = DockStyle.Fill;
      lstParts.Columns.Add(Translations.Get("SwitchesColSwitch"), 220);
      lstParts.Columns.Add(Translations.Get("SwitchesColMeaning"), 580);
      lstParts.ShowItemToolTips = true;
      foreach (RobocopySwitches.Part Part in RobocopySwitches.Explain(CommandLine)) {
        ListViewItem Item = new ListViewItem(new String[] { Part.Text, Part.Meaning });
        Item.ToolTipText = Part.Meaning;
        lstParts.Items.Add(Item);
      }

      Panel pnlButtons = new Panel();
      pnlButtons.Dock = DockStyle.Bottom;
      pnlButtons.Height = 46;
      Button bnCopy = new Button();
      bnCopy.Text = Translations.Get("bnCopyCommand");
      bnCopy.Size = new Size(130, 30);
      bnCopy.Location = new Point(0, 12);
      bnCopy.Click += (Sender, Args) => {
        try { Clipboard.SetText(CommandLine); } catch { }
      };
      Button bnClose = new Button();
      bnClose.Text = Translations.Get("Close");
      bnClose.Size = new Size(110, 30);
      bnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      bnClose.DialogResult = DialogResult.OK;
      pnlButtons.Controls.Add(bnCopy);
      pnlButtons.Controls.Add(bnClose);
      pnlButtons.Resize += (Sender, Args) => bnClose.Location = new Point(pnlButtons.Width - bnClose.Width, 12);
      AcceptButton = bnClose;
      CancelButton = bnClose;

      // Docking runs from the last-added control to the first.
      Controls.Add(lstParts);
      Controls.Add(lblList);
      Controls.Add(txtCommand);
      Controls.Add(pnlButtons);
      Theme.Apply(this);
      txtCommand.BackColor = Theme.SurfaceAlt;
      lstParts.Columns[1].Width = -2; // Fill the remaining width.
    }
  }
}
