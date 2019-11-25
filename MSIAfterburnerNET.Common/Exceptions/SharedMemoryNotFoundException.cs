using System;

namespace MSIAfterburnerNET.Common.Exceptions
{
    public class SharedMemoryNotFoundException : Exception
    {
        private const string MSG = "Could not connect to MSI Afterburner shared memory.";

        public SharedMemoryNotFoundException() : base(MSG)
        {
        }

        public SharedMemoryNotFoundException(Exception innerException) : base(MSG, innerException)
        {
        }

        public SharedMemoryNotFoundException(string message) : base(message)
        {
        }

        public SharedMemoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}