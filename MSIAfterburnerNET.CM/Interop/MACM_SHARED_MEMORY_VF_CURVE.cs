using System;
using System.Runtime.InteropServices;

namespace MSIAfterburnerNET.CM.Interop
{
    [Serializable]
    public struct MACM_SHARED_MEMORY_VF_CURVE
    {
        public uint version;
        public uint flags;
        public uint points;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public MACM_SHARED_MEMORY_VF_POINT_ENTRY[] vfPoints;

        public uint lockIndex;
        public uint powerTuplesSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public MACM_SHARED_MEMORY_POWER_TUPLE_ENTRY[] powerTuples;

        public uint thermalTuplesSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public MACM_SHARED_MEMORY_POWER_TUPLE_ENTRY[] thermalTuples;
    }
}