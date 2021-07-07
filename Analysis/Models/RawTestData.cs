namespace Roi.Data.Models
{
    public class RawTestData
    {
		public RawTestData() { }

		public RawTestData(long time, string uuid, string path, byte[] data, string args, string company)
		{
			Time = time;
			Uuid = uuid;
			Path = path;
			Data = data;
			Args = args;
			Company = company;
		}

		public long Time { get; set; }
        public string Uuid { get; set; }
        public string Path { get; set; }
        public byte[] Data { get; set; }
        public string Args { get; set; }
        public string Company { get; set; }
    }
}
