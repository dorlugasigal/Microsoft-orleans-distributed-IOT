using IoT.GrainInterfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.GrainClasses
{
    public class SystemGrain : Grain, ISystemGrain
    {
        Dictionary<long, double> temperatures;
        ObserverSubscriptionManager<ISystemObserver> observers;

        public override Task ActivateAsync()
        {
            this.temperatures = new Dictionary<long, double>();
            this.observers = new ObserverSubscriptionManager<ISystemObserver>();

            var timer = this.RegisterTimer(this.Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

            return base.ActivateAsync();
        }

        Task Callback(object callbackState)
        {
            var value = this.temperatures.Values.Average();
            if (value > 100)
            {
                this.observers.Notify(x => x.HighTemperature(value));
            }
            return TaskDone.Done;
        }

        public Task SetTemperature(TemperatureReading reading)
        {
            if (this.temperatures.Keys.Contains(reading.DeviceId))
            {
                this.temperatures[reading.DeviceId] = reading.Value;
            }
            else
            {
                this.temperatures.Add(reading.DeviceId, reading.Value);
            }

            return TaskDone.Done;
        }



        public Task Subscribe(ISystemObserver observer)
        {
            this.observers.Subscribe(observer);
            return TaskDone.Done;
        }

        public Task Unsubscribe(ISystemObserver observer)
        {
            this.observers.Unsubscribe(observer);
            return TaskDone.Done;
        }


        public Task<double> GetTemperature()
        {
            return Task.FromResult(this.temperatures.Values.Average());
        }
    }
}
