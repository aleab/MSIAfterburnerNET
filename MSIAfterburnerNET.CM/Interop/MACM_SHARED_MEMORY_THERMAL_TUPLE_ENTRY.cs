using System;

namespace MSIAfterburnerNET.CM.Interop
{
    [Serializable]
    public struct MACM_SHARED_MEMORY_THERMAL_TUPLE_ENTRY
    {
        public uint temperatureCur;
        public uint temperatureDef;
        public uint frequencyCur;
        public uint frequencyDef;
    }
}