namespace Roi.Analysis.Api.Models
{
	public class ViewTest
	{
		public string uuid { get; set; }
		public string time { get; set; }
		public bool sendTest { get; set; }
		public byte[] pdf { get; set; }
		public TestInfo test { get; set; }
		public bool onePage { get; set; }
	}
}