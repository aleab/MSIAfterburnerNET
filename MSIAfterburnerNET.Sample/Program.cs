using MSIAfterburnerNET.HM;
using MSIAfterburnerNET.HM.Interop;
using System;
using System.Threading;

namespace MSIAfterburnerNET.Sample
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                using (var mahm = new HardwareMonitor())
                {
                    // print out current MACM Header values
                    Console.WriteLine("***** MSI AFTERTERBURNER HARDWARE MONITOR HEADER *****");
                    Console.WriteLine(mahm.Header.ToString().Replace(";", "\n"));
                    Console.WriteLine();

                    // print out current Entry values
                    for (int i = 0; i < mahm.Header.EntryCount; ++i)
                    {
                        Console.WriteLine($"***** MSI AFTERTERBURNER DATA SOURCE {i} *****");
                        Console.WriteLine(mahm.Entries[i].ToString().Replace(";", "\n"));
                        Console.WriteLine();
                    }

                    // print out current MAHM GPU Entry values
                    for (int i = 0; i < mahm.Header.GpuEntryCount; ++i)
                    {
                        Console.WriteLine($"***** MSI AFTERTERBURNER GPU {i} *****");
                        Console.WriteLine(mahm.GpuEntries[i].ToString().Replace(";", "\n"));
                        Console.WriteLine();
                    }

                    // show a data source monitor several times
                    HardwareMonitorEntry framerate = mahm.GetEntry(HardwareMonitor.GPU_GLOBAL_INDEX, MONITORING_SOURCE_ID.FRAMERATE);
                    if (framerate != null)
                    {
                        Console.WriteLine("***** FRAMERATE *****");
                        for (int i = 0; i < 10; ++i)
                        {
                            Console.WriteLine(framerate.Data);
                            Thread.Sleep(1000);
                            mahm.RefreshEntry(framerate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}