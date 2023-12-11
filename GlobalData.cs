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
using System.Linq;
using System.Text;
using System.IO;

namespace RoboG {
  public static class GlobalData {
    public
    const String BackupPrefix = "Backup";
    public static bool Admin = false;
    public static bool AutoExitCmd = false;
    public static System.Windows.Forms.Form MainWindow;
    public static String BackupVolume = "C:\\TEMP";
    public static String LogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoboG";
    public static Dictionary < String, String > ShadowCopyMounts = new Dictionary < String, String > ();
    public
    const Int64 BufferSpace = 5368709120;
    public static string SystemXMLFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Settings.xml";
    public static int SourceVolumeCount = 0;
    public static List < String > SourceVolumes = new List < String > ();
    public static bool SettingsInAppData = false;
    public static String RobocopyPath = "";
    public static String RobocopyVersion = "";
    public static bool NoPreviousBackup = true;
  }
}
