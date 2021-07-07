using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
    public class GetRegistrationInfoController : ControllerBase
    {
        // GET api/<controller>/5
        public string Get(string id) => Ctx.machines.Where(m => m.name == id).Select(m => m.company_name).FirstOrDefault();
    }
}