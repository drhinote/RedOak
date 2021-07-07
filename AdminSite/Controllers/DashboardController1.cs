using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedoakAdmin.Controllers
{
    public class DashboardController1 : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            using (var ctx = new Roi.Data.RoiDb())
            {
                var tests = ctx.Tests.Select(t => new AllTests()
                {
                    uuid = t.UuId,
                    time = t.DateTime.DateTime,
                    opid = t.OpId,
                    tester = t.TesterId.ToString(),
                    company = t.Company.Name,
                });

                return JsonConvert.SerializeObject(tests);
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class AllTests
    {
        public string uuid { get; set; }
        public DateTime time { get; set; }
        public string opid { get; set; }
        public string tester { get; set; }
        public string company { get; set; }
    }
}