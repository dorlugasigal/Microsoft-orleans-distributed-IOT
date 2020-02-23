using System.Threading.Tasks;

namespace IoT.GrainInterfaces
{
    /// <summary>
    /// Orleans grain communication interface IGrain1
    /// </summary>
    public interface IDeviceGrain : Orleans.IGrain
    {
        Task SetTemperature(double value);

        Task<double> GetTemperature();

        Task JoinSystem(string name);
    }
}
