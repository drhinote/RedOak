using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Roi.Analysis.Api.Models;
using Roi.Data;
using System;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
    public class ControllerBase : ApiController
    {
        protected RoiDb Ctx { get; set; } = new RoiDb();
        protected machine Handset => ((HandsetPrincipal) ControllerContext.RequestContext.Principal).Handset;
        protected string Company => ((HandsetPrincipal)ControllerContext.RequestContext.Principal).Handset?.company;
		protected string WebCompany => ((WebPrincipal)ControllerContext.RequestContext.Principal).Id?.company;
        protected CloudStorageAccount Blob => CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("reportconverter_AzureStorageConnectionString"));
    }
}