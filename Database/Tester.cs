namespace Roi.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    public partial class Tester : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();

        public string Name { get; set; }

        public string Info { get; set; }
                        
        public string Password { get; set; }

        public bool Active { get; set; }

        public DateTime? LastUpdate { get; set; }

    }
}
