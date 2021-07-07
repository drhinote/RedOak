using System.ComponentModel.DataAnnotations;

namespace Roi.Data.Models
{
    public class Search
    {
        public string Uuid { get; set; } // uuid (length: 50)
        [Key]
        public long Time { get; set; } // time (Primary key)
        public string Opid { get; set; } // opid (length: 50)
        public string Dob { get; set; } // dob (length: 50)
        public byte[] Hardware { get; set; } // hardware
        public string History { get; set; } // history
        public byte[] Right2 { get; set; } // right2
        public byte[] Left2 { get; set; } // left2
        public byte[] Right3 { get; set; } // right3
        public byte[] Left3 { get; set; } // left3
        public string Tester { get; set; } // tester (length: 50)
        public string Company { get; set; } // company (length: 50)
        public string TwoSymbol { get; set; } // TwoSymbol
        public string ThreeSymbol { get; set; } // ThreeSymbol
        public byte[] Pdf1 { get; set; } // pdf1
        public string SportsTable { get; set; } // SportsTable (length: 1000)
        public byte[] Pdf0 { get; set; } // pdf0
    }
}
