using System;

namespace MSIAfterburnerNET.Common.Exceptions
{
    public class UnsupportedSharedMemoryVersionException : Exception
    {
        private const string MSG = "Connected to an unsupported version of MSI Afterburner shared memory.";

        public UnsupportedSharedMemoryVersionException() : base(MSG)
        {
        }

        public UnsupportedSharedMemoryVersionException(Exception innerException) : base(MSG, innerException)
        {
        }

        public UnsupportedSharedMemoryVersionException(string message) : base(message)
        {
        }

        public UnsupportedSharedMemoryVersionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}