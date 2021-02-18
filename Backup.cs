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
using System.Windows.Forms;

namespace RoboG
{
	static class Backup
	{
		private static bool _Abort = false;
		private static bool _MirOptionSet = false;

		public delegate void StartingBackupEventHandler();
		public static event StartingBackupEventHandler StartingBackup;

		public delegate void SizeDeterminedEventHandler(Int64 Size);
		public static event SizeDeterminedEventHandler SizeDetermined;

		private static List<String> _BackupDirectories = new List<String>();
		private static List<String> _TargetDirectories = new List<String>();

		public static void Initialize()
		{
			IO.Initialize();
			_TargetDirectories = IO.GetBackupRoot(true);
		}

		public static bool DoBackup()
		{
			try
			{
				_Abort = false;
				_MirOptionSet = false;

				for (int i = 0; i < Settings.Sources.Count; i++)
				{
					Settings.Sources[i].NoChanges = false;

					if (Settings.Sources[i].MIR)
					{
						_MirOptionSet = true;
					}
				}
				/* if (_MirOptionSet)
				{
					if (HelperFunctions.ShowYesNoQuestion("RoboG", Translations.Get("WarningMIR"), 2) != System.Windows.Forms.DialogResult.Yes)
					{
						_Abort = true;
					}
				} */

				for (int i = 0; i < _TargetDirectories.Count; i++)
				{

					if (_Abort) return false;

					if (_TargetDirectories[i] == null) return false;
					if (!Directory.IsWriteable(_TargetDirectories[i]) && !System.IO.Directory.Exists(_TargetDirectories[i] + Settings.Subfolder))
					{
						HelperFunctions.ShowError(Translations.Get("DirectoryNotWriteable1") + _TargetDirectories[i] + Translations.Get("DirectoryNotWriteable2"));
						return false;
					}
					if (Settings.Subfolder != "" && !System.IO.Directory.Exists(_TargetDirectories[i] + Settings.Subfolder)) Directory.Create(_TargetDirectories[i] + Settings.Subfolder);
					if (!Directory.IsWriteable(_TargetDirectories[i] + Settings.Subfolder))
					{
						HelperFunctions.ShowError(Translations.Get("DirectoryNotWriteable1") + _TargetDirectories[i] + Settings.Subfolder + Translations.Get("DirectoryNotWriteable2"));
						return false;
					}
					if (!Directory.TestAttributes(_TargetDirectories[i] + Settings.Subfolder))
					{
						HelperFunctions.ShowError(Translations.Get("DirectoryNoAttributes1") + _TargetDirectories[i] + Settings.Subfolder + Translations.Get("DirectoryNoAttributes2"));
						return false;
					}
				}

				if (_Abort) return false;
				HelperFunctions.UpdateStatus(Translations.Get("LookingForChanges"));
				HelperFunctions.WriteLog(Translations.Get("LookingForChanges"));

				if (_BackupDirectories.Count > 0) GlobalData.NoPreviousBackup = false;

				Int64 SpaceNeeded = 0;
				bool NoChanges = true;

				if (Settings.Scan)
				{

					for (int i = 0; i < Settings.Sources.Count; i++)
					{
						long Space = IO.GetSpaceNeeded(Settings.Sources[i]);
						if (Space == -1) return false;
						SpaceNeeded += Space;
						if (!Settings.Sources[i].NoChanges) NoChanges = false;
					}



					if (_Abort) return false;

					if (SizeDetermined != null) SizeDetermined(SpaceNeeded);

					if (NoChanges)
					{
						HelperFunctions.WriteLog(Translations.Get("NoChanges"));
						return true;
					}
				}
				if (_Abort) return false;
				if (StartingBackup != null) StartingBackup();

				HelperFunctions.UpdateStatus(Translations.Get("BackingUp") + HelperFunctions.SizeSuffix(SpaceNeeded) + "...");


				for (int i = 0; i < Settings.Sources.Count; i++)
				{
					if (_Abort) return false;
                    HelperFunctions.WriteLog(Translations.Get("BackingUpSource") + (i + 1) + "...");
					
                    if (Settings.Sources[i].NoChanges)
					{
						HelperFunctions.WriteLog(Translations.Get("NoChanges"));
						continue;
					}

					IO.MirrorDirectory(Settings.Sources[i]);
				}

				HelperFunctions.WriteLog(Translations.Get("BackupCompleted"));
				return true;
			}
			catch (IOException ex)
			{
				HelperFunctions.ShowError(ex);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return false;
		}

		public static bool Abort()
		{
			_Abort = true;
			IO.Abort();
			return true;

		}
	}
}
