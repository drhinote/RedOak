namespace Roi.Data.OldDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class test
    {
        [Required]
        [StringLength(50)]
        public string uuid { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long time { get; set; }

        [Required]
        [StringLength(50)]
        public string opid { get; set; }

        [Required]
        [StringLength(50)]
        public string dob { get; set; }

        [Required]
        public string history { get; set; }

        [Required]
        [StringLength(50)]
        public string tester { get; set; }

        [Required]
        [StringLength(50)]
        public string company { get; set; }

        public string TwoSymbol { get; set; }

        public string ThreeSymbol { get; set; }

        [StringLength(1000)]
        public string SportsTable { get; set; }

        public decimal? SampleRatePerSec { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
