using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.OData;
using System.Web.Http;
using System.Security.Claims;
using System.Data.Entity.Migrations;
using System.Net.Http;

namespace OData_Server
{
    public abstract class ODataBase<T> : ODataController
        where T : class, IEntity, ICompanySpecific
    {
        public RoiDb Db { get; set; } = new RoiDb();

        public abstract DbSet<T> Set { get; }

        public virtual Func<T, IEnumerable<Guid>> GetCompanyIds => c => new[] { ((ICompanySpecific)c).CompanyId };

        public List<Guid> Companies => (User.Identity as ClaimsIdentity)?.Claims.Where(c => c.Type == ClaimTypes.GroupSid).Select(c => Guid.Parse(c.Value)).ToList();

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }

        [EnableQuery]
        public virtual IQueryable<T> Get()
        {
            var res = Set.Where(i => Companies.Contains(i.CompanyId)).ToList();
            return res.AsQueryable();
        }

        [EnableQuery]
        public virtual SingleResult<T> Get([FromODataUri] Guid key)
        {
            T result = Set.Find(key);
            if (Companies.Contains(result.CompanyId)) return SingleResult.Create(new List<T> { result }.AsQueryable());
            return SingleResult.Create(new List<T>().AsQueryable());
        }

        public virtual async Task<IHttpActionResult> Post(T item)
        {
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, item);
            response.Headers.Location = Request.RequestUri;
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          //  if(!Companies.Contains(item.CompanyId)) return BadRequest("Unauthorized update, user company mismatch");
            Set.AddOrUpdate(item);
            await Db.SaveChangesAsync();
            
            return ResponseMessage(response);
        }

        public virtual async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<T> item)
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
            if (!Companies.Contains(entity.CompanyId)) return BadRequest("Unauthorized update, user company mismatch");


            item.Patch(entity);

            await Db.SaveChangesAsync();
            return Updated(entity);

        }

        public virtual async Task<IHttpActionResult> Put([FromODataUri] Guid key, T update)
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
            if (!Companies.Contains(entity.CompanyId)) return BadRequest("Unauthorized update, user company mismatch");

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
            if (!Companies.Contains(entity.CompanyId)) return BadRequest("Unauthorized delete, user company mismatch");
            Set.Remove(entity);
            await Db.SaveChangesAsync();
            return Ok();

        }
    }
}