using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.GrainInterfaces
{
    public interface ISystemObserver : IGrainObserver
    {
        void HighTemperature(double value);
    }
}
