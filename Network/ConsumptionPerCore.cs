using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Core
    {
        public string Name { get; set; }
        public double Consumption { get; set; }
        public override string ToString()
        {
            return String.Format("{0} - {1:f}", Name, Consumption);
        }
    }

    class Processor
    {
        public string Name { get; set; }
        public List<Core> Cores { get; set; } = new List<Core>();
        public double TotalConsumption { get; set; }
        public override string ToString()
        {
            return String.Format("{0} - {1:f}", Name, TotalConsumption);
        }
    }

    class ConsumptionPerCore
    {
        public static Double Calculate(CounterSample oldSample, CounterSample newSample)
        {
            double difference = newSample.RawValue - oldSample.RawValue;
            double timeInterval = newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec;
            if (timeInterval != 0) return 100 * (1 - (difference / timeInterval));
            return 0;
        }
        PerformanceCounter pc;
        PerformanceCounterCategory cat;
        string[] instances;
        Dictionary<string, CounterSample> cs;

        public ConsumptionPerCore()
        {
            pc = new PerformanceCounter("Processor Information", "% Processor Time");
            cat = new PerformanceCounterCategory("Processor Information");
            instances = cat.GetInstanceNames();
            cs = new Dictionary<string, CounterSample>();

            foreach (var s in instances)
            {
                pc.InstanceName = s;
                cs.Add(s, pc.NextSample());
            }
        }

        public Processor[] Step()
        {
            List<Processor> result = new List<Processor>();

            foreach (var s in instances)
            {
                pc.InstanceName = s;

                string[] values = s.Split(new[] { ',' }, StringSplitOptions.None);

                if (values.Length == 2)
                {
                    Processor processor = result.SingleOrDefault(x => x.Name == values[0]);
                    if (processor == null)
                    {
                        processor = new Processor() { Name = values[0] };
                        result.Add(processor);
                    }

                    double consumption = Calculate(cs[s], pc.NextSample());
                    if (values[1] == "_Total")
                    {
                        processor.TotalConsumption = consumption;
                    }
                    else
                    {
                        Core core = new Core()
                        {
                            Consumption = consumption,
                            Name = values[1]
                        };
                        processor.Cores.Add(core);
                    }
                }

                Console.WriteLine("{0} - {1:f}", s, Calculate(cs[s], pc.NextSample()));
                cs[s] = pc.NextSample();
            }

            return result.ToArray();
        }
    }
}
