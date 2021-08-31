namespace CMP331Practical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AmountDue = c.Single(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        PropertyId = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Available = c.Boolean(nullable: false),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        PostCode = c.String(),
                        MonthlyRent = c.Single(nullable: false),
                        RequiredMaintainance = c.String(),
                        QuarterlyInspection = c.DateTime(nullable: false),
                        AnnualGasInspection = c.DateTime(nullable: false),
                        FiveYearElectricalInspection = c.DateTime(nullable: false),
                        MaintainanceStaffId = c.String(),
                        LettingAgentId = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Properties");
            DropTable("dbo.Invoices");
        }
    }
}
