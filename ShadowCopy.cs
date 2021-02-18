using System;
using System.Collections.Generic;
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

using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Alphaleonis.Win32.Vss;

namespace RoboG
{
	class ShadowCopy : IDisposable
	{
		private const uint DDD_RAW_TARGET_PATH = 0x00000001;
		private const uint DDD_REMOVE_DEFINITION = 0x00000002;
		private const uint DDD_EXACT_MATCH_ON_REMOVE = 0x00000004;
		private const uint DDD_NO_BROADCAST_SYSTEM = 0x00000008;

		[DllImport("kernel32.dll")]
		private static extern bool DefineDosDevice(uint dwFlags, string lpDeviceName, string lpTargetPath);

		[DllImport("kernel32.dll")]
		private static extern bool DeleteVolumeMountPoint(string lpszVolumeMountPoint);

		private String _OldSnapshotSetIdsPath;

		Guid _SnapshotSetId;
		public Guid SnapshotSetId { get { return _SnapshotSetId; } }

		List<Guid> _SnapshotIds = new List<Guid>();
		IVssBackupComponents _BackupComponents = null;

		public bool Create(List<String> Volumes)
		{
			try
			{
				if (OperatingSystemInfo.ProcessorArchitecture == ProcessorArchitecture.X64 || OperatingSystemInfo.ProcessorArchitecture == ProcessorArchitecture.IA64)
				{
					if (!System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\AlphaVSS.60.x64.dll"))
					{
						HelperFunctions.ShowError(Translations.Get("WrongVersion64"));
						Settings.ShadowCopy = false;
						return false;
					}
				}
				else
				{
					if (!System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\AlphaVSS.60.x86.dll"))
					{
						HelperFunctions.ShowError(Translations.Get("WrongVersion32"));
						Settings.ShadowCopy = false;
						return false;
					} 
				}
				if (GlobalData.SettingsInAppData)
				{
					_OldSnapshotSetIdsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\RoboGapp\\SnapshotIds.dat";
				}
				else
				{
					_OldSnapshotSetIdsPath = System.Windows.Forms.Application.StartupPath + "\\SnapshotIds.dat";
				}
				Abort();
				HelperFunctions.WriteLog(Translations.Get("LoadingVSSImplementation"));
				IVssImplementation VssImplementation = null;
				try
				{
					VssImplementation = VssUtils.LoadImplementation();
				}
				catch (Exception ex)
				{
					HelperFunctions.ShowError(Translations.Get("ShadowCopyError") + "\n\n" + Translations.Get("Error") + "\n" + ex.Message);
					if (OperatingSystemInfo.ProcessorArchitecture == ProcessorArchitecture.X64)
					{
						HelperFunctions.ShowError(Translations.Get("VCRedist64"));
					}
					else if (OperatingSystemInfo.ProcessorArchitecture == ProcessorArchitecture.IA64)
					{
						HelperFunctions.ShowError(Translations.Get("VCRedistIA64"));
					}
					else
					{
						HelperFunctions.ShowError(Translations.Get("VCRedist32"));
					}
					return false;
				}
				HelperFunctions.WriteLog(Translations.Get("CreatingBackupComponents"));
				_BackupComponents = VssImplementation.CreateVssBackupComponents();
				HelperFunctions.WriteLog(Translations.Get("InitializingVSSBackup"));
				_BackupComponents.InitializeForBackup(null);
				if (OperatingSystemInfo.OSVersionName > OSVersionName.WindowsServer2003)
				{
					HelperFunctions.WriteLog(Translations.Get("SettingVSSContext"));
					_BackupComponents.SetContext(VssVolumeSnapshotAttributes.Persistent | VssVolumeSnapshotAttributes.NoAutoRelease);
				}
				HelperFunctions.WriteLog(Translations.Get("SettingVSSBackupState"));
				_BackupComponents.SetBackupState(false, true, VssBackupType.Full, false);
				HelperFunctions.WriteLog(Translations.Get("GatheringWriterMetadata"));
				_BackupComponents.GatherWriterMetadata();

				HelperFunctions.WriteLog(Translations.Get("CreatingSnapshotSet"));
				_SnapshotSetId = _BackupComponents.StartSnapshotSet();
				_SnapshotIds.Clear();
				for (int i = 0; i < Volumes.Count; i++)
				{
					HelperFunctions.WriteLog(Translations.Get("AddingToSnapshotSet1") + Volumes[i] + Translations.Get("AddingToSnapshotSet2"));
					Guid SnapshotId = _BackupComponents.AddToSnapshotSet(Volumes[i], Guid.Empty);
					_SnapshotIds.Add(SnapshotId);
				}
				HelperFunctions.WriteLog(Translations.Get("SavingSnapshotSetID"));
				SaveSnapshotSetId();

				HelperFunctions.WriteLog(Translations.Get("PreparingVSSBackup"));
				_BackupComponents.PrepareForBackup();

				HelperFunctions.WriteLog(Translations.Get("CreatingVSSSnapshots"));
				_BackupComponents.DoSnapshotSet();

				HelperFunctions.WriteLog(Translations.Get("ExposingSnapshots"));
				GlobalData.ShadowCopyMounts.Clear();
				List<String> FreeDriveLetters = IO.GetFreeDriveLetters();

				String Path = System.Windows.Forms.Application.StartupPath;
				if (OperatingSystemInfo.OSVersionName > OSVersionName.WindowsServer2003)
				{
					try
					{
						if (System.IO.File.Exists(Path + "\\write.test")) File.Delete(Path + "\\write.test");
						using (System.IO.File.Create(Path + "\\write.test")) { }
						File.Delete(Path + "\\write.test");
					}
					catch
					{
						Path = IO.GetCommonApplicationDataPath();
						if (Path == "")
						{
							HelperFunctions.ShowError(Translations.Get("ExposingSnapshotsDirError"));
							return false;
						}
					}
				}
				for (int i = 0; i < Volumes.Count; i++)
				{
					if (OperatingSystemInfo.OSVersionName <= OSVersionName.WindowsServer2003)
					{
						GlobalData.ShadowCopyMounts.Add(Volumes[i], FreeDriveLetters[i]);
						DefineDosDevice(0, FreeDriveLetters[i].Substring(0, 2), _BackupComponents.GetSnapshotProperties(_SnapshotIds[i]).SnapshotDeviceObject);
					}
					else
					{
						DriveInfo DriveInfo = null;
						if (Path == System.Windows.Forms.Application.StartupPath)
						{
							DriveInfo = DriveInfo.GetDrives().FirstOrDefault((DriveInfo DI) => DI.RootDirectory.ToString() == System.Windows.Forms.Application.StartupPath.Substring(0, 3));
							if (DriveInfo == null || DriveInfo.DriveType == DriveType.Network)
							{
								Path = IO.GetCommonApplicationDataPath();
								if (Path == "")
								{
									HelperFunctions.ShowError(Translations.Get("ExposingSnapshotsDirError"));
									return false;
								}
							}
						}
						GlobalData.ShadowCopyMounts.Add(Volumes[i], Path + "\\mnt" + i.ToString() + "\\");
						if (System.IO.Directory.Exists(GlobalData.ShadowCopyMounts[Volumes[i]]))
						{
							try
							{
								Directory.Delete(GlobalData.ShadowCopyMounts[Volumes[i]], false);
							}
							catch (Exception ex)
							{
								HelperFunctions.ShowError(ex);
								return false;
							}
						}
						Directory.Create(GlobalData.ShadowCopyMounts[Volumes[i]]);
						_BackupComponents.ExposeSnapshot(_SnapshotIds[i], null, VssVolumeSnapshotAttributes.ExposedLocally, GlobalData.ShadowCopyMounts[Volumes[i]]);
					}
				}
				HelperFunctions.WriteLog(Translations.Get("SnapshotSuccessful"));
				return true;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
				return false;
			}
		}

		public void Abort()
		{
			HelperFunctions.WriteLog(Translations.Get("LoadingSnapshotIDs"));
			List<Guid> OldSnapshotSetIds = LoadOldSnapshotSetIds();
			if (OldSnapshotSetIds.Count == 0) return;

			HelperFunctions.WriteLog(Translations.Get("DeletingOldSnapshots"));
			HelperFunctions.WriteLog(Translations.Get("LoadingVSSImplementation"));
			IVssImplementation VssImplementation = VssUtils.LoadImplementation();
			HelperFunctions.WriteLog(Translations.Get("CreatingBackupComponents"));
			using (IVssBackupComponents BackupComponents = VssImplementation.CreateVssBackupComponents())
			{
				HelperFunctions.WriteLog(Translations.Get("InitializingVSSBackup"));
				BackupComponents.InitializeForBackup(null);
				if (OperatingSystemInfo.OSVersionName > OSVersionName.WindowsServer2003)
				{
					HelperFunctions.WriteLog(Translations.Get("SettingVSSContext"));
					BackupComponents.SetContext(VssVolumeSnapshotAttributes.Persistent | VssVolumeSnapshotAttributes.NoAutoRelease);
				}
				HelperFunctions.WriteLog(Translations.Get("QueryingSnapshots"));
				List<Guid> SnapshotSetIds = new List<Guid>();
				foreach (VssSnapshotProperties VssSnapshotProperties in BackupComponents.QuerySnapshots())
				{
					if (OldSnapshotSetIds.IndexOf(VssSnapshotProperties.SnapshotSetId) > -1)
					{
						if (SnapshotSetIds.IndexOf(VssSnapshotProperties.SnapshotSetId) == -1) SnapshotSetIds.Add(VssSnapshotProperties.SnapshotSetId);
						try
						{
							HelperFunctions.WriteLog(Translations.Get("Unexposing") + VssSnapshotProperties.SnapshotId.ToString() + "...");
							if (OperatingSystemInfo.OSVersionName > OSVersionName.WindowsServer2003)
							{
								BackupComponents.UnexposeSnapshot(VssSnapshotProperties.SnapshotId);
							}
						}
						catch
						{
						}
					}
				}
				for (int i = 0; i < SnapshotSetIds.Count; i++)
				{
					try
					{
						HelperFunctions.WriteLog(Translations.Get("DeletingSnapshot") + SnapshotSetIds[i].ToString() + "...");
						BackupComponents.DeleteSnapshotSet(SnapshotSetIds[i], true);
					}
					catch { }
				}
			}

			try
			{
				if (System.IO.File.Exists(_OldSnapshotSetIdsPath)) File.Delete(_OldSnapshotSetIdsPath);
			}
			catch (IOException ex)
			{
				HelperFunctions.ShowError(ex);
			}
		}

		private List<Guid> LoadOldSnapshotSetIds()
		{
			List<Guid> OldSnapshotIds = new List<Guid>();
			try
			{
				if (!System.IO.File.Exists(_OldSnapshotSetIdsPath)) return OldSnapshotIds;
				using (FileStream FileStream = new FileStream(_OldSnapshotSetIdsPath, FileMode.Open))
				{
					using (StreamReader StreamReader = new StreamReader(FileStream))
					{
						String Guid;
						while (!StreamReader.EndOfStream)
						{
							Guid = StreamReader.ReadLine().Trim();
							if (Guid.Length == 36)
							{
								OldSnapshotIds.Add(new Guid(Guid));
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				HelperFunctions.ShowError(ex.Message);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex.Message);
			}
			return OldSnapshotIds;
		}

		private void SaveSnapshotSetId()
		{
			try
			{
				using (FileStream FileStream = new FileStream(_OldSnapshotSetIdsPath, FileMode.Append))
				{
					using (StreamWriter StreamWriter = new StreamWriter(FileStream))
					{
						StreamWriter.WriteLine(_SnapshotSetId);
					}
				}
			}
			catch (IOException ex)
			{
				HelperFunctions.ShowError(ex.Message);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex.Message);
			}
		}

		protected void Dispose(bool Disposing)
		{
			try
			{
				if (Disposing && _BackupComponents != null)
				{
					try
					{
						for (int i = 0; i < _SnapshotIds.Count; i++)
						{
							if (OperatingSystemInfo.OSVersionName <= OSVersionName.WindowsServer2003)
							{
								DeleteVolumeMountPoint(GlobalData.ShadowCopyMounts.Values.ElementAt(i).Substring(0, 2));
							}
							else
							{
								_BackupComponents.UnexposeSnapshot(_SnapshotIds[i]);
								Directory.Delete(GlobalData.ShadowCopyMounts.Values.ElementAt(i));
							}
						}
					}
					catch { }
					HelperFunctions.WriteLog(Translations.Get("DeletingSnapshotSet"));
					_BackupComponents.DeleteSnapshotSet(_SnapshotSetId, true);
					_BackupComponents.Dispose();
					HelperFunctions.WriteLog(Translations.Get("SnapshotSetDeleted"));
					try
					{
						File.Delete(_OldSnapshotSetIdsPath);
					}
					catch (IOException ex)
					{
						HelperFunctions.ShowError(ex);
					}
				}
			}
			catch
			{
			}
		}

		public void Dispose()
		{
			try
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch
			{
			}
		}
	}
}
