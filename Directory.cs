using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace RoboG {
  class Directory {
    public static void Create(string Path) {
      String UNCPath = GetUNCPath(Path);
      if (!WinAPI.CreateDirectory(UNCPath.Trim(), IntPtr.Zero)) WinAPI.ThrowWin32Exception();
    }

    public static void Delete(String Path, bool Recursive) {
      if (Recursive) {
        DeleteRecursion(Path);
      } else {
        if (Path.EndsWith("\\")) Path = Path.Substring(0, Path.Length - 1);
        String UNCPath = GetUNCPath(Path);
        if (!WinAPI.RemoveDirectory(UNCPath)) WinAPI.ThrowWin32Exception();
      }
    }

    public static void Delete(String Path) {
      DeleteRecursion(Path);
    }

    private static bool DeleteRecursion(String Path) {
      if (Path.EndsWith("\\")) Path = Path.Substring(0, Path.Length - 1);
      HelperFunctions.WriteLog(Translations.Get("DeletingDirectory") + Path);
      String UNCPath = GetUNCPath(Path);
      List < String > Directories = new List < String > ();
      WinAPI.WIN32_FIND_DATA FindData;
      using(WinAPI.SafeFindHandle FindHandle = WinAPI.FindFirstFile(UNCPath + @ "\*", out FindData)) {
        if (!FindHandle.IsInvalid) {
          do {
            if ((FindData.dwFileAttributes & (uint) System.IO.FileAttributes.Directory) != 0) {
              if (FindData.cFileName != "." && FindData.cFileName != "..") {
                if (!DeleteRecursion(Path + "\\" + FindData.cFileName)) return false;
                //if (!WinAPI.RemoveDirectory(UNCPath + "\\" + FindData.cFileName)) WinAPI.ThrowWin32Exception();
              }
            } else {
              if ((FindData.dwFileAttributes & (uint) System.IO.FileAttributes.ReadOnly) != 0) WinAPI.SetFileAttributes(UNCPath + "\\" + FindData.cFileName, FindData.dwFileAttributes ^ (uint) System.IO.FileAttributes.ReadOnly);
              File.Delete(UNCPath + "\\" + FindData.cFileName);
            }
          }
          while (WinAPI.FindNextFile(FindHandle, out FindData));
          if (Marshal.GetLastWin32Error() != WinAPI.ERROR_NO_MORE_FILES) {
            return false;
          }
        }
      }
      return WinAPI.RemoveDirectory(UNCPath); //Delete empty directory
    }

    public static String[] GetDirectories(string Path) {
      List < String > Directories = new List < String > ();
      try {
        if (Path.EndsWith("\\")) Path = Path.Substring(0, Path.Length - 1);
        String UNCPath = GetUNCPath(Path);
        WinAPI.WIN32_FIND_DATA FindData;
        using(WinAPI.SafeFindHandle FindHandle = WinAPI.FindFirstFile(UNCPath + @ "\*", out FindData)) {
          if (!FindHandle.IsInvalid) {
            do {
              if ((FindData.dwFileAttributes & (uint) System.IO.FileAttributes.Directory) != 0) {
                if (FindData.cFileName != "." && FindData.cFileName != "..") {
                  Directories.Add(FindData.cFileName);
                }
              }
            }
            while (WinAPI.FindNextFile(FindHandle, out FindData));
          }
        }
      } catch (Exception ex) {
        HelperFunctions.ShowError(ex);
      }
      return Directories.ToArray();
    }

    public static String[] GetFiles(string Path) {
      List < String > Files = new List < String > ();
      if (Path.EndsWith("\\")) Path = Path.Substring(0, Path.Length - 1);
      String UNCPath = GetUNCPath(Path);
      WinAPI.WIN32_FIND_DATA FindData;
      using(WinAPI.SafeFindHandle FindHandle = WinAPI.FindFirstFile(UNCPath + @ "\*", out FindData)) {
        if (!FindHandle.IsInvalid) {
          do {
            if ((FindData.dwFileAttributes & (uint) System.IO.FileAttributes.Directory) == 0) {
              if (FindData.cFileName != "." && FindData.cFileName != "..") {
                Files.Add(FindData.cFileName);
              }
            }
          }
          while (WinAPI.FindNextFile(FindHandle, out FindData));
        }
      }
      return Files.ToArray();
    }

    public static bool IsWriteable(string Path) {
      if (!Path.EndsWith("\\")) Path += "\\";
      if (!System.IO.Directory.Exists(Path)) return false;
      String TestDir = "test" + Guid.NewGuid().ToString();
      String TestFile = TestDir + ".dat";
      try {
        using(System.IO.File.Create(Path + TestFile)) {};
        File.Delete(Path + TestFile);
        Directory.Create(TestDir);
        Directory.Delete(TestDir);
        return true;
      } catch {
        return false;
      }
    }

    public static bool TestAttributes(string Path) {
      if (!Path.EndsWith("\\")) Path += "\\";
      if (!System.IO.Directory.Exists(Path)) return false;
      String TestFile = "test" + Guid.NewGuid().ToString() + ".dat";
      String UNCPath = GetUNCPath(Path + TestFile);
      try {
        using(System.IO.File.Create(Path + TestFile)) {};
      } catch {
        return false;
      }
      try {
        WinAPI.SetFileAttributes(UNCPath, (uint) System.IO.FileAttributes.Normal);
        System.IO.FileAttributes Attributes = (System.IO.FileAttributes) WinAPI.GetFileAttributes(UNCPath);
        if (!Attributes.HasFlag(System.IO.FileAttributes.Normal)) return false;

        //WinAPI.SetFileAttributes(UNCPath, (uint)System.IO.FileAttributes.Archive + (uint)System.IO.FileAttributes.Hidden + (uint)System.IO.FileAttributes.ReadOnly + (uint)System.IO.FileAttributes.System);
        WinAPI.SetFileAttributes(UNCPath, (uint) System.IO.FileAttributes.Archive);
        Attributes = (System.IO.FileAttributes) WinAPI.GetFileAttributes(UNCPath);
        //if (!Attributes.HasFlag(System.IO.FileAttributes.Archive) || !Attributes.HasFlag(System.IO.FileAttributes.Hidden) || !Attributes.HasFlag(System.IO.FileAttributes.ReadOnly) || !Attributes.HasFlag(System.IO.FileAttributes.System)) return false;
        if (!Attributes.HasFlag(System.IO.FileAttributes.Archive)) return false;

        return true;
      } catch {
        return false;
      } finally {
        try {
          WinAPI.SetFileAttributes(UNCPath, (uint) System.IO.FileAttributes.Normal); //Cannot delete file if it is readonly
        } catch {}
        try {
          File.Delete(UNCPath);
        } catch {}
      }
    }

    public static String GetUNCPath(String Path) {
      if (Path.StartsWith(@ "\\?\")) {
            return Path;
          } else {
            if (Path.StartsWith(@ "\\")) {
              return @ "\\?\UNC" + Path.Substring(1);
            } else {
              return @ "\\?\" + Path;
            }
          }
        }

      }
    }
