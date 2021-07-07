namespace Roi.Data.OldDb
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OldDbCtx : DbContext
    {
        public OldDbCtx()
            : base("name=OldDbCtx")
        {
        }

        public virtual DbSet<authorized_ids> authorized_ids { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<machine> machines { get; set; }
        public virtual DbSet<subject> subjects { get; set; }
        public virtual DbSet<test> tests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(e => e.authorized_ids)
                .WithOptional(e => e.Company1)
                .HasForeignKey(e => e.Company_Id);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.machines)
                .WithOptional(e => e.Company1)
                .HasForeignKey(e => e.Company_Id);
        }
    }
}
