namespace Roi.Data.OldDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            authorized_ids = new HashSet<authorized_ids>();
            machines = new HashSet<machine>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [Column("company")]
        [StringLength(50)]
        public string company1 { get; set; }

        [StringLength(50)]
        public string contact { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        [StringLength(10)]
        public string zip { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? LastUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<authorized_ids> authorized_ids { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<machine> machines { get; set; }
    }
}


