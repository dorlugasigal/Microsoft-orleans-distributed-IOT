using IoT.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.TestSilo
{
    public class SystemObserver : ISystemObserver
    {
        public void HighTemperature(double value)
        {
            Console.WriteLine("Observed a high system temperature {0}", value);
        }
    }
}
