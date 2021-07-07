using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roi.Data
{
    public class Subject : IEntity, ICompanySpecific
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public string Name { get; set; }

        public string Dob { get; set; }

        public string Social { get; set; }

        public string OpId { get; set; }

        public string UuId { get; set; }

        public string History { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
