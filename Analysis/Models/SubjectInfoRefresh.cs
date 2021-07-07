using Roi.Data;
using System.Collections.Generic;

namespace Roi.Analysis.Api.Models
{
    public class SubjectInfo
    {
        public Subject subject { get; set; } = new Subject();
        public string name { get { return subject.Name; } set { subject.Name = value; } }
        public string dob { get { return subject.Dob; } set { subject.Dob = value; } }
        public int social { get { int res = 0; int.TryParse(subject.Social, out res); return res; } set { subject.Social = value.ToString(); } }
        public string opid { get { return subject.OpId??""; } set { subject.OpId = value; } } 
        public string uuid { get { return subject.UuId; } set { subject.UuId = value; } }
    }

    public class SubjectInfoRefresh
    {
        public string serverNum { get; set; }
        public string localNum { get; set; }
        public List<SubjectInfo> updatedInfo { get; set; } = new List<SubjectInfo>();
    }
}