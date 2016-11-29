namespace PublicLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addaavailabilitytoabook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "IsItAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "IsItAvailable");
        }
    }
}
