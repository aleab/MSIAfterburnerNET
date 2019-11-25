using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MSIAfterburnerNET.Common
{
    public static class Win32API
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpAttributes, FileMapProtection flProtect, int dwMaxSizeHi, int dwMaxSizeLow, string lpName);

        public static IntPtr CreateFileMapping(FileStream File, FileMapProtection flProtect, long ddMaxSize, string lpName)
        {
            int dwMaxSizeHi = (int)(ddMaxSize / int.MaxValue);
            int dwMaxSizeLow = (int)(ddMaxSize % int.MaxValue);
            return CreateFileMapping(File.SafeFileHandle.DangerousGetHandle(), IntPtr.Zero, flProtect, dwMaxSizeHi, dwMaxSizeLow, lpName);
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenFileMapping(FileMapAccess DesiredAccess, bool bInheritHandle, string lpName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMapping, FileMapAccess dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap);

        public static IntPtr MapViewOfFile(IntPtr hFileMapping, FileMapAccess dwDesiredAccess, long ddFileOffset, int dwNumberOfBytesToMap)
        {
            int dwFileOffsetHigh = (int)(ddFileOffset / int.MaxValue);
            int dwFileOffsetLow = (int)(ddFileOffset % int.MaxValue);
            return MapViewOfFile(hFileMapping, dwDesiredAccess, dwFileOffsetHigh, dwFileOffsetLow, dwNumberOfBytesToMap);
        }

        [DllImport("kernel32.dll")]
        public static extern bool FlushViewOfFile(IntPtr lpBaseAddress, int dwNumberOfBytesToFlush);

        [DllImport("kernel32.dll")]
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hFile);

        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo);

        [Flags]
        public enum FileMapProtection : uint
        {
            PageReadonly = 2,
            PageReadWrite = 4,
            PageWriteCopy = 8,
            PageExecuteRead = 32,
            PageExecuteReadWrite = 64,
            SectionCommit = 134217728,
            SectionImage = 16777216,
            SectionNoCache = 268435456,
            SectionReserve = 67108864,
        }

        [Flags]
        public enum FileMapAccess : uint
        {
            FileMapCopy = 1,
            FileMapWrite = 2,
            FileMapRead = 4,
            FileMapAllAccess = 31,
            fileMapExecute = 32,
        }

        public struct SYSTEM_INFO
        {
            internal _PROCESSOR_INFO_UNION uProcessorInfo;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort dwProcessorLevel;
            public ushort dwProcessorRevision;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct _PROCESSOR_INFO_UNION
        {
            [FieldOffset(0)]
            internal uint dwOemId;

            [FieldOffset(0)]
            internal ushort wProcessorArchitecture;

            [FieldOffset(2)]
            internal ushort wReserved;
        }
    }
}