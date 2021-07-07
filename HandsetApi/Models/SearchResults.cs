using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roi.Analysis.Api.Models
{
	public class SearchResults
	{
		public string Company { get; set; }
		public long Time { get; set; }
		public string Dob { get; set; }
		public string Opid { get; set; }
		public string Tester { get; set; }
		public string Uuid { get; set; }
	}
}