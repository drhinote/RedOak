using Roi.Analysis.Api.Filters;
using Roi.Analysis.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
    [HandsetEndpoint]
    public class TesterInfoRefreshController : ControllerBase
    {
        public TesterInfoRefresh Post([FromBody]TesterInfoRefresh data)
        {
            return new TesterInfoRefresh
            {
               updatedInfo = Ctx.authorized_ids.Where(a => a.company == Company).Select(a => new TesterInfo { name = a.userid, paw = a.paw }).ToList()
            };
        }
    }
}
