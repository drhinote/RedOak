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
    public class TestsController : ControllerBase
    {
        // POST api/<controller>
        public IEnumerable<test> Post([FromBody]IEnumerable<test> tests)
        {
            foreach (var s in tests)
            {
                s.company = Company;
                Ctx.tests.AddOrUpdate(s);
                Ctx.SaveChanges();
            }
            return Ctx.tests.Where(s => s.company == Company);
        }
    }
}