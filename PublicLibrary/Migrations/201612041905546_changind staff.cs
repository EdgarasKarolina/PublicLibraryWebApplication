namespace PublicLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changindstaff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "YearPublished", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Genre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Genre", c => c.String());
            AlterColumn("dbo.Books", "YearPublished", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
