using Orleans;
using System.Threading.Tasks;
using Orleans.Concurrency;

namespace IoT.GrainInterfaces
{
    [StatelessWorker]
    public interface IDecodeGrain : IGrain
    {
        Task Decode(string message);
    }
}
