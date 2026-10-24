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
using System.Runtime.InteropServices;

namespace RoboG {
  /// <summary>
  /// Minimal managed bindings for the Windows Volume Shadow Copy requester API (vssapi.dll, Windows Vista and later).
  /// Replaces the AlphaVSS 1.2 native DLLs: no Visual C++ runtime, and it runs on x86, x64 and ARM64.
  /// Interface method order must match vsbackup.h exactly; unused methods are kept as placeholders.
  /// </summary>
  static class Vss {
    public const int VSS_CTX_BACKUP = 0; // Non-persistent, auto-release: Windows deletes the snapshot when RoboG releases it or exits.

    public enum BackupType {
      Undefined = 0,
      Full = 1,
      Incremental = 2,
      Differential = 3,
      Log = 4,
      Copy = 5, // Does not affect application backup history (no log truncation).
      Other = 6
    }

    public enum ObjectType {
      Unknown = 0,
      None = 1,
      SnapshotSet = 2,
      Snapshot = 3
    }

    // Well-known VSS HRESULTs.
    public const int E_ACCESSDENIED = unchecked((int)0x80070005);
    public const int VSS_E_BAD_STATE = unchecked((int)0x80042301);
    public const int VSS_E_PROVIDER_VETO = unchecked((int)0x80042306);
    public const int VSS_E_OBJECT_NOT_FOUND = unchecked((int)0x80042308);
    public const int VSS_E_VOLUME_NOT_SUPPORTED = unchecked((int)0x8004230C);
    public const int VSS_E_VOLUME_NOT_SUPPORTED_BY_PROVIDER = unchecked((int)0x8004230E);
    public const int VSS_E_UNEXPECTED_PROVIDER_ERROR = unchecked((int)0x8004230F);
    public const int VSS_E_FLUSH_WRITES_TIMEOUT = unchecked((int)0x80042313);
    public const int VSS_E_HOLD_WRITES_TIMEOUT = unchecked((int)0x80042314);
    public const int VSS_E_SNAPSHOT_SET_IN_PROGRESS = unchecked((int)0x80042316);
    public const int VSS_E_MAXIMUM_NUMBER_OF_SNAPSHOTS_REACHED = unchecked((int)0x80042317);
    public const int VSS_E_INSUFFICIENT_STORAGE = unchecked((int)0x8004231F);

    // VSS_WRITER_STATE: a writer's state after a VSS event. 6 and up mean the writer failed.
    public enum WriterState {
      Unknown = 0,
      Stable = 1,
      WaitingForFreeze = 2,
      WaitingForThaw = 3,
      WaitingForPostSnapshot = 4,
      WaitingForBackupComplete = 5,
      FailedAtIdentify = 6,
      FailedAtPrepareBackup = 7,
      FailedAtPrepareSnapshot = 8,
      FailedAtFreeze = 9,
      FailedAtThaw = 10,
      FailedAtPostSnapshot = 11,
      FailedAtBackupComplete = 12,
      FailedAtPreRestore = 13,
      FailedAtPostRestore = 14,
      FailedAtBackupShutdown = 15
    }

    // Writer failure codes (the writer's own HRESULT).
    public const int VSS_E_WRITERERROR_INCONSISTENTSNAPSHOT = unchecked((int)0x800423F0);
    public const int VSS_E_WRITERERROR_OUTOFRESOURCES = unchecked((int)0x800423F1);
    public const int VSS_E_WRITERERROR_TIMEOUT = unchecked((int)0x800423F2);
    public const int VSS_E_WRITERERROR_RETRYABLE = unchecked((int)0x800423F3);
    public const int VSS_E_WRITERERROR_NONRETRYABLE = unchecked((int)0x800423F4);
    public const int VSS_E_WRITERERROR_RECOVERY_FAILED = unchecked((int)0x800423F5);

    /// <summary>One application writer (SQL Server, Exchange, Registry, ...) and how it handled the snapshot.</summary>
    public class WriterStatus {
      public String Name;
      public WriterState State;
      public int Failure;
      public bool Failed { get { return State >= WriterState.FailedAtIdentify || Failure < 0; } }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SnapshotProp {
      public Guid SnapshotId;
      public Guid SnapshotSetId;
      public int SnapshotsCount;
      public IntPtr SnapshotDeviceObject;
      public IntPtr OriginalVolumeName;
      public IntPtr OriginatingMachine;
      public IntPtr ServiceMachine;
      public IntPtr ExposedName;
      public IntPtr ExposedPath;
      public Guid ProviderId;
      public int SnapshotAttributes;
      public long CreationTimestamp;
      public int Status;
    }

    [ComImport, Guid("507C37B4-CF5B-4e95-B0AF-14EB9767467E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVssAsync {
      void Cancel();
      void Wait(uint Milliseconds);
      void QueryStatus(out int Result, IntPtr Reserved);
    }

    [ComImport, Guid("665c1d5f-c218-414d-a05d-7fef5f9d5c86"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVssBackupComponents {
      void GetWriterComponentsCount(out uint Count);
      void GetWriterComponents(); // placeholder
      void InitializeForBackup([MarshalAs(UnmanagedType.BStr)] String Xml);
      void SetBackupState([MarshalAs(UnmanagedType.U1)] bool SelectComponents, [MarshalAs(UnmanagedType.U1)] bool BackupBootableSystemState, BackupType Type, [MarshalAs(UnmanagedType.U1)] bool PartialFileSupport);
      void InitializeForRestore(); // placeholder
      void SetRestoreState(); // placeholder
      void GatherWriterMetadata(out IVssAsync Async);
      void GetWriterMetadataCount(out uint Count);
      void GetWriterMetadata(); // placeholder
      void FreeWriterMetadata();
      void AddComponent(); // placeholder
      void PrepareForBackup(out IVssAsync Async);
      void AbortBackup();
      void GatherWriterStatus(out IVssAsync Async);
      void GetWriterStatusCount(out uint Count);
      void FreeWriterStatus();
      void GetWriterStatus(uint Writer, out Guid InstanceId, out Guid WriterId, [MarshalAs(UnmanagedType.BStr)] out String Name, out WriterState State, out int Failure);
      void SetBackupSucceeded(); // placeholder
      void SetBackupOptions(); // placeholder
      void SetSelectedForRestore(); // placeholder
      void SetRestoreOptions(); // placeholder
      void SetAdditionalRestores(); // placeholder
      void SetPreviousBackupStamp(); // placeholder
      void SaveAsXML(); // placeholder
      void BackupComplete(out IVssAsync Async);
      void AddAlternativeLocationMapping(); // placeholder
      void AddRestoreSubcomponent(); // placeholder
      void SetFileRestoreStatus(); // placeholder
      void AddNewTarget(); // placeholder
      void SetRangesFilePath(); // placeholder
      void PreRestore(); // placeholder
      void PostRestore(); // placeholder
      void SetContext(int Context);
      void StartSnapshotSet(out Guid SnapshotSetId);
      void AddToSnapshotSet([MarshalAs(UnmanagedType.LPWStr)] String VolumeName, Guid ProviderId, out Guid SnapshotId);
      void DoSnapshotSet(out IVssAsync Async);
      void DeleteSnapshots(Guid SourceObjectId, ObjectType SourceObjectType, [MarshalAs(UnmanagedType.Bool)] bool ForceDelete, out int DeletedSnapshots, out Guid NondeletedSnapshotId);
      void ImportSnapshots(); // placeholder
      void BreakSnapshotSet(); // placeholder
      void GetSnapshotProperties(Guid SnapshotId, out SnapshotProp Properties);
      // Remaining methods (Query, IsVolumeSupported, ... QueryRevertStatus) are not used and need no declaration.
    }

    [DllImport("vssapi.dll", EntryPoint = "CreateVssBackupComponentsInternal", ExactSpelling = true, PreserveSig = true)]
    private static extern int CreateVssBackupComponentsInternal([MarshalAs(UnmanagedType.Interface)] out IVssBackupComponents BackupComponents);

    [DllImport("vssapi.dll", EntryPoint = "VssFreeSnapshotPropertiesInternal", ExactSpelling = true)]
    private static extern void VssFreeSnapshotPropertiesInternal(ref SnapshotProp Properties);

    public static IVssBackupComponents CreateBackupComponents() {
      IVssBackupComponents BackupComponents;
      int Result = CreateVssBackupComponentsInternal(out BackupComponents);
      if (Result < 0) Marshal.ThrowExceptionForHR(Result);
      return BackupComponents;
    }

    /// <summary>
    /// How each application writer handled the last VSS event (call after DoSnapshotSet). A writer that failed
    /// still leaves a snapshot, but that application's files in it are only crash-consistent: as if the power
    /// had been cut, not flushed and paused.
    /// </summary>
    public static System.Collections.Generic.List<WriterStatus> GetWriterStatuses(IVssBackupComponents BackupComponents) {
      System.Collections.Generic.List<WriterStatus> Writers = new System.Collections.Generic.List<WriterStatus>();
      IVssAsync Async;
      BackupComponents.GatherWriterStatus(out Async);
      WaitFor(Async);
      try {
        uint Count;
        BackupComponents.GetWriterStatusCount(out Count);
        for (uint i = 0; i < Count; i++) {
          Guid Instance, Writer;
          WriterStatus Status = new WriterStatus();
          BackupComponents.GetWriterStatus(i, out Instance, out Writer, out Status.Name, out Status.State, out Status.Failure);
          Writers.Add(Status);
        }
      } finally {
        BackupComponents.FreeWriterStatus();
      }
      return Writers;
    }

    /// <summary>Why a writer failed, in words (English, for the run log).</summary>
    public static String DescribeWriterFailure(WriterStatus Writer) {
      String Why;
      switch (Writer.Failure) {
      case VSS_E_WRITERERROR_INCONSISTENTSNAPSHOT: Why = "its data in the snapshot is inconsistent"; break;
      case VSS_E_WRITERERROR_OUTOFRESOURCES: Why = "it ran out of memory or other resources"; break;
      case VSS_E_WRITERERROR_TIMEOUT: Why = "it took too long to flush and pause writes"; break;
      case VSS_E_WRITERERROR_RETRYABLE: Why = "a temporary error (retrying later may work)"; break;
      case VSS_E_WRITERERROR_NONRETRYABLE: Why = "an error that needs attention (check the application's event log)"; break;
      case VSS_E_WRITERERROR_RECOVERY_FAILED: Why = "it could not recover its data in the snapshot"; break;
      default: Why = Writer.Failure < 0 ? "error 0x" + Writer.Failure.ToString("X8") : "no error code"; break;
      }
      return Writer.Name + ": failed (" + Writer.State + ", " + Why + ")";
    }

    /// <summary>Waits for an asynchronous VSS operation and throws if it failed.</summary>
    public static void WaitFor(IVssAsync Async) {
      try {
        Async.Wait(0xFFFFFFFF);
        int Result;
        Async.QueryStatus(out Result, IntPtr.Zero);
        if (Result < 0) Marshal.ThrowExceptionForHR(Result);
      } finally {
        Marshal.ReleaseComObject(Async);
      }
    }

    /// <summary>Returns the snapshot's device path (\\?\GLOBALROOT\Device\HarddiskVolumeShadowCopyN), or null if it no longer exists.</summary>
    public static String GetSnapshotDevice(IVssBackupComponents BackupComponents, Guid SnapshotId) {
      SnapshotProp Properties;
      try {
        BackupComponents.GetSnapshotProperties(SnapshotId, out Properties);
      } catch (COMException ex) {
        if (ex.ErrorCode == VSS_E_OBJECT_NOT_FOUND) return null;
        throw;
      }
      try {
        return Marshal.PtrToStringUni(Properties.SnapshotDeviceObject);
      } finally {
        VssFreeSnapshotPropertiesInternal(ref Properties);
      }
    }

    /// <summary>A readable explanation for common VSS failures.</summary>
    public static String Describe(Exception ex) {
      COMException Com = ex as COMException;
      int Code = Com != null ? Com.ErrorCode : (ex is UnauthorizedAccessException ? E_ACCESSDENIED : 0);
      String Text;
      switch (Code) {
      case E_ACCESSDENIED:
        Text = Translations.Get("VssAccessDenied");
        break;
      case VSS_E_VOLUME_NOT_SUPPORTED:
      case VSS_E_VOLUME_NOT_SUPPORTED_BY_PROVIDER:
        Text = Translations.Get("VssNotSupported");
        break;
      case VSS_E_INSUFFICIENT_STORAGE:
        Text = Translations.Get("VssNoSpace");
        break;
      case VSS_E_MAXIMUM_NUMBER_OF_SNAPSHOTS_REACHED:
        Text = Translations.Get("VssMaxShadows");
        break;
      case VSS_E_SNAPSHOT_SET_IN_PROGRESS:
        Text = Translations.Get("VssBusy");
        break;
      case VSS_E_FLUSH_WRITES_TIMEOUT:
      case VSS_E_HOLD_WRITES_TIMEOUT:
        Text = Translations.Get("VssFlushTimeout");
        break;
      case VSS_E_PROVIDER_VETO:
      case VSS_E_UNEXPECTED_PROVIDER_ERROR:
        Text = Translations.Get("VssProviderError");
        break;
      default:
        Text = ex.Message;
        break;
      }
      return Code != 0 ? Text + " (0x" + Code.ToString("X8") + ")" : Text;
    }
  }
}
