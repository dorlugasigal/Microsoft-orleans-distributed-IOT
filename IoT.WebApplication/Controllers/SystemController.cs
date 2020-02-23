using IoT.GrainInterfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace IoT.WebApplication.Controllers
{
    public class SystemController : ApiController
    {
        public Task<double> Get(string id)
        {
            var grain = SystemGrainFactory.GetGrain(0, id);
            return grain.GetTemperature();
        }
    }
}
