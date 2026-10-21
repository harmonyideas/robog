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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RoboG
{
	static class Program
	{


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[DllImport("ole32.dll")]
		private static extern int CoInitializeSecurity(IntPtr pSecDesc, int cAuthSvc, IntPtr asAuthSvc, IntPtr pReserved1, uint dwAuthnLevel, uint dwImpLevel, IntPtr pAuthList, uint dwCapabilities, IntPtr pReserved3);

		private const uint RPC_C_AUTHN_LEVEL_PKT_PRIVACY = 6;
		private const uint RPC_C_IMP_LEVEL_IDENTIFY = 2;
		private const uint EOAC_DYNAMIC_CLOAKING = 0x40;

		[STAThread]
		static void Main()
		{
			// VSS writers call back into the requester during a snapshot; this is the COM security
			// Microsoft's VSS requester sample (vshadow) sets. It must run before any other COM call,
			// and failing here (e.g. already initialized) is harmless.
			try
			{
				CoInitializeSecurity(IntPtr.Zero, -1, IntPtr.Zero, IntPtr.Zero, RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_IMP_LEVEL_IDENTIFY, IntPtr.Zero, EOAC_DYNAMIC_CLOAKING, IntPtr.Zero);
			}
			catch { }

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new App());
		}
	}
}
