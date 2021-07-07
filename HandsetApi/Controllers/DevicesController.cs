using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
    public class DevicesController : ControllerBase
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string id) => Ctx.machines.Where(m => m.name == id).Select(c => c.company_name).FirstOrDefault();
		//public string Get(string id)
		//{

		//}

		[Route("api/devices/{deviceName}/isregistered")]
		[HttpGet]
		public bool IsRegistered(string deviceName)
		{
			using (var ctx = new Roi.Data.RoiDb())
			{
				return ctx.machines.Any(m => m.name == deviceName);
			}

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
}