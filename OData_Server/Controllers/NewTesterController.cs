using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Roi.Data;

namespace OData_Server.Controllers
{
	[JwtAuth]
	[Authorize]
	public class NewTesterController : ODataController
    {
        private RoiDb db = new RoiDb();

		// POST: odata/NewTester
		public async Task<IHttpActionResult> Post([FromBody]TesterCompany TC)
        {
            try
            {
				var existingTester = db.Testers.Find(TC.Tester.Id);

				if (existingTester == null)
				{
					//var newTester = new Tester();
					//newTester.Id = TC.TesterId;
					//newTester.Name = TC.TesterName;
					//newTester.Password = TC.TesterPassword;
					//newTester.Active = true;

					//db.Testers.Add(newTester);
					db.Testers.Add(TC.Tester);
					db.SaveChanges();

					var Company = db.Companies.Find(TC.CompanyId);
					//Company.Testers.Add(newTester);
					Company.Testers.Add(TC.Tester);
					db.Companies.AddOrUpdate(Company);
					//db.SaveChanges();
				}
				//else
				//{
				//	existingTester.Password = tester.Password;
				//	existingTester.Companies.Clear();
				//	existingTester.Companies.Add(tester.Companies.FirstOrDefault());
				//	db.Testers.AddOrUpdate(existingTester);
				//}
				

				await db.SaveChangesAsync();
				return Ok();
			}
            catch (DbUpdateException)
            {
                throw;
            }
        }

        // GET: odata/NewTester(5)/Companies
        [EnableQuery]
        public IQueryable<Company> GetCompanies([FromODataUri] Guid key)
        {
            return db.Testers.Where(m => m.Id == key).SelectMany(m => m.Companies);
        }

		private bool TesterExists(Guid key)
		{
			return db.Testers.Count(e => e.Id == key) > 0;
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
