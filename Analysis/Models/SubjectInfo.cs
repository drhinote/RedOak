
namespace Roi.Data.Models
{
    public class SubjectInfo
    {
        /// <summary>
        /// Initializes a new instance of the SubjectInfo class.
        /// </summary>
        public SubjectInfo() { }

        /// <summary>
        /// Initializes a new instance of the SubjectInfo class.
        /// </summary>
        public SubjectInfo(Subject subject, string name, string dob, int social, string opid, string uuid, long time)
        {
            Subject = subject;
            Name = name;
            Dob = dob;
            Social = social;
            Opid = opid;
            Uuid = uuid;
            Time = time;
        }

        public Subject Subject { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public int? Social { get; set; }
        public string Opid { get; set; }
        public string Uuid { get; set; }
        public long Time { get; set; }
    }
}
