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
  /// Run log that streams straight to disk and keeps only the most recent lines in memory.
  /// A sync of billions of files produces far more log text than fits in RAM, so nothing
  /// here grows with the size of the job. Safe to call from any thread.
  /// </summary>
  static class RunLog {
    // Lines kept in memory for the on-screen "Details" view.
    public const int RecentLineCount = 2000;

    private static readonly object _Lock = new object();
    private static readonly Queue<String> _Recent = new Queue<String>(RecentLineCount);
    private static StreamWriter _Writer = null;
    private static long _Version = 0;

    /// <summary>Incremented on every write, so the UI can tell whether it needs to refresh.</summary>
    public static long Version {
      get {
        lock (_Lock) return _Version;
      }
    }

    /// <summary>Starts a new log file, replacing any previous one. Returns false if the file could not be opened.</summary>
    public static bool Start(String FilePath) {
      lock (_Lock) {
        CloseWriter();
        _Recent.Clear();
        _Version++;
        try {
          String Directory = Path.GetDirectoryName(FilePath);
          if (!String.IsNullOrEmpty(Directory)) System.IO.Directory.CreateDirectory(Directory);
          _Writer = new StreamWriter(FilePath, false, new UTF8Encoding(false), 1 << 16);
          return true;
        } catch {
          _Writer = null; // Keep running with the in-memory view only.
          return false;
        }
      }
    }

    public static void Write(String Line) {
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

    /// <summary>Pushes buffered lines to disk. Called periodically so the log file is readable during a long run.</summary>
    public static void Flush() {
      lock (_Lock) {
        if (_Writer == null) return;
        try {
          _Writer.Flush();
        } catch {
          CloseWriter();
        }
      }
    }

    public static void Stop() {
      lock (_Lock) CloseWriter();
    }

    /// <summary>The most recent lines, joined for display.</summary>
    public static String GetRecentText() {
      lock (_Lock) {
        StringBuilder Text = new StringBuilder(_Recent.Count * 80);
        foreach (String Line in _Recent) Text.Append(Line).Append("\r\n");
        return Text.ToString();
      }
    }

    private static void CloseWriter() {
      if (_Writer == null) return;
      try {
        _Writer.Dispose();
      } catch {}
      _Writer = null;
    }
  }
}
