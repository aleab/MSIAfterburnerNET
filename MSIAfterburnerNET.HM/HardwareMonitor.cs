using MSIAfterburnerNET.Common;
using MSIAfterburnerNET.Common.Exceptions;
using MSIAfterburnerNET.HM.Interop;
using System;

namespace MSIAfterburnerNET.HM
{
    public class HardwareMonitor : IDisposable
    {
        public static uint GPU_GLOBAL_INDEX = uint.MaxValue;

        private HMSharedMemory mmf;

        public HardwareMonitorHeader Header { get; }
        public HardwareMonitorEntry[] Entries { get; private set; }
        public HardwareMonitorGpuEntry[] GpuEntries { get; private set; }

        public HardwareMonitor()
        {
            this.Header = new HardwareMonitorHeader();
            this.Refresh();
        }

        private void LoadEntry(uint index)
        {
            if (index < this.Header.EntryCount)
            {
                long offset = this.Header.HeaderSize + this.Header.EntrySize * index;
                MAHM_SHARED_MEMORY_ENTRY entry = default;
                this.mmf.ReadMAHMEntry(ref entry, offset);
                this.Entries[index].NativeEntry = entry;
            }
        }

        private void LoadGpuEntry(uint gpuIndex)
        {
            if (gpuIndex < this.Header.GpuEntryCount)
            {
                long offset = (uint)((int)this.Header.HeaderSize + (int)this.Header.EntrySize * (int)this.Header.EntryCount + (int)this.Header.GpuEntrySize * (int)gpuIndex);
                MAHM_SHARED_MEMORY_GPU_ENTRY gpuEntry = default;
                this.mmf.ReadMAHMGpuEntry(ref gpuEntry, offset);
                this.GpuEntries[gpuIndex].NativeGpuEntry = gpuEntry;
            }
        }

        private void OpenMemory()
        {
            this.CloseMemory();
            try
            {
                this.mmf = new HMSharedMemory("MAHMSharedMemory", Win32API.FileMapAccess.FileMapAllAccess);
            }
            catch (Exception ex)
            {
                throw new SharedMemoryNotFoundException(ex);
            }
        }

        private void CloseMemory()
        {
            this.mmf?.Dispose();
            this.mmf = null;
            GC.Collect();
        }

        public void Refresh()
        {
            this.RefreshHeader();

            this.Entries = new HardwareMonitorEntry[this.Header.EntryCount];
            for (uint i = 0; i < this.Entries.Length; ++i)
            {
                this.Entries[i] = new HardwareMonitorEntry();
                this.LoadEntry(i);
            }

            this.GpuEntries = new HardwareMonitorGpuEntry[this.Header.GpuEntryCount];
            for (uint i = 0; i < this.GpuEntries.Length; ++i)
            {
                this.GpuEntries[i] = new HardwareMonitorGpuEntry(i);
                this.LoadGpuEntry(i);
            }
        }

        public void RefreshHeader()
        {
            this.OpenMemory();
            MAHM_SHARED_MEMORY_HEADER header = default;
            this.mmf.ReadMAHMHeader(ref header);
            this.Header.NativeHeader = header;
            this.Header.Validate();
        }

        public void RefreshGpuEntry(uint gpuIndex)
        {
            if (gpuIndex > this.Header.GpuEntryCount - 1U)
                throw new ArgumentOutOfRangeException();
            this.LoadGpuEntry(gpuIndex);
        }

        public void RefreshEntry(uint index)
        {
            if (index > this.Header.EntryCount - 1U)
                throw new ArgumentOutOfRangeException();
            this.LoadEntry(index);
        }

        public void RefreshEntry(MONITORING_SOURCE_ID id)
        {
            if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
                throw new ArgumentOutOfRangeException();

            for (uint i = 0; i < this.Header.EntryCount; ++i)
            {
                if (this.Entries[i].SrcId == id)
                    this.LoadEntry(i);
            }
        }

        public void RefreshEntry(uint gpuIndex, MONITORING_SOURCE_ID id)
        {
            if (!this.VerifyGpuIndex(gpuIndex))
                throw new ArgumentOutOfRangeException();
            if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
                throw new ArgumentOutOfRangeException();

            int index = Array.FindIndex(this.Entries, e => e.GPU == gpuIndex && e.SrcId == id);
            if (index >= 0)
                this.LoadEntry((uint)index);
        }

        public void RefreshEntry(uint gpuIndex, string name)
        {
            if (!this.VerifyGpuIndex(gpuIndex))
                throw new ArgumentOutOfRangeException();

            int index = Array.FindIndex(this.Entries, e => e.GPU == gpuIndex && e.SrcName == name);
            if (index >= 0)
                this.LoadEntry((uint)index);
        }

        public void RefreshEntry(HardwareMonitorEntry dataSource)
        {
            int index = Array.FindIndex(this.Entries, e => e == dataSource);
            if (index >= 0)
                this.LoadEntry((uint)index);
        }

        /// <summary>
        /// Returns the first entry with the specified <see cref="MONITORING_SOURCE_ID"/>.
        /// </summary>
        public HardwareMonitorEntry GetEntry(MONITORING_SOURCE_ID id)
        {
            if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
                throw new ArgumentOutOfRangeException();

            return Array.Find(this.Entries, e => e.SrcId == id);
        }


        /// <summary>
        /// Returns the entry with the specified <see cref="MONITORING_SOURCE_ID"/> from the specified GPU.
        /// </summary>
        public HardwareMonitorEntry GetEntry(uint gpuIndex, MONITORING_SOURCE_ID id)
        {
            if (!this.VerifyGpuIndex(gpuIndex))
                throw new ArgumentOutOfRangeException();
            if (!Enum.IsDefined(typeof(MONITORING_SOURCE_ID), id))
                throw new ArgumentOutOfRangeException();

            return Array.Find(this.Entries, e => e.GPU == gpuIndex && e.SrcId == id);
        }

        public HardwareMonitorEntry GetEntry(uint gpuIndex, string name)
        {
            if (!this.VerifyGpuIndex(gpuIndex))
                throw new ArgumentOutOfRangeException();

            return Array.Find(this.Entries, e => e.GPU == gpuIndex && e.SrcName == name);
        }

        private bool VerifyGpuIndex(uint gpuIndex)
        {
            return gpuIndex == uint.MaxValue || gpuIndex <= this.Header.EntryCount - 1U;
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                this.CloseMemory();

                this.disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~HardwareMonitor()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}