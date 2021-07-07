using Roi.Analysis.Api.Filters;
using Roi.Analysis.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
	[WebEndpoint]
    public class RegisteredController : ControllerBase
    {
        // POST: Search
        public bool Post(string deviceId)
        {
            return Ctx.machines.Any(m => m.deviceId == deviceId);
		}
    }
}