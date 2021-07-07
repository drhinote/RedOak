using Microsoft.AspNet.OData;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OData_Server
{
    [JwtAuth]
    [Authorize]
    public class CompaniesController : ODataBase<Company>
    {
        public override DbSet<Company> Set => Db.Companies;

		[EnableQuery]
        public override IQueryable<Company> Get()
        {
            var res = Set.Where(i => Companies.Contains(i.Id)).ToList();
			var active = new List<Company>();
			foreach( var a in res)
			{
				if (a.IsActive == true)
				{
					active.Add(a);
				}
			}
			return active.AsQueryable();
			//return res.AsQueryable();
		}

        [EnableQuery]
        public override SingleResult<Company> Get([FromODataUri] Guid key)
        {
            Company result = Set.Find(key);
            if (Companies.Contains(result.Id)) return SingleResult.Create(new List<Company> { result }.AsQueryable());
            return SingleResult.Create(new List<Company>().AsQueryable());
        }

		public override async Task<IHttpActionResult> Post(Company company)
		{
			try
			{
				var testers = company.Testers.ToList();
				var existingCompany = Set.Find(company.Id);
				if (existingCompany == null)
				{
					company.Testers.Clear();
					Set.Add(company);
					Db.SaveChanges();
					existingCompany = Set.Find(company.Id);
				}
				foreach(var tester in testers)
				{
					var currentTester = Db.Testers.Find(tester.Id);
					if (!existingCompany.Testers.Contains(currentTester))
					{
						company.Testers.Add(currentTester);
					}
				}
			
				Set.AddOrUpdate(company);
				
				await Db.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}