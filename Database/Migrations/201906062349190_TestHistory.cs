namespace Roi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Analysis", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "Analysis");
        }
    }
}
