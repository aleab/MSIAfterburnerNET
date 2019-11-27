using MSIAfterburnerNET.Common.Extensions;
using MSIAfterburnerNET.HM.Interop;

namespace MSIAfterburnerNET.HM
{
    public class HardwareMonitorEntry
    {
        public MAHM_SHARED_MEMORY_ENTRY? NativeEntry { get; set; }

        public HardwareMonitorEntry()
        {
        }

        public HardwareMonitorEntry(MAHM_SHARED_MEMORY_ENTRY entry)
        {
            this.NativeEntry = entry;
        }

        public string SrcName => this.NativeEntry.HasValue ? new string(this.NativeEntry.Value.srcName).TrimEnd((char)0) : null;
        public string SrcUnits => this.NativeEntry.HasValue ? new string(this.NativeEntry.Value.srcUnits).TrimEnd((char)0) : null;
        public string LocalizedSrcName => this.NativeEntry.HasValue ? new string(this.NativeEntry.Value.localizedSrcName).TrimEnd((char)0) : null;
        public string LocalizedSrcUnits => this.NativeEntry.HasValue ? new string(this.NativeEntry.Value.localizedSrcUnits).TrimEnd((char)0) : null;
        public string RecommendedFormat => this.NativeEntry.HasValue ? new string(this.NativeEntry.Value.recommendedFormat).TrimEnd((char)0) : null;
        public float Data => this.NativeEntry.HasValue && !this.NativeEntry.Value.data.IsAlmostEqual(float.MaxValue, 1E-20f) ? this.NativeEntry.Value.data : 0.0f;
        public float MinLimit => this.NativeEntry?.minLimit ?? 0.0f;
        public float MaxLimit => this.NativeEntry?.maxLimit ?? 0.0f;
        public MAHM_SHARED_MEMORY_ENTRY_FLAG Flags => this.NativeEntry?.flags ?? MAHM_SHARED_MEMORY_ENTRY_FLAG.None;
        public uint ComponentIndex => this.NativeEntry?.index ?? HardwareMonitor.GLOBAL_INDEX;
        public MONITORING_SOURCE_ID SrcId => this.NativeEntry.HasValue ? (MONITORING_SOURCE_ID)this.NativeEntry.Value.srcId : MONITORING_SOURCE_ID.UNKNOWN;

        public override string ToString()
        {
            try
            {
                return "SrcName = " + this.SrcName +
                    ";SrcUnits = " + this.SrcUnits +
                    ";LocalizedSourceName = " + this.LocalizedSrcName +
                    ";LocalizedSrcUnits = " + this.LocalizedSrcUnits +
                    ";RecommendedFormat = " + this.RecommendedFormat +
                    ";Data = " + this.Data.ToString() +
                    ";MinLimit = " + this.MinLimit.ToString() +
                    ";MaxLimit = " + this.MaxLimit.ToString() +
                    ";Flags = " + this.Flags.ToString() +
                    ";ComponentIndex = " + this.ComponentIndex.ToString() +
                    ";SrcId = " + this.SrcId.ToString();
            }
            catch
            {
                return base.ToString();
            }
        }
    }
}