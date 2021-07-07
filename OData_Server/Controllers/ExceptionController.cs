using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OData_Server.Controllers
{
    public class ExceptionController : ApiController
    {
        public IHttpActionResult Post([FromBody] string error)
        {
            foreach (var line in error.Split('\r'))
            {
                System.Diagnostics.Trace.TraceError(line);
            }
            return Ok();
        }
    }
}
