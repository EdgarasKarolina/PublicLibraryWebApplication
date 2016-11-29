namespace PublicLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Size", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Size");
        }
    }
}
