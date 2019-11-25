using System;

namespace MSIAfterburnerNET.CM.Interop
{
    [Flags]
    public enum MACM_SHARED_MEMORY_FLAG : uint
    {
        None = 0,
        LINK = 0x00000001,
        SYNC = 0x00000002,
        THERMAL = 0x00000004,
    }
}