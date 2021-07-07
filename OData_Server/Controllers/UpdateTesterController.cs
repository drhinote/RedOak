using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Roi.Data;

namespace OData_Server.Controllers
{
	[JwtAuth]
	[Authorize]
	public class UpdateTesterController : ODataController
    {
        private RoiDb db = new RoiDb();

        // POST: odata/UpdateTester
        public async Task<IHttpActionResult> Post(Tester tester)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Testers.Add(tester);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TesterExists(tester.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(tester);
        }

        // PATCH: odata/UpdateTester(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Tester> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tester tester = await db.Testers.FindAsync(key);
            if (tester == null)
            {
                return NotFound();
            }

            patch.Patch(tester);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TesterExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(tester);
        }

        // DELETE: odata/UpdateTester(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Tester tester = await db.Testers.FindAsync(key);
            if (tester == null)
            {
                return NotFound();
            }

            db.Testers.Remove(tester);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TesterExists(Guid key)
        {
            return db.Testers.Count(e => e.Id == key) > 0;
        }
    }
}
