namespace Roi.Data.Models
{
	public class SearchResults
	{
		/// <summary>
		/// Initializes a new instance of the SearchResults class.
		/// </summary>
		public SearchResults() { }

		/// <summary>
		/// Initializes a new instance of the SearchResults class.
		/// </summary>
		public SearchResults(string company, long time, string dob, string opid, string tester, string uuid)
		{
			Company = company;
			Time = time;
			Dob = dob;
			Opid = opid;
			Tester = tester;
			Uuid = uuid;
		}

		public string Company { get; set; }
		public long Time { get; set; }
		public string Dob { get; set; }
		public string Opid { get; set; }
		public string Tester { get; set; }
		public string Uuid { get; set; }
	}
}
