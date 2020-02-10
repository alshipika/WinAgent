using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class ProcessCPUMemoryInfo
    {
        public int ProcessId { get; set; }

        public int WorkingSet { get; set; }

        public TimeSpan TotalProcessorTime { get; set; }

        public override string ToString()
        {
            return ProcessId + " " + WorkingSet + " " + TotalProcessorTime.ToString();
        }
    }

    class ProcessesHelper
    {
        public static List<ServiceCPUMemoryInfo> GetExpenses(ServiceInfo[] services)
        {
            List<ProcessCPUMemoryInfo> result = new List<ProcessCPUMemoryInfo>();

            System.Diagnostics.Process[] currentProcesses = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in currentProcesses)
            {
                try
                {
                    result.Add(
                        new ProcessCPUMemoryInfo()
                        {
                            ProcessId = process.Id,
                            WorkingSet = process.WorkingSet,
                            TotalProcessorTime = process.TotalProcessorTime
                        }
                    );
                    Console.WriteLine(process.ProcessName + " " + process.WorkingSet + " " + process.TotalProcessorTime);
                }
                catch
                { }
            }

            return Calculate(result, services);
        }

        private static List<ServiceCPUMemoryInfo> Calculate(List<ProcessCPUMemoryInfo> processCPUMemoryInfos, ServiceInfo[] services)
        {
            List<ServiceCPUMemoryInfo> result = new List<ServiceCPUMemoryInfo>();

            //var a = tcpTableRecords.Where(x => services.ToList().Exists(y => y.ProcessId == x.ProcessId && y.ProcessId != 0)).ToList();

            var groupedResult = processCPUMemoryInfos.Where(x => x.ProcessId != 0);

            foreach (var processItem in groupedResult)
            {
                List<ServiceInfo> servicesOfProcess = services.Where(x => processItem.ProcessId == x.ProcessId).ToList();

                foreach (var serviceInfo in servicesOfProcess)
                {
                    result.Add(new ServiceCPUMemoryInfo()
                    {
                        ServiceInfo = serviceInfo,
                        TotalProcessorTime = processItem.TotalProcessorTime,
                        WorkingSet = processItem.WorkingSet
                    });
                }
            }

            return result;
        }

        public static void Main1()
        {
            // Define variables to track the peak
            // memory usage of the process.
            long peakPagedMem = 0,
                 peakWorkingSet = 0,
                 peakVirtualMem = 0;

            // Start the process.
            using (Process myProcess = Process.Start("NotePad.exe"))
            {
                // Display the process statistics until
                // the user closes the program.
                do
                {
                    if (!myProcess.HasExited)
                    {
                        // Refresh the current process property values.
                        myProcess.Refresh();

                        Console.WriteLine();

                        // Display current process statistics.

                        Console.WriteLine($"{myProcess} -");
                        Console.WriteLine("-------------------------------------");

                        Console.WriteLine($"  Physical memory usage     : {myProcess.WorkingSet64}");
                        Console.WriteLine($"  Base priority             : {myProcess.BasePriority}");
                        Console.WriteLine($"  Priority class            : {myProcess.PriorityClass}");
                        Console.WriteLine($"  User processor time       : {myProcess.UserProcessorTime}");
                        Console.WriteLine($"  Privileged processor time : {myProcess.PrivilegedProcessorTime}");
                        Console.WriteLine($"  Total processor time      : {myProcess.TotalProcessorTime}");
                        Console.WriteLine($"  Paged system memory size  : {myProcess.PagedSystemMemorySize64}");
                        Console.WriteLine($"  Paged memory size         : {myProcess.PagedMemorySize64}");

                        // Update the values for the overall peak memory statistics.
                        peakPagedMem = myProcess.PeakPagedMemorySize64;
                        peakVirtualMem = myProcess.PeakVirtualMemorySize64;
                        peakWorkingSet = myProcess.PeakWorkingSet64;

                        if (myProcess.Responding)
                        {
                            Console.WriteLine("Status = Running");
                        }
                        else
                        {
                            Console.WriteLine("Status = Not Responding");
                        }
                    }
                }
                while (!myProcess.WaitForExit(1000));


                Console.WriteLine();
                Console.WriteLine($"  Process exit code          : {myProcess.ExitCode}");

                // Display peak memory statistics for the process.
                Console.WriteLine($"  Peak physical memory usage : {peakWorkingSet}");
                Console.WriteLine($"  Peak paged memory usage    : {peakPagedMem}");
                Console.WriteLine($"  Peak virtual memory usage  : {peakVirtualMem}");
            }
        }
    }
}
