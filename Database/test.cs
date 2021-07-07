using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roi.Data
{
    public class Test : IEntity, ICompanySpecific
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UuId { get; set; }

        public long UnixTimeStamp { get; set; }

        public DateTimeOffset DateTime
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(UnixTimeStamp); }
            set {  }
        }

        public string OpId { get; set; }

        public string Dob { get; set; }

        public string History { get; set; }

        public string Analysis { get; set; }
        
        public Guid TesterId { get; set; }

        public virtual Tester Tester { get; set; }

        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
