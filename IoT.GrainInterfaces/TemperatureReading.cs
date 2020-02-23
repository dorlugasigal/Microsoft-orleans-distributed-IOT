using Orleans;
using System;
using Orleans.Concurrency;

namespace IoT.GrainInterfaces
{
    [Immutable]
    public class TemperatureReading
    {
        public double Value { get; set; }
        public long DeviceId { get; set; }
        public DateTime Time { get; set; }
    }
}
