using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Roi.Analysis.Api.Controllers
{
    public class IsRegisteredController : ControllerBase
    {
        // GET api/<controller>/5
        public bool Get(string id) => Ctx.machines.Any(m => m.name == id);
    }
}