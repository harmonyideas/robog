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
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Security.Permissions;
using System.Diagnostics;
using System.Windows.Forms;

namespace RoboG
{
	public class Processes
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool CloseHandle(IntPtr Handle);

		[Flags]
		enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VMOperation = 0x00000008,
			VMRead = 0x00000010,
			VMWrite = 0x00000020,
			DupHandle = 0x00000040,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			Synchronize = 0x00100000
		}

		[DllImport("kernel32.dll")]
		static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

		[DllImport("Psapi.dll", SetLastError = true)]
		static extern bool EnumProcesses([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In][Out] UInt32[] processIds, UInt32 arraySizeBytes, [MarshalAs(UnmanagedType.U4)] out UInt32 bytesCopied);

		[DllImport("psapi.dll")]
		static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

		[DllImport("psapi.dll", SetLastError = true)]
		static extern bool EnumProcessModules(IntPtr hProcess, IntPtr Modules, uint cb, [MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded);

		private const UInt32 TOKEN_QUERY = 0x0008;
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

		enum TOKEN_INFORMATION_CLASS
		{
			/// <summary>
			/// The buffer receives a TOKEN_USER structure that contains the user account of the token.
			/// </summary>
			TokenUser = 1,

			/// <summary>
			/// The buffer receives a TOKEN_GROUPS structure that contains the group accounts associated with the token.
			/// </summary>
			TokenGroups,

			/// <summary>
			/// The buffer receives a TOKEN_PRIVILEGES structure that contains the privileges of the token.
			/// </summary>
			TokenPrivileges,

			/// <summary>
			/// The buffer receives a TOKEN_OWNER structure that contains the default owner security identifier (SID) for newly created objects.
			/// </summary>
			TokenOwner,

			/// <summary>
			/// The buffer receives a TOKEN_PRIMARY_GROUP structure that contains the default primary group SID for newly created objects.
			/// </summary>
			TokenPrimaryGroup,

			/// <summary>
			/// The buffer receives a TOKEN_DEFAULT_DACL structure that contains the default DACL for newly created objects.
			/// </summary>
			TokenDefaultDacl,

			/// <summary>
			/// The buffer receives a TOKEN_SOURCE structure that contains the source of the token. TOKEN_QUERY_SOURCE access is needed to retrieve this information.
			/// </summary>
			TokenSource,

			/// <summary>
			/// The buffer receives a TOKEN_TYPE value that indicates whether the token is a primary or impersonation token.
			/// </summary>
			TokenType,

			/// <summary>
			/// The buffer receives a SECURITY_IMPERSONATION_LEVEL value that indicates the impersonation level of the token. If the access token is not an impersonation token, the function fails.
			/// </summary>
			TokenImpersonationLevel,

			/// <summary>
			/// The buffer receives a TOKEN_STATISTICS structure that contains various token statistics.
			/// </summary>
			TokenStatistics,

			/// <summary>
			/// The buffer receives a TOKEN_GROUPS structure that contains the list of restricting SIDs in a restricted token.
			/// </summary>
			TokenRestrictedSids,

			/// <summary>
			/// The buffer receives a DWORD value that indicates the Terminal Services session identifier that is associated with the token.
			/// </summary>
			TokenSessionId,

			/// <summary>
			/// The buffer receives a TOKEN_GROUPS_AND_PRIVILEGES structure that contains the user SID, the group accounts, the restricted SIDs, and the authentication ID associated with the token.
			/// </summary>
			TokenGroupsAndPrivileges,

			/// <summary>
			/// Reserved.
			/// </summary>
			TokenSessionReference,

			/// <summary>
			/// The buffer receives a DWORD value that is nonzero if the token includes the SANDBOX_INERT flag.
			/// </summary>
			TokenSandBoxInert,

			/// <summary>
			/// Reserved.
			/// </summary>
			TokenAuditPolicy,

			/// <summary>
			/// The buffer receives a TOKEN_ORIGIN value.
			/// </summary>
			TokenOrigin,

			/// <summary>
			/// The buffer receives a TOKEN_ELEVATION_TYPE value that specifies the elevation level of the token.
			/// </summary>
			TokenElevationType,

			/// <summary>
			/// The buffer receives a TOKEN_LINKED_TOKEN structure that contains a handle to another token that is linked to this token.
			/// </summary>
			TokenLinkedToken,

			/// <summary>
			/// The buffer receives a TOKEN_ELEVATION structure that specifies whether the token is elevated.
			/// </summary>
			TokenElevation,

			/// <summary>
			/// The buffer receives a DWORD value that is nonzero if the token has ever been filtered.
			/// </summary>
			TokenHasRestrictions,

			/// <summary>
			/// The buffer receives a TOKEN_ACCESS_INFORMATION structure that specifies security information contained in the token.
			/// </summary>
			TokenAccessInformation,

			/// <summary>
			/// The buffer receives a DWORD value that is nonzero if virtualization is allowed for the token.
			/// </summary>
			TokenVirtualizationAllowed,

			/// <summary>
			/// The buffer receives a DWORD value that is nonzero if virtualization is enabled for the token.
			/// </summary>
			TokenVirtualizationEnabled,

			/// <summary>
			/// The buffer receives a TOKEN_MANDATORY_LABEL structure that specifies the token's integrity level.
			/// </summary>
			TokenIntegrityLevel,

			/// <summary>
			/// The buffer receives a DWORD value that is nonzero if the token has the UIAccess flag set.
			/// </summary>
			TokenUIAccess,

			/// <summary>
			/// The buffer receives a TOKEN_MANDATORY_POLICY structure that specifies the token's mandatory integrity policy.
			/// </summary>
			TokenMandatoryPolicy,

			/// <summary>
			/// The buffer receives the token's logon security identifier (SID).
			/// </summary>
			TokenLogonSid,

			/// <summary>
			/// The maximum value for this enumeration
			/// </summary>
			MaxTokenInfoClass
		}

		private enum TOKEN_ELEVATION_TYPE
		{
			TokenElevationTypeDefault = 1,
			TokenElevationTypeFull = 2,
			TokenElevationTypeLimited = 3
		}

		[DllImport("advapi32.dll", SetLastError = true)]
		static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, int TokenInformationLength, out int ReturnLength);

		[DllImport("kernel32.dll")]
		static extern IntPtr GetCurrentProcess();

		private const int MAX_PATH = 260;
		/*
			Private Const PROCESS_QUERY_INFORMATION As Integer = 1024
			Private Const PROCESS_VM_READ As Integer = 16
    
			Private Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
			Private Const SYNCHRONIZE As Integer = &H100000
			'STANDARD_RIGHTS_REQUIRED Or SYNCHRONIZE Or &HFFF
			Private Const PROCESS_ALL_ACCESS As Integer = &H1F0FFF
			Private Const TH32CS_SNAPPROCESS As Integer = &H2&
			Private Const hNull As Integer = 0
				*/

		public static bool RestartAsAdmin()
		{
			String GUID = Guid.NewGuid().ToString();
			String NewSettingsFile = "Settings" + GUID + ".xml";
			try
			{
				if (GlobalData.SettingsInAppData)
				{
					File.Copy(Settings.Path, IO.GetCommonApplicationDataPath() + "\\" + NewSettingsFile);
				}
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			try
			{
				Process RoboG = new Process();
				RoboG.StartInfo.FileName = Application.ExecutablePath;
				RoboG.StartInfo.Verb = "runas";
				RoboG.StartInfo.Arguments = "/Admin /s";
				if (GlobalData.SettingsInAppData) RoboG.StartInfo.Arguments += " /Settings:" + NewSettingsFile;
				else RoboG.StartInfo.Arguments += " /User:" + HelperFunctions.GetUserName();
				RoboG.Start();
				Environment.Exit(0);
				return true;
			}
			catch (System.ComponentModel.Win32Exception)
			{
				return false;
			}
		}

		public static bool Elevated()
		{
			IntPtr hToken = IntPtr.Zero;
			try
			{
				if (!OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, out hToken)) return false;
				int TokenInformationLength = 0;
				GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenElevationType, IntPtr.Zero, TokenInformationLength, out TokenInformationLength);
				TOKEN_ELEVATION_TYPE ElevationType = 0;
				IntPtr TokenInformation = Marshal.AllocHGlobal(TokenInformationLength);
				if (GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenElevationType, TokenInformation, TokenInformationLength, out TokenInformationLength))
				{
					ElevationType = (TOKEN_ELEVATION_TYPE)Marshal.PtrToStructure(TokenInformation, typeof(Int32));
				}
				Marshal.FreeHGlobal(TokenInformation);
				if (ElevationType == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull) return true;
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			finally
			{
				CloseHandle(hToken);
			}
			return false;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct PROCESS_BASIC_INFORMATION
		{
			public IntPtr ExitStatus;
			public IntPtr PebBaseAddress;
			public IntPtr AffinityMask;
			public IntPtr BasePriority;
			public UIntPtr UniqueProcessId;
			public IntPtr InheritedFromUniqueProcessId;

			public int Size
			{
				get { return (6 * IntPtr.Size); }
			}
		}

		[DllImport("ntdll.dll")]
		private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

		public static Process GetParentProcess(int id)
		{
			try
			{
				Process Process = Process.GetProcessById(id);
				return GetParentProcess(Process.Handle);
			}
			catch (Exception ex)
			{
				HelperFunctions.ShowError(ex);
			}
			return null;
		}

		public static Process GetParentProcess(IntPtr Handle)
		{
			try
			{
				PROCESS_BASIC_INFORMATION PBI = new PROCESS_BASIC_INFORMATION();
				int ReturnLength;
				int Status = NtQueryInformationProcess(Handle, 0, ref PBI, PBI.Size, out ReturnLength);
				if (Status != 0) throw new System.ComponentModel.Win32Exception(Status);

				try
				{
					return Process.GetProcessById(PBI.InheritedFromUniqueProcessId.ToInt32());
				}
				catch (ArgumentException) //not found
				{
					return null;
				}
			}
			catch //Not enough privileges
			{
				return null;
			}

		}

		public static int ProcessPathCount(String ProcessPath)
		{
			uint cb;
			uint cbNeeded;
			int NumElements;
			uint[] ProcessIDs = new uint[] { };
			uint cbNeeded2;
			IntPtr[] Modules = new IntPtr[200];
			bool lRet;
			StringBuilder ModuleName;
			int nSize;
			IntPtr hProcess;
			int ProcessCount = 0;
			//Get the array containing the process id's for each process object
			cb = 8;
			cbNeeded = 96;
			while (cb <= cbNeeded)
			{
				cb *= 2;
				ProcessIDs = new uint[((int)(cb / 4)) - 1];
				lRet = EnumProcesses(ProcessIDs, cb, out cbNeeded);
			}
			NumElements = (int)(cbNeeded / 4);

			for (int i = 0; i < NumElements; i++)
			{
				//Get a handle to the Process
				hProcess = OpenProcess(ProcessAccessFlags.QueryInformation | ProcessAccessFlags.VMRead, false, (int)ProcessIDs[i]);
				//Got a Process handle
				if (hProcess.ToInt64() != 0)
				{
					//Get an array of the module handles for the specified process
					int size = Marshal.SizeOf(Modules[0]) * Modules.Length;
					IntPtr ptr = Marshal.AllocHGlobal(size);
					try
					{
						Marshal.Copy(Modules, 0, ptr, Modules.Length);
						lRet = EnumProcessModules(hProcess, ptr, (uint)200, out cbNeeded2);
						Marshal.Copy(ptr, Modules, 0, Modules.Length);
					}
					finally
					{
						Marshal.FreeHGlobal(ptr);
					}
					//If the Module Array is retrieved, Get the ModuleFileName
					if (lRet)
					{
						ModuleName = new System.Text.StringBuilder(MAX_PATH);
						nSize = MAX_PATH;
						uint ret = GetModuleFileNameEx(hProcess, Modules[0], ModuleName, nSize);
						if (ModuleName.ToString().Substring(0, (int)ret).ToLower() == ProcessPath.ToLower()) ProcessCount++;
					}
				}
				//Close the handle to the process
				CloseHandle(hProcess);
			}
			return ProcessCount;
		}
	}
}
