namespace PublicLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletesize : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Size", c => c.Int(nullable: false));
        }
    }
}
