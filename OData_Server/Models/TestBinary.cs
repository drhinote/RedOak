using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Roi.Data
{
   
    public class TestBinary
    {
        [Key]
        public Guid TestId { get; set; }

        public string TestSegment { get; set; }

        public string Data { get; set; }

        public int Size { get; set; }

        public string Hash { get; set; }

       
    }
}