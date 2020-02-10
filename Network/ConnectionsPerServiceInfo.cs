using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class ConnectionsPerServiceInfo
    {
        public ServiceInfo ServiceInfo { get; set; }
        
        public int  Count { get; set; }

        public override string ToString()
        {
            return ServiceInfo.ProcessId + " " + ServiceInfo.Name + " " + Count;
        }
    }
}
