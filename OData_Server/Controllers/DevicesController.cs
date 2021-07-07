using Microsoft.AspNet.OData;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OData_Server
{
    [JwtAuth]
    [System.Web.Http.Authorize]
    public class DevicesController : ODataBase<Device>
    {
        public override DbSet<Device> Set => Db.Devices;
    }
}