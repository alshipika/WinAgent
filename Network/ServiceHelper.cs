
using System;
using System.Collections.Generic;
using System.Management;
using System.ServiceProcess;
using System.Linq;

namespace Network
{
    class ServiceHelper
    {
        enum ServiceStatus
        {
            OK,
            Error,
            Degraded,
            Unknown,
            PredFail,
            Starting,
            Stopping,
            Service,
            Stressed,
            NonRecover,
            NoContact,                     
            LostComm
        }


        public static void GetAllServices()
        {
            foreach (ServiceController service in ServiceController.GetServices())
            {
                string serviceName = service.ServiceName;
                string serviceDisplayName = service.DisplayName;
                string serviceType = service.ServiceType.ToString();
                string status = service.Status.ToString();
            }
        }

        public static ServiceInfo[] GetService32()
        {
            List<ServiceInfo> result = new List<ServiceInfo>();

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Service");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);


            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject m in queryCollection)
            {
                Console.WriteLine("ServiceName : {0}", m["Name"]);
                Console.WriteLine("ServicePathName : {0}", m["PathName"]);
                Console.WriteLine("ServiceProcessId : {0}", m["ProcessId"]);
                Console.WriteLine("State : {0}", m["State"]);
                Console.WriteLine("Status : {0}", m["Status"]);
                Console.WriteLine();

                try
                {
                    ServiceInfo serviceInfo = new ServiceInfo()
                    {
                        Name = m["Name"].ToString(),
                        PathName = m["PathName"].ToString(),
                        ProcessId = int.Parse(m["ProcessId"].ToString()),
                        Status = m["Status"].ToString(),
                        State = m["State"].ToString()
                    };

                    result.Add(serviceInfo);
                }
                catch
                {
                }
            }

            var groupedResult = result.Where(x => x.ProcessId != 0).GroupBy(x => x.ProcessId).Where(x => x.Count() > 1).OrderBy(x => x.Count()).SelectMany(x => x).ToList();

            foreach (var item in groupedResult)
                item.IsShared = true;

            return result.ToArray();
        }
    }
}
