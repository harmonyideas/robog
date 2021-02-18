using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Security.Permissions;

namespace RoboG
{
	class WinAPI
	{
		internal const int ERROR_NO_MORE_FILES = 18;
		internal const int MAX_PATH = 260;
		internal const int MAX_ALTERNATE = 14;

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WIN32_FIND_DATA
		{
			public uint dwFileAttributes;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint dwReserved0;
			public uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
			public string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ALTERNATE)]
			public string cAlternateFileName;
		}

		internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Methods
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			internal SafeFindHandle()
				: base(true)
			{
			}

			public SafeFindHandle(IntPtr PreExistingHandle, bool OwnsHandle)
				: base(OwnsHandle)
			{
				base.SetHandle(PreExistingHandle);
			}

			protected override bool ReleaseHandle()
			{
				try
				{
					if (!(IsInvalid || IsClosed))
					{
						return FindClose(this);
					}
				}
				catch { }
				return (IsInvalid || IsClosed);
			}

			protected override void Dispose(bool Disposing)
			{
				try
				{
					if (!(IsInvalid || IsClosed))
					{
						FindClose(this);
					}
					base.Dispose(Disposing);
				}
				catch { }
			}
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteFile(string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern bool GetDiskFreeSpace(string lpRootPathName, out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters, out uint lpTotalNumberOfClusters);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern bool CreateDirectory(string lpNewDirectory, IntPtr lpSecurityAttributes);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern System.IO.FileAttributes GetFileAttributes(string lpFileName);

		[DllImport("kernel32.dll")]
		internal static extern bool SetFileAttributes(string lpFileName, uint dwFileAttributes);

		[DllImport("mpr.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeFindHandle FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool FindNextFile(SafeFindHandle hFindFile, out WIN32_FIND_DATA lpFindFileData);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FindClose(SafeFindHandle hFindFile);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool RemoveDirectory(string lpPathName);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CopyFile(string lpExistingFileName, string lpNewFileName, bool bFailIfExists);

		#region "Platform"
		public enum Platform
		{
			X86,
			X64,
			IA64,
			Unknown
		}

		internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
		internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
		internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
		internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

		[StructLayout(LayoutKind.Sequential)]
		internal struct SYSTEM_INFO
		{
			public ushort wProcessorArchitecture;
			public ushort wReserved;
			public uint dwPageSize;
			public IntPtr lpMinimumApplicationAddress;
			public IntPtr lpMaximumApplicationAddress;
			public UIntPtr dwActiveProcessorMask;
			public uint dwNumberOfProcessors;
			public uint dwProcessorType;
			public uint dwAllocationGranularity;
			public ushort wProcessorLevel;
			public ushort wProcessorRevision;
		};

		[DllImport("kernel32.dll")]
		internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);
		#endregion

		internal static void ThrowWin32Exception()
		{
			System.ComponentModel.Win32Exception ex = new System.ComponentModel.Win32Exception();
			if (ex.NativeErrorCode != 0) throw ex;
		}
	}
}
