namespace CMP331Practical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "Bedrooms", c => c.Int(nullable: false));
            AddColumn("dbo.Properties", "Bathrooms", c => c.Int(nullable: false));
            AddColumn("dbo.Properties", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "Type");
            DropColumn("dbo.Properties", "Bathrooms");
            DropColumn("dbo.Properties", "Bedrooms");
        }
    }
}
