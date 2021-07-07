namespace Roi.Data
{
    using System;

    public enum StatusCode
    {
        Ordered,
        Assembly,
        Calibration,
        Inventory,
        DemoUnit,
        Active,
        Repair
    }

    public class DeviceStatus
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid DeviceId { get; set; }

        public virtual Device Device { get; set; }

        public StatusCode Status { get; set; }

		public DateTime Date { get; set; }

        public string Data { get; set; }
        
        public DateTime? LastUpdate { get; set; }
    }
}
