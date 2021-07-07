using Roi.Analysis.Api.Filters;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
    public class DeviceController : ControllerBase
    {

        public string Post([FromBody]dynamic device)
        {
            string id = device.id;
            if (!Ctx.machines.Any(m => m.name == id))
            {
                Ctx.machines.Add(new machine { company = "RegCal", company_name = "RegCal", company_id = Guid.Parse("d4a3c1fb-8342-4201-811c-3db87a60e3fc"), device_id = Guid.NewGuid().ToString(), name = device.id, status = 0, time = 0 });
                Ctx.SaveChanges();
            }
            return "OK";
        }

    }
}