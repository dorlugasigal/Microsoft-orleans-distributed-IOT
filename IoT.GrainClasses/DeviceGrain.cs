using IoT.GrainInterfaces;
using Orleans;
using System;
using System.Threading.Tasks;
using Orleans.Providers;
using Orleans.Concurrency;

namespace IoT.GrainClasses
{
    public interface IDeviceGrainState : IGrainState
    {
        double LastValue { get; set; }
        string _System { get; set; }
    }

    [StorageProvider(ProviderName="AzureStore")]
    [Reentrant]
    public class DeviceGrain : Orleans.Grain<IDeviceGrainState>, IDeviceGrain
    {
        public override Task ActivateAsync()
        {
            var id = this.GetPrimaryKeyLong();
            Console.WriteLine("Activated {0}", id);
            Console.WriteLine("Activated state = {0}", this.State.LastValue);
            return base.ActivateAsync();
        }

        public async Task SetTemperature(double value)
        {
            if (this.State.LastValue < 100 && value >= 100)
            {
                Console.WriteLine("High temperature recorded {0}", value);
            }
            if (this.State.LastValue != value)
            {
                this.State.LastValue = value;
                await this.State.WriteStateAsync();
            }
            var systemGrain = SystemGrainFactory.GetGrain(0, this.State._System);
            var reading = new TemperatureReading
            {
                DeviceId = this.GetPrimaryKeyLong(),
                Time = DateTime.UtcNow,
                Value = value
            };
            await systemGrain.SetTemperature(reading);
        }


        public Task JoinSystem(string name)
        {
            this.State._System = name;
            return this.State.WriteStateAsync();
        }


        public Task<double> GetTemperature()
        {
            return Task.FromResult(this.State.LastValue);
        }
    }
}
