using System;
using System.Runtime.InteropServices;

namespace MSIAfterburnerNET.CM.Interop
{
    [Serializable]
    public struct MACM_SHARED_MEMORY_GPU_ENTRY
    {
        public MACM_SHARED_MEMORY_GPU_ENTRY_FLAG flags;

        public uint coreClockCur;
        public uint coreClockMin;
        public uint coreClockMax;
        public uint coreClockDef;

        public uint shaderClockCur;
        public uint shaderClockMin;
        public uint shaderClockMax;
        public uint shaderClockDef;

        public uint memoryClockCur;
        public uint memoryClockMin;
        public uint memoryClockMax;
        public uint memoryClockDef;

        public uint fanSpeedCur;
        public MACM_SHARED_MEMORY_GPU_ENTRY_FAN_FLAG fanFlagsCur;
        public uint fanSpeedMin;
        public uint fanSpeedMax;
        public uint fanSpeedDef;
        public MACM_SHARED_MEMORY_GPU_ENTRY_FAN_FLAG fanFlagsDef;

        public uint coreVoltageCur;
        public uint coreVoltageMin;
        public uint coreVoltageMax;
        public uint coreVoltageDef;

        public uint memoryVoltageCur;
        public uint memoryVoltageMin;
        public uint memoryVoltageMax;
        public uint memoryVoltageDef;

        public uint auxVoltageCur;
        public uint auxVoltageMin;
        public uint auxVoltageMax;
        public uint auxVoltageDef;

        public int coreVoltageBoostCur;
        public int coreVoltageBoostMin;
        public int coreVoltageBoostMax;
        public int coreVoltageBoostDef;

        public int memoryVoltageBoostCur;
        public int memoryVoltageBoostMin;
        public int memoryVoltageBoostMax;
        public int memoryVoltageBoostDef;

        public int auxVoltageBoostCur;
        public int auxVoltageBoostMin;
        public int auxVoltageBoostMax;
        public int auxVoltageBoostDef;

        public int powerLimitCur;
        public int powerLimitMin;
        public int powerLimitMax;
        public int powerLimitDef;

        public int coreClockBoostCur;
        public int coreClockBoostMin;
        public int coreClockBoostMax;
        public int coreClockBoostDef;

        public int memoryClockBoostCur;
        public int memoryClockBoostMin;
        public int memoryClockBoostMax;
        public int memoryClockBoostDef;

        public int thermalLimitCur;
        public int thermalLimitMin;
        public int thermalLimitMax;
        public int thermalLimitDef;

        public uint thermalPrioritizeCur;
        public uint thermalPrioritizeDef;

        public uint aux2VoltageCur;
        public uint aux2VoltageMin;
        public uint aux2VoltageMax;
        public uint aux2VoltageDef;

        public int aux2VoltageBoostCur;
        public int aux2VoltageBoostMin;
        public int aux2VoltageBoostMax;
        public int aux2VoltageBoostDef;

        public MACM_SHARED_MEMORY_VF_CURVE vfCurve;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
        public char[] gpuId;
    }
}