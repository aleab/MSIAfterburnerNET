﻿namespace MSIAfterburnerNET.HM.Interop
{
    public enum MONITORING_SOURCE_ID : uint
    {
        GPU_TEMPERATURE = 0x00000000,
        PCB_TEMPERATURE = 0x00000001,
        MEM_TEMPERATURE = 0x00000002,
        VRM_TEMPERATURE = 0x00000003,
        FAN_SPEED = 0x00000010,
        FAN_TACHOMETER = 0x00000011,
        FAN_SPEED2 = 0x00000012,
        FAN_TACHOMETER2 = 0x00000013,
        FAN_SPEED3 = 0x00000014,
        FAN_TACHOMETER3 = 0x00000015,
        CORE_CLOCK = 0x00000020,
        SHADER_CLOCK = 0x00000021,
        MEMORY_CLOCK = 0x00000022,
        GPU_USAGE = 0x00000030,
        MEMORY_USAGE = 0x00000031,
        FB_USAGE = 0x00000032,
        VID_USAGE = 0x00000033,
        BUS_USAGE = 0x00000034,
        GPU_VOLTAGE = 0x00000040,
        AUX_VOLTAGE = 0x00000041,
        MEMORY_VOLTAGE = 0x00000042,
        AUX2_VOLTAGE = 0x00000043,
        FRAMERATE = 0x00000050,
        FRAMETIME = 0x00000051,
        FRAMERATE_MIN = 0x00000052,
        FRAMERATE_AVG = 0x00000053,
        FRAMERATE_MAX = 0x00000054,
        FRAMERATE_1DOT0_PERCENT_LOW = 0x00000055,
        FRAMERATE_0DOT1_PERCENT_LOW = 0x00000056,
        GPU_POWER = 0x00000060,
        GPU_TEMP_LIMIT = 0x00000070,
        GPU_POWER_LIMIT = 0x00000071,
        GPU_VOLTAGE_LIMIT = 0x00000072,
        GPU_UTIL_LIMIT = 0x00000074,
        GPU_SLI_SYNC_LIMIT = 0x00000075,
        CPU_TEMPERATURE = 0x00000080,
        CPU_USAGE = 0x00000090,
        RAM_USAGE = 0x00000091,
        PAGEFILE_USAGE = 0x00000092,
        CPU_CLOCK = 0x000000A0,
        GPU_TEMPERATURE2 = 0x000000B0,
        PCB_TEMPERATURE2 = 0x000000B1,
        MEM_TEMPERATURE2 = 0x000000B2,
        VRM_TEMPERATURE2 = 0x000000B3,
        GPU_TEMPERATURE3 = 0x000000C0,
        PCB_TEMPERATURE3 = 0x000000C1,
        MEM_TEMPERATURE3 = 0x000000C2,
        VRM_TEMPERATURE3 = 0x000000C3,
        GPU_TEMPERATURE4 = 0x000000D0,
        PCB_TEMPERATURE4 = 0x000000D1,
        MEM_TEMPERATURE4 = 0x000000D2,
        VRM_TEMPERATURE4 = 0x000000D3,
        GPU_TEMPERATURE5 = 0x000000E0,
        PCB_TEMPERATURE5 = 0x000000E1,
        MEM_TEMPERATURE5 = 0x000000E2,
        VRM_TEMPERATURE5 = 0x000000E3,
        PLUGIN_GPU = 0x000000F0,
        PLUGIN_CPU = 0x000000F1,
        PLUGIN_MOBO = 0x000000F2,
        PLUGIN_RAM = 0x000000F3,
        PLUGIN_HDD = 0x000000F4,
        PLUGIN_NET = 0x000000F5,
        PLUGIN_PSU = 0x000000F6,
        PLUGIN_UPS = 0x000000F7,
        PLUGIN_MISC = 0x000000FF,
        CPU_POWER = 0x00000100,
        UNKNOWN = 0xFFFFFFFF
    }
}