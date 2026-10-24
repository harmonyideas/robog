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
using System.Runtime.InteropServices;

namespace RoboG
{
	/// <summary>
	/// Takes one VSS snapshot set covering all source volumes, so every volume is captured at the same instant,
	/// and makes each snapshot reachable for robocopy through a directory symbolic link under
	/// %ProgramData%\RoboG\Snapshots\&lt;drive letter&gt;.
	///
	/// The snapshots are non-persistent (auto-release): Windows deletes them as soon as this object is disposed,
	/// and also if RoboG crashes or is killed, so nothing is ever left behind using disk space.
	/// The backup is declared as a "copy" backup, which does not change applications' backup history
	/// (SQL Server, Exchange and similar will not truncate their logs because of it).
	///
	/// All methods must be called on the same thread (the backup thread).
	/// </summary>
	class ShadowCopy : IDisposable
	{
		private const uint SYMBOLIC_LINK_FLAG_DIRECTORY = 0x1;

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, uint dwFlags);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool RemoveDirectory(string lpPathName);

		private Vss.IVssBackupComponents _BackupComponents = null;
		private Guid _SnapshotSetId = Guid.Empty;
		private readonly Dictionary<String, Guid> _SnapshotIds = new Dictionary<String, Guid>(StringComparer.OrdinalIgnoreCase);
		private readonly List<String> _Links = new List<String>();
		private bool _SnapshotsCreated = false;

		public static String MountRoot
		{
			get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\RoboG\\Snapshots"; }
		}

		/// <summary>
		/// Snapshots the given volumes ("C:\", "D:\", ...) and fills GlobalData.ShadowCopyMounts with
		/// volume -> mount path. Returns false (after reporting the error) if anything fails.
		/// </summary>
		public bool Create(List<String> Volumes)
		{
			try
			{
				if (Environment.OSVersion.Version.Major < 6)
				{
					HelperFunctions.ShowError(Translations.Get("VssNeedsVista"));
					return false;
				}
				if (!Service.IsServiceUsable("VSS"))
				{
					HelperFunctions.ShowError(Translations.Get("VssServiceDisabled"));
					return false;
				}

				RemoveStaleLinks();
				GlobalData.ShadowCopyMounts.Clear();

				HelperFunctions.WriteLog(Translations.Get("CreatingBackupComponents"));
				_BackupComponents = Vss.CreateBackupComponents();
				HelperFunctions.WriteLog(Translations.Get("InitializingVSSBackup"));
				_BackupComponents.InitializeForBackup(null);
				_BackupComponents.SetContext(Vss.VSS_CTX_BACKUP);
				HelperFunctions.WriteLog(Translations.Get("SettingVSSBackupState"));
				_BackupComponents.SetBackupState(false, false, Vss.BackupType.Copy, false);

				// Asking writers for metadata lets applications (databases, mail servers) flush to disk
				// before the snapshot, so their files are consistent in it.
				HelperFunctions.WriteLog(Translations.Get("GatheringWriterMetadata"));
				Vss.IVssAsync Async;
				_BackupComponents.GatherWriterMetadata(out Async);
				Vss.WaitFor(Async);
				_BackupComponents.FreeWriterMetadata();

				HelperFunctions.WriteLog(Translations.Get("CreatingSnapshotSet"));
				_BackupComponents.StartSnapshotSet(out _SnapshotSetId);
				foreach (String Volume in Volumes)
				{
					HelperFunctions.WriteLog(Translations.Get("AddingToSnapshotSet1") + Volume + Translations.Get("AddingToSnapshotSet2"));
					Guid SnapshotId;
					_BackupComponents.AddToSnapshotSet(Volume, Guid.Empty, out SnapshotId);
					_SnapshotIds[Volume] = SnapshotId;
				}

				HelperFunctions.WriteLog(Translations.Get("PreparingVSSBackup"));
				_BackupComponents.PrepareForBackup(out Async);
				Vss.WaitFor(Async);

				HelperFunctions.WriteLog(Translations.Get("CreatingVSSSnapshots"));
				_BackupComponents.DoSnapshotSet(out Async);
				Vss.WaitFor(Async);
				_SnapshotsCreated = true;

				if (!CheckWriters())
				{
					Dispose();
					return false;
				}

				HelperFunctions.WriteLog(Translations.Get("ExposingSnapshots"));
				System.IO.Directory.CreateDirectory(MountRoot);
				foreach (String Volume in Volumes)
				{
					String Device = Vss.GetSnapshotDevice(_BackupComponents, _SnapshotIds[Volume]);
					if (Device == null) throw new COMException("The shadow copy disappeared right after it was created.", Vss.VSS_E_OBJECT_NOT_FOUND);
					String Link = MountRoot + "\\" + Volume.Substring(0, 1).ToUpperInvariant();
					if (!CreateSymbolicLink(Link, Device + "\\", SYMBOLIC_LINK_FLAG_DIRECTORY))
					{
						throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "Could not create the link " + Link + " to the shadow copy");
					}
					_Links.Add(Link);
					GlobalData.ShadowCopyMounts.Add(Volume, Link + "\\");
					HelperFunctions.WriteLog(Volume + " -> " + Device + " (" + Link + ")");
				}

				HelperFunctions.WriteLog(Translations.Get("SnapshotSuccessful"));
				return true;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(Translations.Get("ShadowCopyError") + "\n\n" + Vss.Describe(ex));
				Dispose();
				return false;
			}
		}

		// Logs how each application writer handled the snapshot. False when a writer failed and the profile says
		// to stop then (after reporting it); a failure to read the statuses is only logged.
		private bool CheckWriters()
		{
			System.Collections.Generic.List<Vss.WriterStatus> Writers;
			try
			{
				Writers = Vss.GetWriterStatuses(_BackupComponents);
			}
			catch (Exception ex)
			{
				HelperFunctions.WriteLog("Could not read the application writers' status: " + ex.Message);
				return true;
			}
			System.Collections.Generic.List<String> Ok = new System.Collections.Generic.List<String>();
			System.Collections.Generic.List<String> Failed = new System.Collections.Generic.List<String>();
			foreach (Vss.WriterStatus Writer in Writers)
			{
				if (Writer.Failed) Failed.Add(Vss.DescribeWriterFailure(Writer));
				else Ok.Add(Writer.Name);
			}
			Ok.Sort(StringComparer.OrdinalIgnoreCase);
			if (Ok.Count > 0) HelperFunctions.WriteLog("Application writers OK (" + Ok.Count + "): " + String.Join(", ", Ok.ToArray()));
			foreach (String Line in Failed) HelperFunctions.WriteLog("Application writer " + Line);
			if (Failed.Count == 0) return true;

			if (Settings.StopIfWriterFails)
			{
				HelperFunctions.ShowError(Translations.Get("WriterFailedStop") + "\n\n" + String.Join("\n", Failed.ToArray()));
				return false;
			}
			HelperFunctions.WriteLog(Translations.Get("WriterFailedContinue"));
			return true;
		}

		/// <summary>
		/// False if Windows has discarded any of the snapshots, typically because shadow storage filled up
		/// during a long copy. Files read after that point will have failed.
		/// </summary>
		public bool IsStillValid()
		{
			if (_BackupComponents == null || !_SnapshotsCreated) return false;
			try
			{
				foreach (Guid SnapshotId in _SnapshotIds.Values)
				{
					if (Vss.GetSnapshotDevice(_BackupComponents, SnapshotId) == null) return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		// Links left behind by a run that crashed point at snapshots that no longer exist.
		// RemoveDirectory on a directory symlink removes only the link, never anything it points to.
		private static void RemoveStaleLinks()
		{
			try
			{
				if (!System.IO.Directory.Exists(MountRoot)) return;
				foreach (String Entry in System.IO.Directory.GetDirectories(MountRoot))
				{
					System.IO.FileAttributes Attributes = System.IO.File.GetAttributes(Entry);
					if ((Attributes & System.IO.FileAttributes.ReparsePoint) != 0) RemoveDirectory(Entry);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.WriteLog("Could not clean up old snapshot links: " + ex.Message);
			}
		}

		public void Dispose()
		{
			foreach (String Link in _Links)
			{
				try { RemoveDirectory(Link); } catch { }
			}
			_Links.Clear();
			GlobalData.ShadowCopyMounts.Clear();

			if (_BackupComponents == null) return;
			try
			{
				if (_SnapshotsCreated)
				{
					// Tell writers the copy is finished, then delete the snapshots explicitly rather than waiting for release.
					Vss.IVssAsync Async;
					_BackupComponents.BackupComplete(out Async);
					Vss.WaitFor(Async);
					HelperFunctions.WriteLog(Translations.Get("DeletingSnapshotSet"));
					int Deleted;
					Guid NotDeleted;
					_BackupComponents.DeleteSnapshots(_SnapshotSetId, Vss.ObjectType.SnapshotSet, true, out Deleted, out NotDeleted);
					HelperFunctions.WriteLog(Translations.Get("SnapshotSetDeleted"));
				}
				else
				{
					_BackupComponents.AbortBackup();
				}
			}
			catch (Exception ex)
			{
				// Releasing the components below still makes Windows delete the auto-release snapshots.
				HelperFunctions.WriteLog("Shadow copy cleanup: " + Vss.Describe(ex));
			}
			finally
			{
				try { Marshal.ReleaseComObject(_BackupComponents); } catch { }
				_BackupComponents = null;
				_SnapshotsCreated = false;
				_SnapshotIds.Clear();
			}
		}
	}
}
