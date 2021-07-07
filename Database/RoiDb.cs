namespace Roi.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EFHooks;
   
    public class RoiDb : HookedDbContext
    {
        static RoiDb()
        {
            Database.SetInitializer<RoiDb>(null);
        }

        public RoiDb()
        {
            RegisterHook(new SubjectMaskHook());
            RegisterHook(new SubjectMaskHook2());
            RegisterHook(new SubjectUnmaskHook());
            RegisterHook(new SubjectUnmaskHook2());
            RegisterHook(new SubjectUnmaskHook3());
            RegisterHook(new TesterHashHook());
            RegisterHook(new TesterHashLoadHook());
        }

        public virtual DbSet<Tester> Testers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

    }
}
