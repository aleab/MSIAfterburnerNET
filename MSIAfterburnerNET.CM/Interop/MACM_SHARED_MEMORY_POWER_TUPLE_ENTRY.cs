using System;

namespace MSIAfterburnerNET.CM.Interop
{
    public struct MACM_SHARED_MEMORY_POWER_TUPLE_ENTRY
    {
        public uint powerCur;
        public uint powerDef;
        public uint frequencyCur;
        public uint frequencyDef;
    }
}