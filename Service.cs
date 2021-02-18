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
using System.ServiceProcess;
using Microsoft.Win32;

namespace RoboG
{
	class Service
	{
		private static bool _VSSWasStarted = false;
		private static bool _VSSProviderWasStarted = false;
		private static bool _COMSysAppWasEnabled = false;

		public static bool StartVSS()
		{
			HelperFunctions.WriteLog(Translations.Get("StartingVSService"));
			bool WasEnabled = false;
			if (!EnableService("COMSysApp", out WasEnabled)) return false;
			_COMSysAppWasEnabled = WasEnabled;
			bool WasStarted = false;
			if (!StartService("VSS", out WasStarted)) return false;
			_VSSWasStarted = WasStarted;
			if (!StartService("swprv", out WasStarted)) return false;
			_VSSProviderWasStarted = WasStarted;
			return true;
		}

		public static void StopVSS()
		{
			HelperFunctions.WriteLog(Translations.Get("StoppingVSService"));
			if (!_VSSWasStarted) StopService("VSS");
			if (!_VSSProviderWasStarted) StopService("swprv");
			//Keep COM+ System Application Service enabled
			//if (!_COMSysAppWasEnabled) DisableService("COMSysApp");
		}

		public static bool EnableService(String ServiceName, out bool WasEnabled)
		{
			WasEnabled = false;
			try
			{
				RegistryKey Key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + ServiceName, true);
				if ((int)Key.GetValue("Start", 3) == 3)
				{
					WasEnabled = true;
				}
				else
				{
					Key.SetValue("Start", 3);
					System.Threading.Thread.Sleep(500);
				}
				return true;
			}
			catch (Exception)
			{
				HelperFunctions.ShowError(Translations.Get("VSSStartError"));
			}
			return false;
		}

		public static bool DisableService(String ServiceName)
		{
			try
			{
				RegistryKey Key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + ServiceName, true);
				Key.SetValue("Start", 4);
				return true;
			}
			catch (Exception)
			{
				HelperFunctions.ShowError(Translations.Get("VSSStartError"));
			}
			return false;
		}

		public static bool StartService(String ServiceName, out bool WasStarted)
		{
			WasStarted = false;
			ServiceController ServiceController = new ServiceController(ServiceName);
			try
			{
				if (ServiceController.Status == ServiceControllerStatus.Running)
				{
					WasStarted = true;
				}
				else
				{
					ServiceController.Start();
					ServiceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(60000));
				}
				return true;
			}
			catch (Exception)
			{
				HelperFunctions.ShowError(Translations.Get("VSSStartError"));
			}
			return false;
		}

		public static bool StopService(String ServiceName)
		{
			ServiceController ServiceController = new ServiceController(ServiceName);
			try
			{
				ServiceController.Stop();
				ServiceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMilliseconds(60000));
				return true;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return false;
		}
	}
}
