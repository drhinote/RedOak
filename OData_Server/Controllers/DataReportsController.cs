using Microsoft.AspNet.OData;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Roi.Data;
using Roi.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace OData_Server
{
	[JwtAuth]
    [Authorize]
    public class DataReportsController : ODataController
    {

        public List<Guid> Companies => (User.Identity as ClaimsIdentity)?.Claims.Where(c => c.Type == ClaimTypes.GroupSid).Select(c => Guid.Parse(c.Value)).ToList();

        static DocumentClient Client = new DocumentClient(new Uri("https://roi.documents.azure.com:443/"), "mdKXWHu7FgVhDaFxTnUzgn8FjikFTssQh7Hx0Y8kIn4M03Pa9PRgmgQcNPMQMcRvgLyt55gXcL4GwLshFGs5bg==");

		[EnableQuery]
        public async Task<string> Get()
        {
			//******************************************************************************************************************
			// 1.3 - Read ALL documents in a Collection

			//SELECT c.id, c.ReportData.Uuid, c.ReportData.InjuryFlag FROM c where c.ReportData.InjuryFlag > 89
			//
			// NOTE: Use MaxItemCount on FeedOptions to control how many documents come back per trip to the server
			//       Important to handle throttles whenever you are doing operations such as this that might
			//       result in a 429 (throttled request)
			//******************************************************************************************************************

			var docs = new List<Document>();

			string continuationToken = null;
			do
			{
				var feed = await Client.ReadDocumentFeedAsync(
					UriFactory.CreateDocumentCollectionUri("Reports", "Models"),
					new FeedOptions { MaxItemCount = 10, RequestContinuation = continuationToken });
				continuationToken = feed.ResponseContinuation;
				foreach (Document document in feed)
				{
					docs.Add(document);
				}
			} while (continuationToken != null);

			return JsonConvert.SerializeObject(docs);

			//ReportBundle querySalesOrder = Client.CreateDocumentQuery<ReportBundle>(
			//	UriFactory.CreateDocumentCollectionUri("Reports", "Models"))
			//	.Where(rb => rb.ReportData.InjuryFlag > 89)
			//	.AsEnumerable()
			//	.First();

			//return JsonConvert.SerializeObject(querySalesOrder);
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