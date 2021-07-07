using Microsoft.AspNet.OData;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OData_Server
{
    [JwtAuth]
    [System.Web.Http.Authorize]
    public class SubjectsController : ODataBase<Subject>
    {
        public override DbSet<Subject> Set => Db.Subjects;
    }
}