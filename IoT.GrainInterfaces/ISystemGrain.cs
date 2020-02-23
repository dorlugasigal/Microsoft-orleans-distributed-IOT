using Orleans;
using System.Threading.Tasks;

namespace IoT.GrainInterfaces
{
    [ExtendedPrimaryKey]
    public interface ISystemGrain : IGrain
    {
        Task SetTemperature(TemperatureReading reading);

        Task<double> GetTemperature();

        Task Subscribe(ISystemObserver observer);

        Task Unsubscribe(ISystemObserver observer);
    }
}
