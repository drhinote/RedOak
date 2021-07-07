using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roi.Analysis.Api.Models
{
	public class RawTestData
	{
		public long time { get; set; }
		public string uuid { get; set; }
		public string path { get; set; }
		public byte[] data { get; set; }
		public string args { get; set; }
		public string company { get; set; }
	}
}