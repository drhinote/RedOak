using System;
namespace Roi.Data.Models
{
    public class TestSearch
	{
		/// <summary>
		/// Initializes a new instance of the TestSearch class.
		/// </summary>
		public TestSearch() { }

		/// <summary>
		/// Initializes a new instance of the TestSearch class.
		/// </summary>
		public TestSearch(string dob, string ssn, string opid, string uuid, DateTime startDate, DateTime endDate)
		{
			Dob = dob;
			Ssn = ssn;
			Opid = opid;
			Uuid = uuid;
			StartDate = startDate;
			EndDate = endDate;
		}
		public string Dob { get; set; }
		public string Ssn { get; set; }
		public string Opid { get; set; }
		public string Uuid { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
