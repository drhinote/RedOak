using Roi.Analysis.Api.Filters;
using Roi.Analysis.Api.Models;
using System.Web.Http;
using System.Data.Entity.Migrations;
using System.Linq;
using System;

namespace Roi.Analysis.Api.Controllers
{
    [HandsetEndpoint]
    public class SubjectInfoRefreshController : ControllerBase
    {
        public SubjectInfoRefresh Post([FromBody]SubjectInfoRefresh data)
        {
            var num = long.Parse(data.serverNum??"0");
            var res = new SubjectInfoRefresh();
            res.updatedInfo = Ctx.subjects.Where(s => s.num > num && s.company == Company).Select(s => new SubjectInfo { subject = s }).ToList();
            res.serverNum = Ctx.subjects.Where(s => s.company == Company).Max(s => s.num).ToString();
            if (data.updatedInfo.Any())
            {
                Ctx.subjects.AddOrUpdate(data.updatedInfo.Select(u => 
                {
                    u.subject.num = DateTime.Now.ToFileTimeUtc();
                    u.subject.company = Company; // subjects table in DerbyDb doesn't have this field, so it's null
                    return u.subject;
                }).ToArray());
            }
            Ctx.SaveChanges();
            return res;
        }
    }
}
