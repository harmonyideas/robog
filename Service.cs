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
using System.ServiceProcess;

namespace RoboG
{
	/// <summary>
	/// The VSS service is demand-start: Windows starts it when RoboG asks for a snapshot and stops it when idle.
	/// RoboG therefore never starts, stops or reconfigures it (stopping it could break another backup that is running).
	/// </summary>
	class Service
	{
		/// <summary>False only if the service exists and is disabled.</summary>
		public static bool IsServiceUsable(String ServiceName)
		{
			try
			{
				using (ServiceController Controller = new ServiceController(ServiceName))
				{
					return Controller.StartType != ServiceStartMode.Disabled;
				}
			}
			catch
			{
				return true; // If the state cannot be read, let VSS itself report any problem.
			}
		}
	}
}
