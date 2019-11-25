using System;

namespace MSIAfterburnerNET.Common.Exceptions
{
    public class InvalidSharedMemoryException : Exception
    {
        private const string MSG = "Connected to invalid MSI Afterburner shared memory.";

        public InvalidSharedMemoryException() : base(MSG)
        {
        }

        public InvalidSharedMemoryException(Exception innerException) : base(MSG, innerException)
        {
        }

        public InvalidSharedMemoryException(string message) : base(message)
        {
        }

        public InvalidSharedMemoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}