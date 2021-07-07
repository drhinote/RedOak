using Roi.Analysis.Api.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Roi.Analysis.Api.Filters;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;
using Roi.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Roi.Analysis.Api.Controllers
{
    [HandsetEndpoint]
    public class ViewTestController : ControllerBase
    {
        public string Post([FromBody]ViewTest data)
        {
            if (data == null) return "";
            var timewe = long.Parse(data.time);
            if (data.test == null)
            {
                if (!Ctx.tests.Any(t => t.time == timewe)) return "";
                data.test.test = Ctx.tests.Find(timewe);
                data.test.subject.subject = Ctx.subjects.Find(data.uuid);
            }
            else
            {
                Ctx.Configuration.ValidateOnSaveEnabled = false;
                data.test.company = data.test.test.company = data.test.subject.subject.company = Company;
                data.test.test.SportsTable = Handset.device_id;
                Ctx.subjects.AddOrUpdate(data.test.subject.subject);

                UploadToBlob(data.test);
                Ctx.tests.AddOrUpdate(data.test.test);

                Ctx.SaveChanges();
            }
            return "https://redoakreports.com/Search/GetReport.aspx?uuid=" + data.uuid + "&time=" + data.time;
        }

        public static void UploadToBlob(TestInfo data)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["testblob"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("tests/");


            var folder = container.GetDirectoryReference(data.uuid + "-" + data.time);
            Task.WaitAll(folder.GetBlockBlobReference("hardware").UploadFromByteArrayAsync(data.hardware, 0, data.hardware.Length), 
                         folder.GetBlockBlobReference("right2").UploadFromByteArrayAsync(data.r2, 0, data.r2.Length),
                         folder.GetBlockBlobReference("left2").UploadFromByteArrayAsync(data.l2, 0, data.l2.Length),
                         folder.GetBlockBlobReference("right3").UploadFromByteArrayAsync(data.r3, 0, data.r3.Length),
                         folder.GetBlockBlobReference("left3").UploadFromByteArrayAsync(data.l3, 0, data.l3.Length));
        }
    }
}

