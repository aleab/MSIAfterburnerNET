using MSIAfterburnerNET.Common;
using MSIAfterburnerNET.Common.SharedMemory;
using MSIAfterburnerNET.HM.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MSIAfterburnerNET.HM
{
    internal class HMSharedMemory : ReadonlyMemory
    {
        public HMSharedMemory(string name, Win32API.FileMapAccess accessLevel) : base(name, accessLevel)
        {
        }

        public void ReadMAHMHeader(ref MAHM_SHARED_MEMORY_HEADER header)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Win32API.MapViewOfFile(this.hMMF, Win32API.FileMapAccess.FileMapRead, 0, Marshal.SizeOf((object)header));
                if (ptr == IntPtr.Zero)
                    throw new Win32Exception();
                header = (MAHM_SHARED_MEMORY_HEADER)Marshal.PtrToStructure(ptr, typeof(MAHM_SHARED_MEMORY_HEADER));
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Win32API.UnmapViewOfFile(ptr);
            }
        }

        public void ReadMAHMEntry(ref MAHM_SHARED_MEMORY_ENTRY entry, long offset)
        {
            IntPtr baseAddress = IntPtr.Zero;
            try
            {
                long entrySize = Marshal.SizeOf((object)entry);
                long ddFileOffset = offset / this.allocationGranularity * this.allocationGranularity;
                offset -= ddFileOffset;

                baseAddress = Win32API.MapViewOfFile(this.hMMF, Win32API.FileMapAccess.FileMapRead, ddFileOffset, Convert.ToInt32(offset + entrySize));
                if (baseAddress == IntPtr.Zero)
                    throw new Win32Exception();

                IntPtr ptr = new IntPtr(baseAddress.ToInt64() + offset);
                entry = (MAHM_SHARED_MEMORY_ENTRY)Marshal.PtrToStructure(ptr, typeof(MAHM_SHARED_MEMORY_ENTRY));
            }
            finally
            {
                if (baseAddress != IntPtr.Zero)
                    Win32API.UnmapViewOfFile(baseAddress);
            }
        }

        public void ReadMAHMGpuEntry(ref MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry, long offset)
        {
            IntPtr lpBaseAddress = IntPtr.Zero;
            try
            {
                long entrySize = Marshal.SizeOf((object)gpuEntry);
                long ddFileOffset = offset / this.allocationGranularity * this.allocationGranularity;
                offset -= ddFileOffset;

                lpBaseAddress = Win32API.MapViewOfFile(this.hMMF, Win32API.FileMapAccess.FileMapRead, ddFileOffset, Convert.ToInt32(offset + entrySize));
                if (lpBaseAddress == IntPtr.Zero)
                    throw new Win32Exception();

                IntPtr ptr = new IntPtr(lpBaseAddress.ToInt64() + offset);
                gpuEntry = (MAHM_SHARED_MEMORY_GPU_ENTRY)Marshal.PtrToStructure(ptr, typeof(MAHM_SHARED_MEMORY_GPU_ENTRY));
            }
            finally
            {
                if (lpBaseAddress != IntPtr.Zero)
                    Win32API.UnmapViewOfFile(lpBaseAddress);
            }
        }
    }
}