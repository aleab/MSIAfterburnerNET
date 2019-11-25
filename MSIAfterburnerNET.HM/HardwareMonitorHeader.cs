using MSIAfterburnerNET.Common.Exceptions;
using MSIAfterburnerNET.HM.Interop;
using System;
using System.Text;

namespace MSIAfterburnerNET.HM
{
    public class HardwareMonitorHeader
    {
        public MAHM_SHARED_MEMORY_HEADER? NativeHeader { get; set; }

        public HardwareMonitorHeader()
        {
        }

        public HardwareMonitorHeader(MAHM_SHARED_MEMORY_HEADER header)
        {
            this.NativeHeader = header;
        }

        public uint Signature => this.NativeHeader?.signature ?? 0;
        public uint Version => this.NativeHeader?.version ?? 0;
        public uint HeaderSize => this.NativeHeader?.headerSize ?? 0;
        public uint EntryCount => this.NativeHeader?.entryCount ?? 0;
        public uint EntrySize => this.NativeHeader?.entrySize ?? 0;
        public DateTime Time => new DateTime(1970, 1, 1).AddSeconds(this.NativeHeader?.time ?? 0).ToLocalTime();
        public uint GpuEntryCount => this.NativeHeader?.gpuEntryCount ?? 0;
        public uint GpuEntrySize => this.NativeHeader?.gpuEntrySize ?? 0;

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

        public void Validate()
        {
            if (this.GetSignatureString() != "MAHM")
            {
                if (this.Signature == 57005U)
                    throw new DeadSharedMemoryException();
                throw new InvalidSharedMemoryException();
            }

            if (this.Version < 131072U)
                throw new UnsupportedSharedMemoryVersionException();
        }

        public override string ToString()
        {
            try
            {
                return "Signature = " + this.GetSignatureString() +
                    ";Version = " + this.GetVersionString() +
                    ";HeaderSize = " + this.HeaderSize +
                    ";EntryCount = " + this.EntryCount +
                    ";EntrySize = " + this.EntrySize +
                    ";Time = " + this.Time.ToString("hh:mm:ss MMM-dd-yyyy") +
                    ";GpuEntryCount = " + this.GpuEntryCount +
                    ";GpuEntrySize = " + this.GpuEntrySize;
            }
            catch
            {
                return base.ToString();
            }
        }
    }
}