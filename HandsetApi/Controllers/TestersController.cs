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
    public class TestersController : ControllerBase
    {

        // POST api/<controller>
        public IEnumerable<Tester> Post([FromBody]IEnumerable<Tester> testers)
        {
            foreach (var s in testers)
            {
                s.company = Company;
                Ctx.authorized_ids.AddOrUpdate(s);
                Ctx.SaveChanges();
            }
            return Ctx.authorized_ids.Where(s => s.company == Company);
        }
    }
}