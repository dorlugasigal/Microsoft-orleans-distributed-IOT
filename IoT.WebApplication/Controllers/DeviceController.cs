using IoT.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IoT.WebApplication.Controllers
{
    public class DeviceController : ApiController
    {
        public Task<double> Get(long id)
        {
            var grain = DeviceGrainFactory.GetGrain(id);
            return grain.GetTemperature();
        }

        public Task Post([FromBody] string message)
        {
            var grain = DecodeGrainFactory.GetGrain(0);
            return grain.Decode(message);
        }

    }
}
