
namespace Roi.Data.Models
{
    public partial class TestInfo
    {
        /// <summary>
        /// Initializes a new instance of the TestInfo class.
        /// </summary>
        public TestInfo() { }

        /// <summary>
        /// Initializes a new instance of the TestInfo class.
        /// </summary>
        public TestInfo(Roi.Data.Test test, SubjectInfo subject, string uuid, long time, string history, byte[] r2, byte[] l2, byte[] r3, byte[] l3, string tester, byte[] hardware, string company)
        {
            Test = test;
            Subject = subject;
            Uuid = uuid;
            Time = time;
            History = history;
            R2 = r2;
            L2 = l2;
            R3 = r3;
            L3 = l3;
            Tester = tester;
            Hardware = hardware;
            Company = company;
        }

        public Roi.Data.Test Test { get; set; }
        public SubjectInfo Subject { get; set; }
        public string Uuid { get; set; }
        public long Time { get; set; }
        public string History { get; set; }
        public byte[] R2 { get; set; }
        public byte[] L2 { get; set; }
        public byte[] R3 { get; set; }
        public byte[] L3 { get; set; }
        public string Tester { get; set; }
        public byte[] Hardware { get; set; }
        public string Company { get; set; }
    }
}
