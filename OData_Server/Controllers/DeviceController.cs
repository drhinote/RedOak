using Roi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OData_Server.Controllers
{
    public class NewDevice
    {
        public string id { get; set; }
    }

    public class DeviceController : ApiController
    {

        public IHttpActionResult Post([FromBody] NewDevice device)
        {
            using(var ctx = new RoiDb())
            {
                if (ctx.Devices.Count(d => d.Serial == device.id) < 1)
                {
                    ctx.Devices.Add(new Device { Serial = device.id, CompanyId = Guid.Parse("d4a3c1fb-8342-4201-811c-3db87a60e3fc"), Enabled = true });
                    ctx.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
