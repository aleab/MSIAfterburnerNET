using System;

namespace MSIAfterburnerNET.CM.Interop
{
    [Serializable]
    public struct MACM_SHARED_MEMORY_VF_POINT_ENTRY
    {
        public uint voltageUv;
        public uint frequency;
        public int frequencyOffset;
    }
}