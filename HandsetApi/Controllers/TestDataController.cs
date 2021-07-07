using Roi.Analysis.Api.Models;
using System.Data.Entity.Migrations;
using System.Web.Http;
using System.Linq;
using Roi.Analysis.Api.Filters;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http.Results;
using System;
using Newtonsoft.Json.Linq;
using Roi.Data;

namespace Roi.Analysis.Api.Controllers
{
    [HandsetEndpoint]
    public class TestDataController : ControllerBase
    {
        public async Task<long> Post([FromBody]TestData data)
        {
            if (data?.test?.subject?.subject == null) return 0;
            data.test.subject.subject.company = Company;
            data.test.test.company = Company;
            Ctx.subjects.AddOrUpdate(data.test.subject.subject);
            await Task.Run(() =>
            {
                ViewTestController.UploadToBlob(data.test);
                Ctx.tests.AddOrUpdate(data.test.test);
            });
            await Ctx.SaveChangesAsync();
            return data.test.time;
        }
    }
}
