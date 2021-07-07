namespace Roi.Data.Models
{
	public class Subject
	{
		public Subject(string name, string dob, int social, string uuid, string company, long num, string opid, string history)
		{
			Name = name;
			Dob = dob;
			Social = social;
			Uuid = uuid;
			Company = company;
			Num = num;
			Opid = opid;
			History = history;
		}

		public string Name { get; set; }
		public string Dob { get; set; }
		public int Social { get; set; }
		public string Opid { get; set; }
		public string Uuid { get; set; }
		public string History { get; set; }
		public string Company { get; set; }
		public long Num { get; set; }
	}
}
