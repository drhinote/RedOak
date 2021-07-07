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
    [HandsetEndpoint]
    public class SubjectsController : ControllerBase
    {
    
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]IEnumerable<Subject> subjects)
        {
            if (!RequestContext.Principal.Identity.IsAuthenticated) return BadRequest("Unauthorized");
            foreach (var s in subjects)
            {
                s.company = Company;
                Ctx.subjects.AddOrUpdate(s);
                Ctx.SaveChanges();
            }
            return Ok(Ctx.subjects.Where(s => s.company == Company));
        }

    }
}