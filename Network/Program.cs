using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Network.ConnectionsHelper;

namespace Network
{
    class Program
    {


        static void Main(string[] args)        
        {



                /*
                var b = System.Diagnostics.PerformanceCounterCategory.GetCategories();

                foreach(var c in b)
                {
                    try
                    {
                        EnumerateCountersFor(c.CategoryName);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.ReadLine();
                    }
                    catch
                    { }
                }*/
                Console.ReadLine();
            var services = ServiceHelper.GetService32();
            //ConnectionsHelper.GetData();

            var serviceCPU = ProcessesHelper.GetExpenses(services);
            //System.Diagnostics.Process.PerformanceCounter
            //var a1 = ConnectionsHelper.GetData();
            /* ConsumptionPerCore consumptionPerCore = new ConsumptionPerCore();
             while (true)
             {
                 var data = consumptionPerCore.Step();
                 System.Threading.Thread.Sleep(15000);
                Console.WriteLine();
             }*/


           // NetworkPerNIC.Do();
            var a = ConnectionsPerProcess.GetAllTcpConnections(services);

           // var d = System.Net.NetworkInformation.
           //  var b2 = a[0].();

        }

        private static void EnumerateCountersFor(string category)
        {
            var sb = new StringBuilder();
            var counterCategory = new PerformanceCounterCategory(category);

            if (counterCategory.CategoryType == PerformanceCounterCategoryType.SingleInstance)
            {
                foreach (var counter in counterCategory.GetCounters())
                {
                    sb.AppendLine(string.Format("{0}:{1}", category, counter.CounterName));
                }
            }
            else
            {
                foreach (var counterInstance in counterCategory.GetInstanceNames())
                {
                    foreach (var counter in counterCategory.GetCounters(counterInstance))
                    {
                        sb.AppendLine(string.Format("{0}:{1}:{2}", counterInstance, category, counter.CounterName));
                    }
                }
            }

            Console.WriteLine(sb.ToString());
        }
    }

}
