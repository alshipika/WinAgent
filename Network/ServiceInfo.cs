using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class ServiceInfo
    {
        // Name is unique 
        public string Name { get; set; }

        public string PathName { get; set; }

        public int ProcessId { get; set; }

        public string Status { get; set; }

        public string State { get; set; }

        public bool IsShared { get; set; }

        public override string ToString()
        {
            return ProcessId + " " + Name;
        }
    }
}
