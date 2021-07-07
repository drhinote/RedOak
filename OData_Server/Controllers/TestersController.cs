using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OData_Server
{
    [JwtAuth]
    [System.Web.Http.Authorize]
    public class TestersController : ODataController
    {
       
        public RoiDb Db { get; set; } = new RoiDb();

        public DbSet<Tester> Set => Db.Testers;
        public Func<Tester, IEnumerable<Guid>> GetCompanyIds => t => t.Companies.Select(c => c.Id);

        public List<Guid> Companies => (User.Identity as ClaimsIdentity)?.Claims.Where(c => c.Type == ClaimTypes.GroupSid).Select(c => Guid.Parse(c.Value)).ToList();

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }

		[EnableQuery]
		public IQueryable<Tester> Get()
		{

			return Set.Where(i => Companies.Any(g => i.Companies.Any(c => c.Id == g)));
		}

		[EnableQuery]
		public SingleResult<Tester> Get([FromODataUri] Guid key)
		{
			Tester result = Set.Find(key);
			if (Companies.Any(g => result.Companies.Any(c => c.Id == g))) return SingleResult.Create(new List<Tester> { result }.AsQueryable());
			return SingleResult.Create(new List<Tester>().AsQueryable());
		}

		public virtual async Task<IHttpActionResult> Post(Tester item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oldTest = Db.Testers.FirstOrDefault(t => t.Name == item.Name);
            if(oldTest == null && !item.Companies.Any())
            {
                Db.Companies.Where(c => Companies.Contains(c.Id)).ToList().ForEach(c => item.Companies.Add(c));
                Set.Add(item);
            }
            else if(oldTest != null && oldTest.Companies.Any(c => Companies.Contains(c.Id)))
            {
                if (oldTest.Active && !item.Active)
                {
                    oldTest.Companies.Where(c => Companies.Contains(c.Id)).ToList().ForEach(c =>
                        oldTest.Companies.Remove(c));
                    if (!oldTest.Companies.Any()) Db.Testers.Remove(oldTest);
                }
                else if(item.Active)
                {
                    oldTest.Name = item.Name;
                    oldTest.Password = item.Password;
                    oldTest.Active = item.Active;
                    oldTest.Info = item.Info;
                }
            }
            else if (oldTest != null && !oldTest.Companies.Any(c => Companies.Contains(c.Id)))
            {
                Db.Companies.Where(c => Companies.Contains(c.Id)).ToList().ForEach(c => oldTest.Companies.Add(c));
            }
            else if (oldTest == null && item.Companies.Any())
            {
                Set.Add(item);
            }
            await Db.SaveChangesAsync();
            return Ok();
        }

        public virtual async Task<IHttpActionResult> Patch([FromODataUri] Guid key, [FromBody] Delta<Tester> item)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var entity = await Set.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            if (!Companies.Any(g => entity.Companies.Any(c => c.Id == g))) return BadRequest("Unauthorized update, user company mismatch");


            item.Patch(entity);

            await Db.SaveChangesAsync();
            return Updated(entity);

        }

        public virtual async Task<IHttpActionResult> Put([FromODataUri] Guid key, Tester update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (update.Id != key)
            {
                return BadRequest("Unable to update primary key value");
            }
            var entity = await Set.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            if (!Companies.Any(g => entity.Companies.Any(c => c.Id == g))) return BadRequest("Unauthorized update, user company mismatch");

            Db.Entry(update).State = EntityState.Modified;
            await Db.SaveChangesAsync();
            return Updated(update);

        }

		public virtual async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
		{

			var entity = await Set.FindAsync(key);
			if (entity == null)
			{
				return NotFound();
			}
			Set.Remove(entity);
			await Db.SaveChangesAsync();
			return Ok();

		}

	}
}