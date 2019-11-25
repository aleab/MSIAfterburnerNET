using MSIAfterburnerNET.CM.Interop;

namespace MSIAfterburnerNET.CM
{
    internal class ControlMemoryGpuEntry
    {
        private int index = -1;
        private bool isMaster;

        public MACM_SHARED_MEMORY_GPU_ENTRY? NativeGpuEntry { get; set; }

        public ControlMemoryGpuEntry()
        {
        }

        public ControlMemoryGpuEntry(MACM_SHARED_MEMORY_GPU_ENTRY gpuEntry)
        {
            this.NativeGpuEntry = gpuEntry;
        }

        public ControlMemoryGpuEntry(ControlMemoryHeader header, int index)
        {
            this.index = index;
            this.isMaster = index == header.MasterGpu;
        }
    }
}