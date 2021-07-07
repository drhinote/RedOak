namespace Roi.Data.OldDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class machine
    {
        [Key]
        [StringLength(50)]
        public string name { get; set; }

        public long time { get; set; }

        [Required]
        [StringLength(50)]
        public string company { get; set; }

        [StringLength(50)]
        public string company_name { get; set; }

        public Guid? Company_Id { get; set; }

        public int status { get; set; }

        public string device_id { get; set; }

        public DateTime? LastUpdate { get; set; }

        public virtual Company Company1 { get; set; }
    }
}
