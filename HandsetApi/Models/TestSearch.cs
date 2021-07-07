using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roi.Analysis.Api.Models
{
	public class TestSearch
	{
		public string Dob { get; set; }
		public string Ssn { get; set; }
		public string Opid { get; set; }
		public string Uuid { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}