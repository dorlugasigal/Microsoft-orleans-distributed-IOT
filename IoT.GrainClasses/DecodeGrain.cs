using IoT.GrainInterfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Concurrency;

namespace IoT.GrainClasses
{
    [Reentrant]
    public class DecodeGrain : Grain, IDecodeGrain
    {
        public Task Decode(string message)
        {
            var parts = message.Split(',');
            var grain = DeviceGrainFactory.GetGrain(long.Parse(parts[0]));
            return grain.SetTemperature(double.Parse(parts[1]));
        }
    }
}
