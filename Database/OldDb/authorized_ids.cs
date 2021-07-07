namespace Roi.Data.OldDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class authorized_ids
    {
        [Key]
        [StringLength(50)]
        public string userid { get; set; }

        [Required]
        [StringLength(50)]
        public string company { get; set; }

        [Required]
        [StringLength(50)]
        public string paw { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? Company_Id { get; set; }

        public DateTime? LastUpdate { get; set; }

        public virtual Company Company1 { get; set; }
    }
}
