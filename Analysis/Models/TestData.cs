using Roi.Data;

namespace Roi.Analysis.Api.Models
{
    public class TestData
    {
        public TestInfo test { get; set; }
    }

    public class TestInfo
    {
        public Test test { get; set; } = new Test();
        private SubjectInfo s;
        public SubjectInfo subject { get { if (test.OpId == null) test.OpId = ""; return s; } set { s = value; test.OpId = s.opid; test.Dob = s.dob;} }
        public string uuid { get { return test.UuId; } set { test.UuId = value; s.uuid = value; s.subject.UuId = s.uuid; } }
        public long time { get { return test.UnixTimeStamp; } set { test.UnixTimeStamp = value; } }
        public string history { get { return test.History; } set { test.History = value; } }
        public string tester { get { return test.Tester.Name; } set { test.Tester.Name = value; } }
        public string company { get; set; }
        public byte[] r2 { get; set; }
        public byte[] r3 { get; set; }
        public byte[] l2 { get; set; }
        public byte[] l3 { get; set; }
        public byte[] hardware { get; set; }

    }
}