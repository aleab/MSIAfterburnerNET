using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MSIAfterburnerNET.Common.SharedMemory
{
    public class ReadonlyMemory : IReadableMemory
    {
        protected readonly BinaryFormatter bf = new BinaryFormatter();
        protected readonly uint allocationGranularity;
        protected IntPtr hMMF = IntPtr.Zero;

        public ReadonlyMemory(string name, Win32API.FileMapAccess accessLevel)
        {
            this.hMMF = Win32API.OpenFileMapping(accessLevel, false, name);
            if (this.hMMF == IntPtr.Zero)
                throw new Win32Exception();

            Win32API.SYSTEM_INFO lpSystemInfo = new Win32API.SYSTEM_INFO();
            Win32API.GetSystemInfo(ref lpSystemInfo);
            this.allocationGranularity = lpSystemInfo.dwAllocationGranularity;
        }

        public unsafe object Read(long offset)
        {
            long size = (offset % this.allocationGranularity) + this.allocationGranularity;
            return this.Read(offset, size);
        }

        public unsafe object Read(long offset, long size)
        {
            IntPtr lpBaseAddress = IntPtr.Zero;
            try
            {
                long ddFileOffset = offset / this.allocationGranularity * this.allocationGranularity;
                offset -= ddFileOffset;
                lpBaseAddress = Win32API.MapViewOfFile(this.hMMF, Win32API.FileMapAccess.FileMapRead, ddFileOffset, (int)size);
                if (lpBaseAddress == IntPtr.Zero)
                    throw new Win32Exception();

                using (var unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)(IntPtr)((long)lpBaseAddress.ToPointer() + offset), size, size, FileAccess.Read))
                {
                    return this.bf.Deserialize(unmanagedMemoryStream);
                }
            }
            finally
            {
                if (lpBaseAddress != IntPtr.Zero)
                    Win32API.UnmapViewOfFile(lpBaseAddress);
            }
        }

        public unsafe int Read(byte[] buffer, int bytesToRead, long offset)
        {
            IntPtr lpBaseAddress = IntPtr.Zero;
            try
            {
                long ddFileOffset = offset / this.allocationGranularity * this.allocationGranularity;
                long length = (offset % this.allocationGranularity) + this.allocationGranularity;
                offset -= ddFileOffset;
                lpBaseAddress = Win32API.MapViewOfFile(this.hMMF, Win32API.FileMapAccess.FileMapRead, ddFileOffset, (int)length);
                if (lpBaseAddress == IntPtr.Zero)
                    throw new Win32Exception();

                using (var unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)(IntPtr)((long)lpBaseAddress.ToPointer() + offset), length, length, FileAccess.Read))
                {
                    return unmanagedMemoryStream.Read(buffer, 0, bytesToRead);
                }
            }
            finally
            {
                if (lpBaseAddress != IntPtr.Zero)
                    Win32API.UnmapViewOfFile(lpBaseAddress);
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                if (this.hMMF != IntPtr.Zero)
                    Win32API.CloseHandle(this.hMMF);
                this.hMMF = IntPtr.Zero;

                this.disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ReadonlyMemory()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}