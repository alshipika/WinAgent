using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Network.ConnectionsHelper;

namespace Network
{
    class Program
    {
        static void Main(string[] args)
        {

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
    }
}
