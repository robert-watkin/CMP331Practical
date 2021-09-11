namespace CMP331Practical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "MonthlyRent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "MonthlyRent", c => c.Single(nullable: false));
        }
    }
}
