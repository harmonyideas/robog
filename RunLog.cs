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
using System.IO;
using System.Text;

namespace RoboG {
  /// <summary>
  /// A log that streams straight to disk and keeps only the most recent lines in memory.
  /// A sync of billions of files produces far more log text than fits in RAM, so nothing
  /// here grows with the size of the job. Safe to call from any thread.
  /// RoboG has one main log (<see cref="RunLog"/>) and one log per job.
  /// </summary>
  class LogFile {
    // Lines kept in memory for the on-screen log view.
    public const int RecentLineCount = 500;

    private readonly object _Lock = new object();
    private readonly Queue<String> _Recent = new Queue<String>(RecentLineCount);
    private StreamWriter _Writer = null;
    private long _Version = 0;

    public String FilePath { get; private set; }

    /// <summary>Incremented on every write, so the UI can tell whether it needs to refresh.</summary>
    public long Version {
      get {
        lock (_Lock) return _Version;
      }
    }

    /// <summary>Starts a new log file, replacing any previous one. Returns false if the file could not be opened.</summary>
    public bool Start(String Path) {
      lock (_Lock) {
        CloseWriter();
        _Recent.Clear();
        _Version++;
        FilePath = Path;
        try {
          String Directory = System.IO.Path.GetDirectoryName(Path);
          if (!String.IsNullOrEmpty(Directory)) System.IO.Directory.CreateDirectory(Directory);
          _Writer = new StreamWriter(Path, false, new UTF8Encoding(false), 1 << 16);
          return true;
        } catch {
          _Writer = null; // Keep running with the in-memory view only.
          return false;
        }
      }
    }

    public void Write(String Line) {
      if (Line == null) return;
      lock (_Lock) {
        if (_Writer != null) {
          try {
            _Writer.WriteLine(Line);
          } catch {
            CloseWriter(); // Disk full or similar: stop file logging rather than fail the sync.
          }
        }
        if (_Recent.Count >= RecentLineCount) _Recent.Dequeue();
        _Recent.Enqueue(Line);
        _Version++;
      }
    }

    /// <summary>Adds a line to the recent lines shown in the window, without writing it to the log file.</summary>
    public void Show(String Line) {
      if (Line == null) return;
      lock (_Lock) {
        if (_Recent.Count >= RecentLineCount) _Recent.Dequeue();
        _Recent.Enqueue(Line);
        _Version++;
      }
    }

    /// <summary>Pushes buffered lines to disk. Called periodically so the log file is readable during a long run.</summary>
    public void Flush() {
      lock (_Lock) {
        if (_Writer == null) return;
        try {
          _Writer.Flush();
        } catch {
          CloseWriter();
        }
      }
    }

    public void Stop() {
      lock (_Lock) CloseWriter();
    }

    /// <summary>The most recent lines, joined for display.</summary>
    public String GetRecentText() {
      lock (_Lock) {
        StringBuilder Text = new StringBuilder(_Recent.Count * 80);
        foreach (String Line in _Recent) Text.Append(Line).Append("\r\n");
        return Text.ToString();
      }
    }

    private void CloseWriter() {
      if (_Writer == null) return;
      try {
        _Writer.Dispose();
      } catch {}
      _Writer = null;
    }
  }

  /// <summary>The main log: overall steps (shadow copies, job start and end, errors). Each job also has its own <see cref="LogFile"/>.</summary>
  static class RunLog {
    public static readonly LogFile Main = new LogFile();

    public static long Version { get { return Main.Version; } }
    public static bool Start(String FilePath) { return Main.Start(FilePath); }
    public static void Write(String Line) { Main.Write(Line); }
    public static void Flush() { Main.Flush(); }
    public static void Stop() { Main.Stop(); }
    public static String GetRecentText() { return Main.GetRecentText(); }
  }
}
