using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roi.Data
{
    public interface IEntity
    {
        Guid Id { get; set; }

        DateTime? LastUpdate { get; set; }
    }
}
