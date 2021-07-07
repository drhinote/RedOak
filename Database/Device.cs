namespace Roi.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Device : ICompanySpecific, IEntity
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(32)]
        public string Serial { get; set; }

        public bool Enabled { get; set; } = true;

        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<DeviceStatus> DeviceStatuses { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
