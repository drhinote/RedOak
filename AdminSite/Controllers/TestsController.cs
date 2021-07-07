using Roi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace RedoakAdmin.Controllers
{
    public class TestsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/tests/bytester/{UserId}/{UtcOffset}")]
        [HttpGet]
        public string GetByTester(Guid UserId, string UtcOffset)
        {
            try
            {
                using (var ctx = new Roi.Data.RoiDb())
                {
                    var res = ctx.Tests.Where(t => t.TesterId == UserId).OrderByDescending(t => t.DateTime).Select(t => new TestContainer()
                    {
                        CompanyName = t.Company.Name,
                        UuId = t.UuId,
                        OpId = t.OpId,
                        TesterId = t.TesterId,
						UnixTimeStamp = t.UnixTimeStamp,
                    }).ToList();

                    foreach (var test in res)
                    {
                        test.TimeString = test.UnixTimeStamp.ToDateTimeString(Convert.ToInt32(UtcOffset));
                    }

                    return new JavaScriptSerializer().Serialize(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/tests/{CompanyId}/{UtcOffset}")]
        [HttpGet]
        public string Get(Guid CompanyId, string UtcOffset)
        {
            try
            {
                using (var ctx = new RoiDb())
                {
					//*** hack for old schema
					// get company from name
					var company = ctx.Companies.Where(c => c.Id == CompanyId).FirstOrDefault();

                    var res = ctx.Tests.Where(t => t.CompanyId == company.Id).OrderByDescending(t => t.DateTime).Select(t => new TestContainer()
                    {
						Id = t.Id,
                        UuId = t.UuId,
                        OpId = t.OpId,
                        TesterName = t.Tester.Name,
						UnixTimeStamp = t.UnixTimeStamp,
                        DateTime = t.DateTime,
                    }).ToList();

					foreach (var test in res)
					{
						//test.Time = test.TimeLong.ToDateTimeString(Convert.ToInt32(UtcOffset));
						//test.TimeString = test.DateTime.ToString("dddd, MM/dd/yyyy hh:mm tt");
						test.TimeString = test.UnixTimeStamp.ToDateTimeString(Convert.ToInt32(UtcOffset));
					}

					return new JavaScriptSerializer().Serialize(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

        public class TestContainer : Test
        {
			public string CompanyName { get; set; }
            public string TimeString { get; set; }
			public string TesterName { get; set; }
        }
    }
}