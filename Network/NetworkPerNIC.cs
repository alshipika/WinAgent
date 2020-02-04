using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class NetworkPerNIC
    {
        public static void Do()
        {

            var a = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            //System.Net.NetworkInformation.TcpConnectionInformation
            foreach (var c in a)
            {
                
                var b = c.GetIPv4Statistics();
                var b1 = c.GetIPStatistics();

                var b2 = c.GetIPProperties();


            }
        }
    }
}
