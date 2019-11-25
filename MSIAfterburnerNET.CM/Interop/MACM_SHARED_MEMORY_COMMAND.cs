using System;

namespace MSIAfterburnerNET.CM.Interop
{
    [Flags]
    public enum MACM_SHARED_MEMORY_COMMAND : uint
    {
        None = 0,
        INIT = 0x00AB0000,
        FLUSH = 0x00AB0001,
        FLUSH_WITHOUT_APPLYING = 0x00AB0002,
        REFRESH_VF_CURVE = 0x00AB0003
    }
}