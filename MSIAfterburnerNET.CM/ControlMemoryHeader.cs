using MSIAfterburnerNET.CM.Interop;
using MSIAfterburnerNET.Common.Exceptions;
using System;
using System.Text;

namespace MSIAfterburnerNET.CM
{
    public class ControlMemoryHeader
    {
        public MACM_SHARED_MEMORY_HEADER NativeHeader;

        public ControlMemoryHeader()
        {
        }

        public ControlMemoryHeader(MACM_SHARED_MEMORY_HEADER header)
        {
            this.NativeHeader = header;
        }

        public uint Signature => this.NativeHeader.signature;
        public uint Version => this.NativeHeader.version;
        public uint HeaderSize => this.NativeHeader.headerSize;
        public uint GpuEntryCount => this.NativeHeader.gpuEntryCount;
        public uint GpuEntrySize => this.NativeHeader.gpuEntrySize;
        public uint MasterGpu => this.NativeHeader.masterGpu;
        public MACM_SHARED_MEMORY_FLAG Flags => this.NativeHeader.flags;
        public DateTime Time => new DateTime(1970, 1, 1).AddSeconds(this.NativeHeader.time).ToLocalTime();

        public MACM_SHARED_MEMORY_COMMAND Command
        {
            get { return this.NativeHeader.command; }
            internal set { this.NativeHeader.command = value; }
        }

        public string GetSignatureString()
        {
            char[] charArray = Encoding.ASCII.GetString(BitConverter.GetBytes(this.Signature)).ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public string GetVersionString()
        {
            return $"{this.Version >> 16}.{(short)this.Version}";
        }

        internal void Validate()
        {
            if (this.GetSignatureString() != "MACM")
            {
                if (this.Signature == 57005U)
                    throw new DeadSharedMemoryException();
                throw new InvalidSharedMemoryException();
            }
            if (this.Version < 131073U)
                throw new UnsupportedSharedMemoryVersionException();
        }

        public override string ToString()
        {
            try
            {
                return "Signature = " + this.GetSignatureString() +
                    ";Version = " + this.GetVersionString() +
                    ";HeaderSize = " + this.HeaderSize +
                    ";GpuEntryCount = " + this.GpuEntryCount +
                    ";GpuEntrySize = " + this.GpuEntrySize +
                    ";MasterGpu = " + this.MasterGpu +
                    ";Flags = " + this.Flags.ToString() +
                    ";Time = " + this.Time.ToString("hh:mm:ss MMM-dd-yyyy") +
                    ";Command = " + this.Command.ToString();
            }
            catch
            {
                return base.ToString();
            }
        }
    }
}