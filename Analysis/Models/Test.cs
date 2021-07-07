
namespace Roi.Data.Models
{
	public class Test
	{
		private object p1;
		private object p2;
		private object p3;

		public Test() { }

		public Test(string uuid, long time, string opid, string dob, byte[] hardware, string history, string tester, string company, byte[] right2, byte[] left2, byte[] right3, byte[] left3, string twoSymbol, string threeSymbol, object p1, object p2, object p3)
		{
			Uuid = uuid;
			Time = time;
			Opid = opid;
			Dob = dob;
			Hardware = hardware;
			History = history;
			Tester = tester;
			Company = company;
			Right2 = right2;
			Left2 = left2;
			Right3 = right3;
			Left3 = left3;
			TwoSymbol = twoSymbol;
			ThreeSymbol = threeSymbol;
			this.p1 = p1;
			this.p2 = p2;
			this.p3 = p3;
		}

		public string Uuid { get; set; }
		public long Time { get; set; }
		public string Opid { get; set; }
		public string Dob { get; set; }
		public byte[] Hardware { get; set; }
		public string History { get; set; }
		public byte[] Right2 { get; set; }
		public byte[] Left2 { get; set; }
		public byte[] Right3 { get; set; }
		public byte[] Left3 { get; set; }
		public string Tester { get; set; }
		public string Company { get; set; }
		public string TwoSymbol { get; set; }
		public string ThreeSymbol { get; set; }
		public byte[] Pdf1 { get; set; }
		public string SportsTable { get; set; }
		public byte[] Pdf0 { get; set; }
	}
}
