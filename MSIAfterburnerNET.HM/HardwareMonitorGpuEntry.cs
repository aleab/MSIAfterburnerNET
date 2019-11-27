using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.HM
{
    public class HardwareMonitorGpuEntry
    {
        public MAHM_SHARED_MEMORY_GPU_ENTRY? NativeGpuEntry { get; set; }
        public uint Index { get; } = HardwareMonitor.GLOBAL_INDEX;

        public HardwareMonitorGpuEntry()
        {
        }

        public HardwareMonitorGpuEntry(uint index)
        {
            this.Index = index;
        }

        public HardwareMonitorGpuEntry(MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry) : this()
        {
            this.NativeGpuEntry = gpuEntry;
        }

        public HardwareMonitorGpuEntry(MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry, uint index) : this(index)
        {
            this.NativeGpuEntry = gpuEntry;
        }

        public string GpuId => this.NativeGpuEntry.HasValue ? new string(this.NativeGpuEntry.Value.gpuId).TrimEnd((char)0) : null;
        public string Family => this.NativeGpuEntry.HasValue ? new string(this.NativeGpuEntry.Value.family).TrimEnd((char)0) : null;
        public string Device => this.NativeGpuEntry.HasValue ? new string(this.NativeGpuEntry.Value.device).TrimEnd((char)0) : null;
        public string Driver => this.NativeGpuEntry.HasValue ? new string(this.NativeGpuEntry.Value.driver).TrimEnd((char)0) : null;
        public string BIOS => this.NativeGpuEntry.HasValue ? new string(this.NativeGpuEntry.Value.BIOS).TrimEnd((char)0) : null;
        public uint MemAmount => this.NativeGpuEntry?.memAmount ?? 0;

        public override string ToString()
        {
            try
            {
                return "GpuId = " + this.GpuId +
                    ";Family = " + this.Family +
                    ";Device = " + this.Device +
                    ";Driver = " + this.Driver +
                    ";BIOS = " + this.BIOS +
                    ";MemAmount = " + this.MemAmount.ToString();
            }
            catch
            {
                return base.ToString();
            }
        }
    }
}