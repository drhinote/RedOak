namespace Roi.Data.OldDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class subject
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string dob { get; set; }

        [Required]
        [StringLength(50)]
        public string social { get; set; }

        [StringLength(50)]
        public string opid { get; set; }

        [Key]
        [StringLength(32)]
        public string uuid { get; set; }

        public string history { get; set; }

        [Required]
        [StringLength(50)]
        public string company { get; set; }

        public long num { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
