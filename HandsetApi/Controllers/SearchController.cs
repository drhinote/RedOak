using Roi.Analysis.Api.Filters;
using Roi.Analysis.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
	[WebEndpoint]
    public class SearchController : ControllerBase
    {
        // POST: Search
        public IEnumerable<SearchResults> Post([FromBody]TestSearch search)
        {
			var startTime = (search.StartDate - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
			var endTime = (search.EndDate - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

			return Ctx.tests.Join(Ctx.subjects, t => t.Uuid, s => s.uuid, (t, s) => new { t, s })
				.Where(t => t.t.company == WebCompany)
				.Where(t => (search.Dob != "") ? t.t.dob == search.Dob : true)
				.Where(s => (search.Ssn != "") ? s.s.social == search.Ssn : true)
				.Where(t => (search.Opid != "") ? t.t.opid == search.Opid : true)
				.Where(t => (search.Uuid != "") ? t.t.Uuid == search.Uuid : true)
				.Where(t => (search.StartDate != DateTime.MinValue) ? t.t.time >= startTime : true)
				.Where(t => (search.EndDate != DateTime.MinValue) ? t.t.time <= endTime : true)
				.Select(test => new SearchResults()
			{
				Company = test.t.company,
				Time = test.t.time,
				Dob = test.t.dob,
				Opid = test.t.opid,
				Tester = test.t.tester,
				Uuid = test.t.Uuid,
			});
		}
    }
}