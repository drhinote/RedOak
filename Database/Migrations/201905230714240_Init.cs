namespace Roi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        private void CreateTrigger(string table)
        {
            Sql($"CREATE TRIGGER [dbo].[{table}Updated] ON [dbo].[{table}] AFTER INSERT, UPDATE AS UPDATE {table} SET LastUpdate=GETDATE() WHERE Id=(SELECT Id FROM inserted)");

        }
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Serial = c.String(maxLength: 32),
                        Enabled = c.Boolean(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.DeviceStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Data = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Testers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Info = c.String(),
                        Password = c.String(),
                        Active = c.Boolean(nullable: false),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        Name = c.String(),
                        Dob = c.String(),
                        Social = c.String(),
                        OpId = c.String(),
                        UuId = c.String(),
                        History = c.String(),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UuId = c.String(),
                        UnixTimeStamp = c.Long(nullable: false),
                        DateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        OpId = c.String(),
                        Dob = c.String(),
                        History = c.String(),
                        TesterId = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        LastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Testers", t => t.TesterId, cascadeDelete: true)
                .Index(t => t.TesterId)
                .Index(t => t.CompanyId);

            CreateTable(
                "dbo.TesterCompanies",
                c => new
                {
                    Tester_Id = c.Guid(nullable: false),
                    Company_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.Tester_Id, t.Company_Id })
                .ForeignKey("dbo.Testers", t => t.Tester_Id, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.Company_Id, cascadeDelete: true)
                .Index(t => t.Tester_Id)
                .Index(t => t.Company_Id);
            CreateTrigger("Companies");
            CreateTrigger("Devices");
            CreateTrigger("DeviceStatus");
            CreateTrigger("Testers");
            CreateTrigger("Subjects");
            CreateTrigger("Tests");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "TesterId", "dbo.Testers");
            DropForeignKey("dbo.Tests", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Subjects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.TesterCompanies", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.TesterCompanies", "Tester_Id", "dbo.Testers");
            DropForeignKey("dbo.DeviceStatus", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "CompanyId", "dbo.Companies");
            DropIndex("dbo.TesterCompanies", new[] { "Company_Id" });
            DropIndex("dbo.TesterCompanies", new[] { "Tester_Id" });
            DropIndex("dbo.Tests", new[] { "CompanyId" });
            DropIndex("dbo.Tests", new[] { "TesterId" });
            DropIndex("dbo.Subjects", new[] { "CompanyId" });
            DropIndex("dbo.DeviceStatus", new[] { "DeviceId" });
            DropIndex("dbo.Devices", new[] { "CompanyId" });
            DropTable("dbo.TesterCompanies");
            DropTable("dbo.Tests");
            DropTable("dbo.Subjects");
            DropTable("dbo.Testers");
            DropTable("dbo.DeviceStatus");
            DropTable("dbo.Devices");
            DropTable("dbo.Companies");
        }
    }
}
