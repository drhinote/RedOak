using Roi.Analysis.Api.Filters;
using Roi.Analysis.Api.Models;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Migrations;

namespace Roi.Analysis.Api.Controllers
{
    [HandsetEndpoint]
    public class UpdateTesterInfoController : ControllerBase
    {
        public int Post([FromBody]UpdateTesterInfo data)
        {
            if (data.oldName != data.newName && Ctx.Testers.Any(t => t.Name == data.newName && t.Name != "")) return 1;
            var tester = string.IsNullOrWhiteSpace(data.oldName) ? new Tester() : Ctx.authorized_ids.First(i => i.userid == data.oldName);
            tester.userid = data.newName;
            tester.paw = data.paw;
            tester.company = Company;
            tester.IsDeleted = false;
            Ctx.authorized_ids.AddOrUpdate(tester);
            Ctx.SaveChanges();
            return 2;
        }
    }
}
