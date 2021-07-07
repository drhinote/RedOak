using Newtonsoft.Json;
using Roi.Analysis.Api.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
    [HandsetEndpoint]
    public class SendErrorReportController : ControllerBase
    {
        public string Post([FromBody]string error)
        {
         //   File.AppendAllText("c:\\Roi\\handset-error.txt", error);
            return "Ticket submitted";
        }
    }
}

