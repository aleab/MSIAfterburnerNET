using System;

namespace MSIAfterburnerNET.Common.SharedMemory
{
    public interface IReadableMemory : IDisposable
    {
        object Read(long offset);

        object Read(long offset, long size);

        int Read(byte[] buffer, int bytesToRead, long offset);
    }
}