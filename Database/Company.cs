using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roi.Data
{
    public class Company : IEntity, ICompanySpecific
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [NotMapped]
        public Guid CompanyId { get { return Id; } set { Id = value; } }
      
        public string Name { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }
        
        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Device> Devices { get; set; } = new HashSet<Device>();

        public virtual ICollection<Tester> Testers { get; set; } = new HashSet<Tester>();

        public DateTime? LastUpdate { get; set; }
    }
}
