using Microsoft.AspNet.OData;
using Newtonsoft.Json;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OData_Server
{
    [JwtAuth]
    [System.Web.Http.Authorize]
    public class TestsController : ODataBase<Test>
    {
        public override DbSet<Test> Set => Db.Tests;

        //public override Task<IHttpActionResult> Post(Test item)
        //{
        //    var device = Db.Devices.FirstOrDefault(d => d.Serial == User.Identity.Name);
        //    var found = false;
        //    foreach(var record in device.DeviceStatuses.Where(s => s.Status == Roi.Data.StatusCode.Calibration))
        //    {
        //        var values = JsonConvert.DeserializeObject<CalibrationRecord>(record.Data);
        //        found = item.History.Contains(values.IndexMax) && item.History.Contains(values.IndexMin)
        //        && item.History.Contains(values.ThumbMax) && item.History.Contains(values.ThumbMin)
        //        && item.History.Contains(values.PinkyMax) && item.History.Contains(values.PinkyMin);
        //    }
        //    if(!found)
        //    {
        //        var vals = item.History.Split(',');
        //        device.DeviceStatuses.Add(new DeviceStatus
        //        {
        //            Status = Roi.Data.StatusCode.Calibration,
        //            Date = DateTime.Now,
        //            Data = JsonConvert.SerializeObject(new CalibrationRecord
        //            {
        //                ThumbMin = vals[2],
        //                ThumbMax = vals[4],
        //                IndexMin = vals[7],
        //                IndexMax = vals[9],
        //                PinkyMin = vals[12],
        //                PinkyMax = vals[14],
        //            }),
        //            DeviceId = device.Id
        //        });
        //        Db.SaveChanges();
        //    }
        //    return base.Post(item);
        //}
    }
}