/*
 * 
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
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace RoboG
{
	public static class HelperFunctions
	{
		private delegate void OneStringParameterDelegate(String Parameter);
		private delegate void TwoStringParametersDelegate(String Parameter1, String Parameter2);
		private delegate void OneExceptionParameterDelegate(Exception Parameter);
		private delegate System.Windows.Forms.DialogResult YesNoQuestionDelegate(String Caption, String Text, Int32 DefaultButtonNumber);


		public static System.Windows.Forms.DialogResult ShowYesNoQuestion(String Caption, String Text, Int32 DefaultButtonNumber)
		{
			if (GlobalData.MainWindow.InvokeRequired)
			{
				return (System.Windows.Forms.DialogResult)GlobalData.MainWindow.Invoke(new YesNoQuestionDelegate(ShowYesNoQuestion), Caption, Text, DefaultButtonNumber);
			}
			else
			{
				System.Windows.Forms.MessageBoxDefaultButton DefaultButton = System.Windows.Forms.MessageBoxDefaultButton.Button1;
				if (DefaultButtonNumber == 2)
				{
					DefaultButton = System.Windows.Forms.MessageBoxDefaultButton.Button2;
				}
				return System.Windows.Forms.MessageBox.Show(GlobalData.MainWindow, Text, Caption, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, DefaultButton);
			}
		}
		public static void ShowError(String Text)
		{
			if (GlobalData.MainWindow != null && GlobalData.MainWindow.InvokeRequired)
			{
				GlobalData.MainWindow.Invoke(new OneStringParameterDelegate(ShowError), Text);
			}
			else
			{
				if (GlobalData.MainWindow != null && !GlobalData.MainWindow.IsDisposed)
				{
					try
					{
						WriteLog(Text);
						if (!GlobalData.MainWindow.IsDisposed && (GlobalData.MainWindow.GetType() != typeof(frmMain) || !Settings.SilentMode)) System.Windows.Forms.MessageBox.Show(GlobalData.MainWindow, Text, "RoboG", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
					}
					catch
					{
					}
				}
				else
				{
					if (!Settings.SilentMode) System.Windows.Forms.MessageBox.Show(Text, "RoboG", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
				}
			}
		}
		public static void ShowMessage(String Text, String Caption)
		{
			if (GlobalData.MainWindow.InvokeRequired)
			{
				GlobalData.MainWindow.Invoke(new TwoStringParametersDelegate(ShowMessage), Text, Caption);
			}
			else
			{
				if (!Settings.SilentMode) System.Windows.Forms.MessageBox.Show(Text, Caption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information); else WriteLog(Text);
			}
		}

		public static void ShowError(Exception e)
		{
			HelperFunctions.ShowError(Translations.Get("ErrorIn") + e.Source + "." + e.TargetSite.Name + ": " + e.Message + "\n\nStacktrace:\n" + e.StackTrace);
		}

		public static void CheckLanguage()
		{
			try
			{
				/*if (!Settings.LanguageSet) {
					frmSelectLanguage frmSelectLanguage = new frmSelectLanguage();
					frmSelectLanguage.ShowDialog();
					if (frmSelectLanguage.Language != "") Settings.Language = frmSelectLanguage.Language; else Environment.Exit(1);
				}*/
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
		}
		public static List<T> CloneList<T>(this List<T> List) where T : ICloneable
		{
			return List.Select(item => (T)item.Clone()).ToList();
		}

		public static String SizeSuffix(long byteCount)
		{
			string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
			if (byteCount == 0)
				return "0" + suf[0];
			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString() + suf[place];
		}

		public static String GetUserName()
		{
			if (Settings.Username == "") return Environment.UserDomainName + "_" + Environment.UserName.Replace(' ', '-'); else return Settings.Username;
		}
		public static System.Xml.XmlTextReader LoadXMLFile(String Filename)
		{
			try
			{
				String xmlText = "";
				int j = 0;
				bool geladen = false;
				while (j < 3 && geladen == false)
				{
					try
					{
						using (System.IO.FileStream fs = new System.IO.FileStream(Filename, System.IO.FileMode.Open, System.IO.FileAccess.Read))
						{
							using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
							{
								xmlText = sr.ReadToEnd();
							}
						}
						geladen = true;
					}
					catch (System.IO.IOException ex)
					{
						System.Threading.Thread.Sleep(1000);
						j++;
						if (j == 3) ShowError(ex);
					}
					catch (UnauthorizedAccessException ex)
					{
						HelperFunctions.ShowError(Translations.Get("LoadError1") + Filename + Translations.Get("LoadError2") + ex.Message);
						break;
					}
					catch (Exception ex)
					{
						System.Threading.Thread.Sleep(1000);
						j++;
						if (j == 3) ShowError(ex);
					}
				}
				System.Xml.NameTable NameTable = new System.Xml.NameTable();
				System.Xml.XmlNamespaceManager NameSpaceManager = new System.Xml.XmlNamespaceManager(NameTable);
				NameSpaceManager.AddNamespace("", "a");
				System.Xml.XmlParserContext ParserContext = new System.Xml.XmlParserContext(null, NameSpaceManager, null, System.Xml.XmlSpace.None);
				return new System.Xml.XmlTextReader(xmlText, System.Xml.XmlNodeType.Element, ParserContext);
			}
			catch (System.IO.IOException ex)
			{
				ShowError(ex);
			}
			catch (System.Xml.XmlException ex)
			{
				ShowError(ex);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
			return null;
		}
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetForegroundWindow(IntPtr hWnd);
		public static void SetForegroundWindow(String WindowTitle, bool Wait)
		{
			IntPtr hWnd = FindWindow(WindowTitle, Wait);
			if (hWnd.ToInt64() != 0)
			{
				SetForegroundWindow(hWnd);
			}
		}
		public static void SetForegroundWindow(String ClassName, String WindowTitle, bool Wait)
		{
			IntPtr hWnd = FindWindow(ClassName, WindowTitle, Wait);
			if (hWnd.ToInt64() != 0)
			{
				SetForegroundWindow(hWnd);
			}
		}
		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		private static IntPtr FindWindow(string windowName, bool wait)
		{
			IntPtr hWnd = FindWindow(null, windowName);
			while (wait && hWnd.ToInt64() == 0)
			{
				System.Threading.Thread.Sleep(500);
				hWnd = FindWindow(null, windowName);
			}
			return hWnd;
		}
		private static IntPtr FindWindow(string classname, string windowName, bool wait)
		{
			IntPtr hWnd = FindWindow(classname, windowName);
			while (wait && hWnd.ToInt64() == 0)
			{
				System.Threading.Thread.Sleep(500);
				hWnd = FindWindow(classname, windowName);
			}
			return hWnd;
		}
		private static LogDelegate _StatusCallback = null;
		public static LogDelegate StatusCallback
		{
			get
			{
				return _StatusCallback;
			}
			set
			{
				_StatusCallback = value;
			}
		}
		public static void UpdateStatus(String Text)
		{
			try
			{
				if (_StatusCallback != null) _StatusCallback.Invoke(Text);
			}
			catch
			{
			}
		}

		public delegate void LogDelegate(String Text);
		private static LogDelegate _LogCallback = null;
		public static LogDelegate LogCallback
		{
			get
			{
				return _LogCallback;
			}
			set
			{
				_LogCallback = value;
			}
		}

		public static void WriteLog(String Text)
		{
			try
			{
				if (_LogCallback != null) _LogCallback.Invoke(Text);
			}
			catch
			{
			}
		}
		public static void WriteLogLine()
		{
			try
			{
				if (_LogCallback != null) _LogCallback.Invoke("______________________________________________________");
			}
			catch
			{
			}
		}
		internal static WinAPI.Platform GetPlatform()
		{
			WinAPI.SYSTEM_INFO sysInfo = new WinAPI.SYSTEM_INFO();
			WinAPI.GetNativeSystemInfo(ref sysInfo);
			//>= Windows XP (5.1)
			switch (sysInfo.wProcessorArchitecture)
			{
				case WinAPI.PROCESSOR_ARCHITECTURE_IA64:
					return WinAPI.Platform.IA64;
				case WinAPI.PROCESSOR_ARCHITECTURE_AMD64:
					return WinAPI.Platform.X64;
				case WinAPI.PROCESSOR_ARCHITECTURE_INTEL:
					return WinAPI.Platform.X86;
				default:
					return WinAPI.Platform.Unknown;
			}
		}
	}
}