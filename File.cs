using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboG {
  class File {
    public static void Delete(String Path) {
      String UNCPath = Directory.GetUNCPath(Path);
      HelperFunctions.WriteLog(Translations.Get("DeletingFile") + Path);
      if (!WinAPI.DeleteFile(UNCPath)) WinAPI.ThrowWin32Exception();
    }

    public static void Copy(string SourceFileName, string DestFileName) {
      Copy(SourceFileName, DestFileName, true);
    }

    public static void Copy(string SourceFileName, string DestFileName, bool Overwrite) {
      String UNCPath1 = Directory.GetUNCPath(SourceFileName);
      String UNCPath2 = Directory.GetUNCPath(DestFileName);
      if (!WinAPI.CopyFile(UNCPath1, UNCPath2, !Overwrite)) WinAPI.ThrowWin32Exception();
    }

    public static bool Exists(String Path) {
      String UNCPath = Directory.GetUNCPath(Path);
      WinAPI.WIN32_FIND_DATA Data;
      using(WinAPI.SafeFindHandle FindHandle = WinAPI.FindFirstFile(UNCPath, out Data)) {
        if (FindHandle.IsInvalid) return false;
        else return true;
      }
    }
  }
}
