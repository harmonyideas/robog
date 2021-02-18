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
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace RoboG
{
	class Settings
	{
		private static string _CommonAppDataFile = "";
		public static string CommonAppDataFile { get { return _CommonAppDataFile; } set { _CommonAppDataFile = value; } }
		private static string _Username = "";
		public static string Username { get { return _Username; } set { _Username = value; } }
		private static string _Path = "";
		public static string Path
		{
			get
			{
				/*
				if (GlobalData.SettingsInAppData)
				{
					if (_CommonAppDataFile == "")
					{
						return IO.GetApplicationDataPath() + "\\Settings.xml";
					}
					else
					{
						return IO.GetCommonApplicationDataPath() + "\\" + _CommonAppDataFile;
					}
				}
				else return Application.StartupPath + "\\" + Environment.UserDomainName + "_" + Environment.UserName + "\\Settings.xml";*/
				return _Path;
			}
		}
		private static string _Language = "";
		public static string Language { get { if (_Language == "") return "en-US"; else return _Language; } set { _Language = value; } }
		public static bool LanguageSet { get { return _Language != ""; } }
		private static string _SmallestDriveLetter = "A:\\";
		public static string SmallestDriveLetter { get { return _SmallestDriveLetter; } }
		private static string _LargestDriveLetter = "Z:\\";
		public static string LargestDriveLetter { get { return _LargestDriveLetter; } }
		private static BackupSources _Sources = new BackupSources();
		public static BackupSources Sources { get { return _Sources; } }
		private static int _ShadowCopy = -1;
		public static bool ShadowCopy
		{
			get
			{
				if (_ShadowCopy == -1)
				{
					_ShadowCopy = 0;
					for (int i = 0; i < _Sources.Count; i++)
					{
						if (_Sources[i].ShadowCopy) _ShadowCopy = 1;
					}
				}
				return _ShadowCopy == 1;
			}
			set { if (value == true) _ShadowCopy = 1; else _ShadowCopy = 0; }
		}

		private static string _Subfolder = "";
		public static string Subfolder { get { return _Subfolder; } set { _Subfolder = value; if (!_Subfolder.EndsWith(@"\") && _Subfolder.Length > 0) _Subfolder += "\\"; } }
		private static string _NetworkShare = "";
		public static string NetworkShare { get { return _NetworkShare; } set { _NetworkShare = value; if (!_NetworkShare.EndsWith(@"\") && _NetworkShare.Length > 0) _NetworkShare += "\\"; } }
		public enum DebugMode { Release, Debug }
		private static DebugMode _StartMode = DebugMode.Release;
		public static DebugMode StartMode { get { return _StartMode; } }

		#region Advanced settings
		private static int _Retries = 3;
		public static int Retries { get { return _Retries; } set { _Retries = value; } }
		private static int _WaitBetweenRetries = 3;
		public static int WaitBetweenRetries { get { return _WaitBetweenRetries; } set { _WaitBetweenRetries = value; } }
		private static bool _AutoExit = false;
		public static bool AutoExit { get { return _AutoExit; } set { _AutoExit = value; } }
		private static bool _AutoExitAlways = false;
		public static bool AutoExitAlways { get { return _AutoExitAlways; } set { _AutoExitAlways = value; } }

		private static String _ProgramBeforeBackup = "";
		public static String ProgramBeforeBackup { get { return _ProgramBeforeBackup; } set { _ProgramBeforeBackup = value; } }
		public static String ArgumentsProgramBeforeBackup { get; set; }
		public static bool ProgramBeforeBackupAdmin { get; set; }

		private static String _ProgramAfterBackup = "";
		public static String ProgramAfterBackup { get { return _ProgramAfterBackup; } set { _ProgramAfterBackup = value; } }
		public static String ArgumentsProgramAfterBackup { get; set; }
		public static bool ProgramAfterBackupAdmin { get; set; }

		private static String _ProgramOnError = "";
		public static String ProgramOnError { get { return _ProgramOnError; } set { _ProgramOnError = value; } }
		public static String ArgumentsProgramOnError { get; set; }
		public static bool ProgramOnErrorAdmin { get; set; }
		public static string _Dir { get; set; }

		private static bool _IncrementalBackup = true;
		public static bool IncrementalBackup { get { return _IncrementalBackup; } set { _IncrementalBackup = value; } }
		private static bool _DeleteFilesBeforeCopying = false;
		public static bool DeleteFilesBeforeCopying { get { return _DeleteFilesBeforeCopying; } set { _DeleteFilesBeforeCopying = value; } }
		private static bool _SilentMode = false;
		private static bool _Scan = true;
		public static bool Scan { get { return _Scan; } set { _Scan = value; } }
		public static bool SilentMode { get { return _SilentMode; } set { _SilentMode = value; } }
		#endregion
		//public static string _MountPointPrefix = "mnt";
		//public static string MountPointPrerix { get { return _MountPointPrefix; } }

		private static void ClearSettings()
		{
			CleanUp();
			_SmallestDriveLetter = @"A:\";
			_LargestDriveLetter = @"Z:\";
			_Sources.Clear();
			_Subfolder = "";
			_StartMode = DebugMode.Release;
		}

		private static string CheckDriveLetter(string DriveLetter)
		{
			switch (DriveLetter.Length)
			{
				case 1:
					return DriveLetter + @":\";
				case 2:
					if (DriveLetter.Substring(1, 1) == @":")
					{
						return DriveLetter + @"\";
					}
					else
					{
						return null;
					}
				case 3:
					if (DriveLetter.Substring(1, 2) == @":\")
					{
						return DriveLetter;
					}
					else
					{
						return null;
					}
			}
			return null;
		}

		public static bool NeedsAdmin()
		{
			try
			{
				if (ShadowCopy) return true;
				foreach (BackupSource Source in _Sources)
				{
					if ((int)Source.CopyFlags > 0 || Source.BackupMode) return true;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return false;
		}

		public static void CleanUp()
		{
			try
			{
				if (_CommonAppDataFile != "" && System.IO.File.Exists(_Path)) File.Delete(_Path);
				String Path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\RoboG";
				if (System.IO.Directory.Exists(Path) && System.IO.Directory.Exists(Path + "\\RoboG"))
				{
					Path = Path + "\\RoboG";
					String[] Files = Directory.GetFiles(Path);
					for (int i = 0; i < Files.Length; i++)
					{
						if (Files[i].StartsWith("Settings") && Files[i].EndsWith(".xml") && Files[i].Length == 48 && Files[i] != _CommonAppDataFile)
						{
							try
							{
								File.Delete(Path + "\\" + Files[i]);
							}
							catch (Exception) { }
						}
					}
				}
			}
			catch (Exception) { }
		}

		public static bool SaveSettings(bool saveFileName)
		{

			try
			{
				if (saveFileName)
				{
					SaveFileDialog saveFileMenuDlg = new SaveFileDialog();
					saveFileMenuDlg.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
					saveFileMenuDlg.FilterIndex = 1;
					saveFileMenuDlg.DefaultExt = "xml";
					saveFileMenuDlg.RestoreDirectory = true;
					if (saveFileMenuDlg.ShowDialog() == DialogResult.OK)
					{
						_Path = saveFileMenuDlg.FileName;
						
						// Get the current configuration associated 
						// with the application
						
						System.Configuration.Configuration config =
						ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
						System.Configuration.AppSettingsSection appSettings = config.AppSettings;

						
						if (appSettings.Settings["SettingsXMLFile"] == null) { appSettings.Settings.Add("SettingsXMLFile", _Path); }

						appSettings.Settings["SettingsXMLFile"].Value = _Path;
						config.Save(ConfigurationSaveMode.Modified);
						ConfigurationManager.RefreshSection("appSettings");
					}
				}

				else
				{
					_Path = GlobalData.SystemXMLFile;
				}
				try
				{

					if (!System.IO.Directory.Exists(new FileInfo(_Path).DirectoryName)) Directory.Create(_Dir);
					if (System.IO.File.Exists(_Path + ".test")) File.Delete(_Path + ".test");
					using (System.IO.File.Create(_Path + ".test")) { }
					File.Delete(_Path + ".test");
				}
				catch
				{
					_Path = IO.GetApplicationDataPath();
					if (_Path == "")
					{
						HelperFunctions.ShowError(Translations.Get("SettingsSaveError"));
						return false;
					}
					GlobalData.SettingsInAppData = true;
				}


				_ShadowCopy = -1;
				using (System.Xml.XmlTextWriter tw = new XmlTextWriter(_Path, System.Text.Encoding.Unicode))
				{
					tw.WriteStartDocument();
					tw.WriteStartElement("Settings");

					tw.WriteStartElement("language");
					tw.WriteString(_Language);
					tw.WriteEndElement();

					tw.WriteStartElement("subfolder");
					tw.WriteString(_Subfolder);
					tw.WriteEndElement();

					tw.WriteStartElement("networkshare");
					tw.WriteString(_NetworkShare);
					tw.WriteEndElement();

					tw.WriteStartElement("retries");
					tw.WriteString(_Retries.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("waitbetweenretries");
					tw.WriteString(_WaitBetweenRetries.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("exitafterbackup");
					tw.WriteString(_AutoExit.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("alwaysexitafterbackup");
					tw.WriteString(_AutoExitAlways.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("programbeforebackup");
					tw.WriteString(ProgramBeforeBackup);
					tw.WriteEndElement();

					tw.WriteStartElement("argumentsprogrambeforebackup");
					tw.WriteString(ArgumentsProgramBeforeBackup);
					tw.WriteEndElement();

					tw.WriteStartElement("programbeforebackupadmin");
					tw.WriteString(ProgramBeforeBackupAdmin.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("programafterbackup");
					tw.WriteString(ProgramAfterBackup);
					tw.WriteEndElement();

					tw.WriteStartElement("argumentsprogramafterbackup");
					tw.WriteString(ArgumentsProgramAfterBackup);
					tw.WriteEndElement();

					tw.WriteStartElement("programafterbackupadmin");
					tw.WriteString(ProgramAfterBackupAdmin.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("programonerror");
					tw.WriteString(ProgramOnError);
					tw.WriteEndElement();

					tw.WriteStartElement("argumentsprogramonerror");
					tw.WriteString(ArgumentsProgramOnError);
					tw.WriteEndElement();

					tw.WriteStartElement("programonerroradmin");
					tw.WriteString(ProgramOnErrorAdmin.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("incrementalbackup");
					tw.WriteString(_IncrementalBackup.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("deletefilesbeforecopying");
					tw.WriteString(_DeleteFilesBeforeCopying.ToString());
					tw.WriteEndElement();

					tw.WriteStartElement("silentmode");
					tw.WriteString(_SilentMode.ToString());
					tw.WriteEndElement();

					for (int i = 0; i < _Sources.Count; i++)
					{
						tw.WriteStartElement("source");

						System.Reflection.PropertyInfo[] Properties = _Sources[i].GetType().GetProperties();
						foreach (System.Reflection.PropertyInfo Property in Properties)
						{
							BackupSource.SaveAttribute[] SaveAttribute = (BackupSource.SaveAttribute[])Property.GetCustomAttributes(typeof(BackupSource.SaveAttribute), false);
							if (SaveAttribute.Length > 0 && !SaveAttribute[0].Save) continue;
							tw.WriteStartElement("Auto" + Property.Name);
							if (Property.PropertyType == typeof(List<String>)) tw.WriteString(String.Join("|", ((List<String>)Property.GetValue(_Sources[i], null)).ToArray()));
							if (Property.PropertyType == typeof(String)) tw.WriteString((String)Property.GetValue(_Sources[i], null));
							if (Property.PropertyType == typeof(long)) tw.WriteString(((long)Property.GetValue(_Sources[i], null)).ToString());
							if (Property.PropertyType == typeof(int)) tw.WriteString(((int)Property.GetValue(_Sources[i], null)).ToString());
							if (Property.PropertyType == typeof(bool)) tw.WriteString(((bool)Property.GetValue(_Sources[i], null)).ToString());
							if (Property.PropertyType == typeof(BackupSource.Attribute)) tw.WriteString(((int)Property.GetValue(_Sources[i], null)).ToString());
							if (Property.PropertyType == typeof(BackupSource.CopyFlag)) tw.WriteString(((int)Property.GetValue(_Sources[i], null)).ToString());
							tw.WriteEndElement();
						}

						tw.WriteEndElement();
					}

					tw.WriteEndElement();
					tw.WriteEndDocument();
					tw.Close();
				}
				return true;
			}
			catch (XmlException ex)
			{
				HelperFunctions.ShowError(ex);
			}
			catch (UnauthorizedAccessException ex)
			{
				HelperFunctions.ShowError(Translations.Get("SaveError") + ex.Message);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			GlobalData.SettingsInAppData = false;
			return false;
		}

		public static bool LoadSettings(bool ErrorWhenNotFound, bool loadFile)
		{
			try
			{
				bool ReadOnly = false;
				_Sources.Clear();
				_Subfolder = "";
				_Path = GlobalData.SystemXMLFile;

				if (loadFile)
				{

					OpenFileDialog FB = new OpenFileDialog();
					FB.Title = Translations.Get("SelectDirectoryToBackup");
					FB.Filter = "XML Files (*.xml)|*.xml";
					FB.FilterIndex = 0;
					FB.DefaultExt = "xml";
					if (FB.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						_Path = FB.FileName;
					}
				}
				else
				{

					// Get the current configuration associated 
					// with the application.

					System.Configuration.Configuration config =
						ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					System.Configuration.AppSettingsSection appSettings = config.AppSettings;

					if (appSettings.Settings["SettingsXMLFile"] != null) { _Path = appSettings.Settings["SettingsXMLFile"].Value.ToString(); }
				}

				if (System.IO.Directory.Exists(new FileInfo(_Path).DirectoryName))
				{
					try
					{
						if (System.IO.File.Exists(_Path + ".test")) File.Delete(_Path + ".test");
						using (System.IO.File.Create(_Path + ".test")) { }
						File.Delete(_Path + ".test");
					}
					catch
					{
						ReadOnly = true;
					}
				}
				if (!System.IO.File.Exists(_Path) || ReadOnly)
				{
					
					if (System.IO.File.Exists(_Path))
					{
						GlobalData.SettingsInAppData = true;
					}
					else
					{
						return false;
					}
				}


				BackupSource BackupSource = new BackupSource();
				System.Reflection.PropertyInfo[] Properties = BackupSource.GetType().GetProperties();

				try
				{
					using (XmlTextReader Rd = HelperFunctions.LoadXMLFile(_Path))
					{
						Rd.WhitespaceHandling = WhitespaceHandling.Significant;
						String Temp;
						int TempInt;
						while (Rd.Read())
						{
							if (Rd.NodeType == XmlNodeType.Element)
							{
								if (Rd.Name.StartsWith("Auto") && Rd.Name.Length > 4)
								{
									IEnumerable<System.Reflection.PropertyInfo> SelectedProperty = from Property in Properties where Property.Name == Rd.Name.Substring(4) select Property;
									foreach (System.Reflection.PropertyInfo Property in SelectedProperty) //Sollte nur eine sein
									{
										try
										{
											if (Property.PropertyType == typeof(List<String>)) Property.SetValue(BackupSource, new List<String>(Rd.ReadString().Split('|')), null);
											if (Property.PropertyType == typeof(String)) Property.SetValue(BackupSource, Rd.ReadString(), null);
											if (Property.PropertyType == typeof(int)) Property.SetValue(BackupSource, int.Parse(Rd.ReadString()), null);
											if (Property.PropertyType == typeof(long)) Property.SetValue(BackupSource, long.Parse(Rd.ReadString()), null);
											if (Property.PropertyType == typeof(bool)) Property.SetValue(BackupSource, bool.Parse(Rd.ReadString()), null);
											if (Property.PropertyType == typeof(BackupSource.Attribute)) Property.SetValue(BackupSource, (BackupSource.Attribute)int.Parse(Rd.ReadString()), null);
											if (Property.PropertyType == typeof(BackupSource.CopyFlag)) Property.SetValue(BackupSource, (BackupSource.CopyFlag)int.Parse(Rd.ReadString()), null);
										}
										catch (Exception ex)
										{
											HelperFunctions.ShowError(ex);
										}
									}
								}
								else
								{
									switch (Rd.Name.ToLower())
									{
										case "language":
											_Language = Rd.ReadString().Trim();
											break;
										case "smallestdriveletter":
											_SmallestDriveLetter = CheckDriveLetter(Rd.ReadString().Trim());
											if (_SmallestDriveLetter == null) _SmallestDriveLetter = @"A:\";
											break;
										case "biggestdriveletter":
											_LargestDriveLetter = CheckDriveLetter(Rd.ReadString().Trim());
											if (_LargestDriveLetter == null) _LargestDriveLetter = @"Z:\";
											break;
										case "source":
											if (BackupSource.SourcePath != "" && BackupSource.Name != "") _Sources.Add(BackupSource);
											BackupSource = new BackupSource();
											break;
										case "subfolder":
											Subfolder = Rd.ReadString().Trim();
											break;
										case "networkshare":
											NetworkShare = Rd.ReadString().Trim();
											break;
										case "retries":
											if (int.TryParse(Rd.ReadString().Trim(), out TempInt)) _Retries = TempInt;
											break;
										case "waitbetweenretries":
											if (int.TryParse(Rd.ReadString().Trim(), out TempInt)) _WaitBetweenRetries = TempInt;
											break;
										case "exitafterbackup":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") _AutoExit = true; else _AutoExit = false;
											break;
										case "alwaysexitafterbackup":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") _AutoExitAlways = true; else _AutoExitAlways = false;
											break;
										case "programbeforebackup":
											ProgramBeforeBackup = Rd.ReadString().Trim();
											break;
										case "argumentsprogrambeforebackup":
											ArgumentsProgramBeforeBackup = Rd.ReadString().Trim();
											break;
										case "programbeforebackupadmin":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") ProgramBeforeBackupAdmin = true; else ProgramBeforeBackupAdmin = false;
											break;
										case "programafterbackup":
											ProgramAfterBackup = Rd.ReadString().Trim();
											break;
										case "argumentsprogramafterbackup":
											ArgumentsProgramAfterBackup = Rd.ReadString().Trim();
											break;
										case "programafterbackupadmin":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") ProgramAfterBackupAdmin = true; else ProgramAfterBackupAdmin = false;
											break;
										case "programonerror":
											ProgramOnError = Rd.ReadString().Trim();
											break;
										case "argumentsprogramonerror":
											ArgumentsProgramOnError = Rd.ReadString().Trim();
											break;
										case "programonerroradmin":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") ProgramOnErrorAdmin = true; else ProgramOnErrorAdmin = false;
											break;
										case "startmode":
											if (Rd.ReadString().Trim().ToLower() == "debug") _StartMode = DebugMode.Debug;
											break;
										case "incrementalbackup":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") _IncrementalBackup = true; else _IncrementalBackup = false;
											break;
										case "deletefilesbeforecopying":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") _DeleteFilesBeforeCopying = true; else _DeleteFilesBeforeCopying = false;
											break;
										case "silentmode":
											Temp = Rd.ReadString().Trim().ToLower();
											if (Temp == "1" || Temp == "true") _SilentMode = true; else _SilentMode = false;
											break;
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					HelperFunctions.ShowError("Please select a valid XML settings file. " + ex.Message);
				}
				if (BackupSource.SourcePath != "")
				{
					_Sources.Add(BackupSource);
				}
				return true;
			}
			catch (XmlException ex)
			{
				HelperFunctions.ShowError(ex.Message);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex.Message);
			}
			GlobalData.SettingsInAppData = false;
			return false;
		}


	}

	public class BackupSource : ICloneable
	{
		public class SaveAttribute : System.Attribute
		{
			public bool Save { get; set; }

			public SaveAttribute(bool Save)
			{
				this.Save = Save;
			}
		}

		public enum Attribute
		{
			None = 0,
			ReadOnly = 1,
			Archive = 2,
			System = 4,
			Hidden = 8,
			Compressed = 16,
			NotIndexed = 32,
			Encrypted = 64,
			Temporary = 128,
			Offline = 256
		}

		public enum CopyFlag
		{
			None = 0,
			Security = 1,
			OwnerInfo = 2,
			AuditingInfo = 4
		}

		private String _Name = "";
		public String Name { get { return _Name; } set { _Name = value.Trim(); } }

		private String _Volume = null;
		[SaveAttribute(false)]
		public String Volume { get { return _Volume; } set { _Volume = value; } }

		private String _TargetPath = null;
		public String TargetPath { get { return _TargetPath; } set { _TargetPath = value; } }

		private bool _ShadowCopy = false;

		public bool ShadowCopy { get { return _ShadowCopy; } set { _ShadowCopy = value; } }

		private bool _LogOnly = false;
		public bool LogOnly { get { return _LogOnly; } set { _LogOnly = value; } }

		private Attribute _AP = Attribute.None;
		public Attribute AP { get { return _AP; } set { _AP = value; } }

		private Attribute _AM = Attribute.None;
		public Attribute AM { get { return _AM; } set { _AM = value; } }

		private Attribute _IA = Attribute.None;
		public Attribute IA { get { return _IA; } set { _IA = value; } }

		private Attribute _XA = Attribute.None;
		public Attribute XA { get { return _XA; } set { _XA = value; } }

		private bool _XC = false;
		public bool XC { get { return _XC; } set { _XC = value; } }

		private bool _XN = false;
		public bool XN { get { return _XN; } set { _XN = value; } }

		private bool _XL = false;
		public bool XL { get { return _XL; } set { _XL = value; } }

		private bool _IT = false;
		public bool IT { get { return _IT; } set { _IT = value; } }

		private bool _MIR = false;
		public bool MIR { get { return _MIR; } set { _MIR = value; } }

		private bool _DisableJob = false;
		public bool DisableJob { get { return _DisableJob; } set { _DisableJob = value; } }

		private CopyFlag _CopyFlags = CopyFlag.None;
		public CopyFlag CopyFlags { get { return _CopyFlags; } set { _CopyFlags = value; } }

		private bool _BackupMode = false;
		public bool BackupMode { get { return _BackupMode; } set { _BackupMode = value; } }

		private bool _M = false;
		public bool M { get { return _M; } set { _M = value; } }

		private long _MAX = -1;
		public long MAX { get { return _MAX; } set { _MAX = value; } }

		private long _MIN = -1;
		public long MIN { get { return _MIN; } set { _MIN = value; } }

		private int _MAXAGE = -1;
		public int MAXAGE { get { return _MAXAGE; } set { _MAXAGE = value; if (_MAXAGE < 10000000 && _MAXAGE >= 1900) _MAXAGE = 1899; } }
		public String GetMAXAGEString()
		{
			return GetDateDaysString(_MAXAGE);
		}

		private int _MINAGE = -1;
		public int MINAGE { get { return _MINAGE; } set { _MINAGE = value; if (_MINAGE < 10000000 && _MINAGE >= 1900) _MINAGE = 1899; } }
		public String GetMINAGEString()
		{
			return GetDateDaysString(_MINAGE);
		}

		private int _MAXLAD = -1;
		public int MAXLAD { get { return _MAXLAD; } set { _MAXLAD = value; if (_MAXLAD < 10000000 && _MAXLAD >= 1900) _MAXLAD = 1899; } }
		public String GetMAXLADString()
		{
			return GetDateDaysString(_MAXLAD);
		}

		private int _MINLAD = -1;
		public int MINLAD { get { return _MINLAD; } set { _MINLAD = value; if (_MINLAD < 10000000 && _MINLAD >= 1900) _MINLAD = 1899; } }
		public String GetMINLADString()
		{
			return GetDateDaysString(_MINLAD);
		}


		private String _SourcePath = "";
		public String SourcePath
		{
			get { return _SourcePath; }
			set
			{
				value = value.Trim();
				if (value.Length < 2) throw new Exception(Translations.Get("InvalidSource"));
				if (value.Length == 2) value += "\\";
				if (value.Length == 3 && !Char.IsLetter(value[0]) && value.Substring(1, 1) != ":" && value.Substring(2) != "\\") throw new Exception(Translations.Get("InvalidSource"));
				if (value.Length > 3 && value.EndsWith("\\")) value = value.Substring(0, value.Length - 1);
				_Volume = value.Substring(0, 3);
				_SourcePath = value;
			}
		}

		[SaveAttribute(false)]
		public String DisplayMember { get { return _Name + " (" + _SourcePath + ")"; } }

		private String _Mount = "";
		[SaveAttribute(false)]
		public String Mount { get { if (_Mount != "") return _Mount; else return _SourcePath; } set { _Mount = value.Trim(); } }

		private bool _NoChanges = false;
		[SaveAttribute(false)]
		public bool NoChanges { get { return _NoChanges; } set { _NoChanges = value; } }

		private List<String> _ExcludedDirectories = new List<string>();
		public List<String> ExcludedDirectories { get { return _ExcludedDirectories; } set { if (value.Count > 0 && value[0] != "") _ExcludedDirectories = value; } }

		private List<String> _ExcludedFiles = new List<string>();
		public List<String> ExcludedFiles { get { return _ExcludedFiles; } set { if (value.Count > 0 && value[0] != "") _ExcludedFiles = value; } }

		public BackupSource()
		{
		}

		public BackupSource(String Source)
		{
			SourcePath = Source;
		}

		public String GetBackupMode()
		{
			String BackupMode = "";

			try
			{

				if (MIR)
				{
					BackupMode = "/UNICODE /MIR /R:";
				}
				else
				{
					BackupMode = "/UNICODE /E /R:";
				}

				if (LogOnly)
				{
					BackupMode = "/L" + " " + BackupMode;
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return BackupMode;
		}



		public String GetRobocopyParameters()
		{
			String Parameters = "";

			if (GlobalData.RobocopyVersion != "XP026")
			{
				Parameters += " /EFSRAW";
			}
			try
			{
				if ((int)AP > 0)
				{
					Parameters += " /A+:";
					if (AP.HasFlag(Attribute.ReadOnly)) Parameters += "R";
					if (AP.HasFlag(Attribute.Archive)) Parameters += "A";
					if (AP.HasFlag(Attribute.System)) Parameters += "S";
					if (AP.HasFlag(Attribute.Hidden)) Parameters += "H";
					if (AP.HasFlag(Attribute.Compressed)) Parameters += "C";
					if (AP.HasFlag(Attribute.NotIndexed)) Parameters += "N";
					if (AP.HasFlag(Attribute.Encrypted)) Parameters += "E";
					if (AP.HasFlag(Attribute.Temporary)) Parameters += "T";
				}
				if ((int)AM > 0)
				{
					Parameters += " /A-:";
					if (AM.HasFlag(Attribute.ReadOnly)) Parameters += "R";
					if (AM.HasFlag(Attribute.Archive)) Parameters += "A";
					if (AM.HasFlag(Attribute.System)) Parameters += "S";
					if (AM.HasFlag(Attribute.Hidden)) Parameters += "H";
					if (AM.HasFlag(Attribute.Compressed)) Parameters += "C";
					if (AM.HasFlag(Attribute.NotIndexed)) Parameters += "N";
					if (AM.HasFlag(Attribute.Encrypted)) Parameters += "E";
					if (AM.HasFlag(Attribute.Temporary)) Parameters += "T";
				}
				if ((int)IA > 0)
				{
					Parameters += " /IA:";
					if (IA.HasFlag(Attribute.ReadOnly)) Parameters += "R";
					if (IA.HasFlag(Attribute.Archive)) Parameters += "A";
					if (IA.HasFlag(Attribute.System)) Parameters += "S";
					if (IA.HasFlag(Attribute.Hidden)) Parameters += "H";
					if (IA.HasFlag(Attribute.Compressed)) Parameters += "C";
					if (IA.HasFlag(Attribute.NotIndexed)) Parameters += "N";
					if (IA.HasFlag(Attribute.Encrypted)) Parameters += "E";
					if (IA.HasFlag(Attribute.Temporary)) Parameters += "T";
					if (IA.HasFlag(Attribute.Offline)) Parameters += "O";
				}
				if ((int)XA > 0)
				{
					Parameters += " /XA:";
					if (XA.HasFlag(Attribute.ReadOnly)) Parameters += "R";
					if (XA.HasFlag(Attribute.Archive)) Parameters += "A";
					if (XA.HasFlag(Attribute.System)) Parameters += "S";
					if (XA.HasFlag(Attribute.Hidden)) Parameters += "H";
					if (XA.HasFlag(Attribute.Compressed)) Parameters += "C";
					if (XA.HasFlag(Attribute.NotIndexed)) Parameters += "N";
					if (XA.HasFlag(Attribute.Encrypted)) Parameters += "E";
					if (XA.HasFlag(Attribute.Temporary)) Parameters += "T";
					if (XA.HasFlag(Attribute.Offline)) Parameters += "O";
				}
				if (M && !ShadowCopy) Parameters += " /M";
				Parameters += GetExcludedFilesString();
				Parameters += GetExcludedDirectoriesString();
				if (MAX > 0) Parameters += " /MAX:" + MAX.ToString();
				if (MIN > 0) Parameters += " /MIN:" + MIN.ToString();
				if (MAXAGE > -1) Parameters += " /MAXAGE:" + MAXAGE.ToString();
				if (MINAGE > -1) Parameters += " /MINAGE:" + MINAGE.ToString();
				if (MAXLAD > -1) Parameters += " /MAXLAD:" + MAXLAD.ToString();
				if (MINLAD > -1) Parameters += " /MINLAD:" + MINLAD.ToString();
				if (XC) Parameters += " /XC";
				if (XN) Parameters += " /XN";
				if (XL && !GlobalData.NoPreviousBackup) Parameters += " /XL";
				if (IT) Parameters += " /IT";
				if ((int)CopyFlags > 0)
				{
					Parameters += " /COPY:DAT";
					if (CopyFlags.HasFlag(CopyFlag.Security)) Parameters += "S";
					if (CopyFlags.HasFlag(CopyFlag.OwnerInfo)) Parameters += "O";
					if (CopyFlags.HasFlag(CopyFlag.AuditingInfo)) Parameters += "U";
				}
				if (_BackupMode) Parameters += " /ZB"; else Parameters += " ";
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return Parameters;
		}

		private String GetExcludedFilesString()
		{
			try
			{
				if (_ExcludedFiles.Count == 0) return "";
				String ExcludedFiles = " /XF";
				for (int i = 0; i < _ExcludedFiles.Count; i++)
				{
					if (_ExcludedFiles[i] == "") continue;
					String ExcludedFile = _ExcludedFiles[i];
					if (GlobalData.ShadowCopyMounts.Count > 0) ExcludedFile = ExcludedFile.Replace(_Volume, GlobalData.ShadowCopyMounts[_Volume]);
					if (ExcludedFile.IndexOf(" ") > -1)
					{
						ExcludedFiles += " \"" + ExcludedFile + "\"";
					}
					else
					{
						ExcludedFiles += " " + ExcludedFile;
					}
				}
				return ExcludedFiles;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return "";
		}

		private String GetExcludedDirectoriesString()
		{
			try
			{
				if (_ExcludedDirectories.Count == 0) return "";
				String ExcludedDirectories = " /XD";
				for (int i = 0; i < _ExcludedDirectories.Count; i++)
				{
					if (_ExcludedDirectories[i] == "") continue;
					String ExcludedDirectory = _ExcludedDirectories[i];
					if (GlobalData.ShadowCopyMounts.Count > 0) ExcludedDirectory = ExcludedDirectory.Replace(_Volume, GlobalData.ShadowCopyMounts[_Volume]);
					if (ExcludedDirectory.IndexOf(" ") > -1)
					{
						ExcludedDirectories += " \"" + ExcludedDirectory + "\"";
					}
					else
					{
						ExcludedDirectories += " " + ExcludedDirectory;
					}
				}
				return ExcludedDirectories;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return "";
		}

		public String GetDateDaysString(int DateDays)
		{
			if (DateDays == -1)
			{
				return "";
			}
			else if (DateDays < 1900)
			{
				return DateDays.ToString() + " days";
			}
			else
			{
				return DateTime.ParseExact(DateDays.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString();
			}
		}

		public object Clone()
		{
			BackupSource BackupSource = (BackupSource)this.MemberwiseClone();
			BackupSource.ExcludedDirectories = HelperFunctions.CloneList(_ExcludedDirectories);
			BackupSource.ExcludedFiles = HelperFunctions.CloneList(_ExcludedFiles);
			return BackupSource;
		}
	}

	public class BackupSources : System.ComponentModel.IListSource
	{
		private List<BackupSource> _BackupSources = new List<BackupSource>();

		public void Add(String Source)
		{
			Add(new BackupSource(Source));
		}

		public void Add(BackupSource BackupSource)
		{
			_BackupSources.Add(BackupSource);
		}

		public int Count
		{
			get
			{
				return _BackupSources.Count;
			}
		}

		public void Clear()
		{
			_BackupSources.Clear();
		}

		public BackupSource ElementAt(int Index)
		{
			return _BackupSources.ElementAt(Index);
		}

		public bool Contains(BackupSource BackupSource)
		{
			return _BackupSources.Contains(BackupSource);
		}

		public BackupSource this[int Index]
		{
			get
			{
				if (Index >= 0 && Index < _BackupSources.Count)
				{
					return _BackupSources[Index];
				}
				return null;
			}
			set
			{
				if (Index >= 0 && Index < _BackupSources.Count) _BackupSources[Index] = value;
			}
		}

		public BackupSource this[String DriveLetter]
		{
			get
			{
				if (DriveLetter.Length == 3)
				{
					for (int i = 0; i < _BackupSources.Count; i++)
					{
						if (_BackupSources[i].Volume == DriveLetter) return _BackupSources[i];
					}
				}
				return null;
			}
			set
			{
				if (DriveLetter.Length == 3)
				{
					for (int i = 0; i < _BackupSources.Count; i++)
					{
						if (_BackupSources[i].Volume == DriveLetter) _BackupSources[i] = value;
					}
				}
			}
		}

		public bool SourceExists(String Folder)
		{
			for (int i = 0; i < _BackupSources.Count; i++)
			{
				if (_BackupSources[i].Name == Folder) return true;
			}
			return false;
		}

		public void RemoveAt(int Index)
		{
			_BackupSources.RemoveAt(Index);
		}

		public int IndexOf(BackupSource item)
		{
			return _BackupSources.IndexOf(item);
		}

		public void Insert(int index, BackupSource item)
		{
			_BackupSources.Insert(index, item);
		}


		public void CopyTo(BackupSource[] array, int arrayIndex)
		{
			_BackupSources.CopyTo(array, arrayIndex);
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(BackupSource item)
		{
			return _BackupSources.Remove(item);
		}

		public IEnumerator<BackupSource> GetEnumerator()
		{
			return _BackupSources.GetEnumerator();
		}

		public bool ContainsListCollection
		{
			get { return false; }
		}

		public System.Collections.IList GetList()
		{
			return _BackupSources;
		}
	}
}
