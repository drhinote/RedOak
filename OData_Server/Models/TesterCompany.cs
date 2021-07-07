using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roi.Data
{
	public class TesterCompany
	{
		[Key]
		//public Guid TesterId { get; set; }
		//public string TesterName { get; set; }
		//public string TesterPassword { get; set; }
		public Guid CompanyId { get; set; }
		public Tester Tester { get; set; }
	}
}