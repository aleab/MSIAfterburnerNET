using System;

namespace MSIAfterburnerNET.HM.Interop
{
    [Flags]
    public enum MAHM_SHARED_MEMORY_ENTRY_FLAG : uint
    {
        None = 0,
        SHOW_IN_OSD = 0x00000001,
        SHOW_IN_LCD = 0x00000002,
        SHOW_IN_TRAY = 0x00000004
    }
}