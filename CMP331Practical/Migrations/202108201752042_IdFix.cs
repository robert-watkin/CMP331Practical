namespace CMP331Practical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "RoleId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
        }
    }
}
