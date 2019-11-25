using System;

namespace MSIAfterburnerNET.Common.SharedMemory
{
    public interface IWritableMemory : IDisposable
    {
        void Write(object obj, long offset);
        void Write(byte[] buffer, int bytesToWrite, long offset);
    }
}