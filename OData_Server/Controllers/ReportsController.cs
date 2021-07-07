using Microsoft.AspNet.OData;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Roi.Data;
using Roi.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OData_Server
{
    [JwtAuth]
    [System.Web.Http.Authorize]
    public class ReportsController : ODataController
    {

        public List<Guid> Companies => (User.Identity as ClaimsIdentity)?.Claims.Where(c => c.Type == ClaimTypes.GroupSid).Select(c => Guid.Parse(c.Value)).ToList();

        static DocumentClient Client = new DocumentClient(new Uri("https://roi.documents.azure.com:443/"), "mdKXWHu7FgVhDaFxTnUzgn8FjikFTssQh7Hx0Y8kIn4M03Pa9PRgmgQcNPMQMcRvgLyt55gXcL4GwLshFGs5bg==");

        [EnableQuery]
        public IQueryable<ReportBundle> Get()
        {
            return Client.CreateDocumentQuery<ReportBundle>(UriFactory.CreateDocumentCollectionUri("Reports", "Models")).Where(c => Companies.Contains(Guid.Parse(c.CompanyId)));
        }

        [EnableQuery]
        public async Task<SingleResult<ReportBundle>> Get([FromODataUri] string key)
        {
            using (var db = new RoiDb())
            {
                var companyId = db.Tests.Find(Guid.Parse(key))?.CompanyId.ToString();
                ReportBundle model = await AnalysisLogic.RetrieveReport(key, companyId);
                return SingleResult.Create(new List<ReportBundle> { model }.AsQueryable());
            }
        }

    }
}