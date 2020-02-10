using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class ServiceCPUMemoryInfo
    {
        public ServiceInfo ServiceInfo { get; set; }

        public int WorkingSet { get; set; }

        public TimeSpan TotalProcessorTime { get; set; }

        public override string ToString()
        {
            return ServiceInfo.ProcessId + " " + ServiceInfo.Name + " " + WorkingSet + " " + TotalProcessorTime.ToString();
        }
    }
}
