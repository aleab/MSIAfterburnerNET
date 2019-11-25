using System;

namespace MSIAfterburnerNET.Common.Exceptions
{
    public class DeadSharedMemoryException : Exception
    {
        private const string MSG = "Connected to MSI Afterburner shared memory that is flagged as dead.";

        public DeadSharedMemoryException() : base(MSG)
        {
        }

        public DeadSharedMemoryException(Exception innerException) : base(MSG, innerException)
        {
        }

        public DeadSharedMemoryException(string message) : base(message)
        {
        }

        public DeadSharedMemoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}