namespace PublicLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingbookings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReaderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Readers", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.ReaderId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "ReaderId", "dbo.Readers");
            DropForeignKey("dbo.Bookings", "BookId", "dbo.Books");
            DropIndex("dbo.Bookings", new[] { "BookId" });
            DropIndex("dbo.Bookings", new[] { "ReaderId" });
            DropTable("dbo.Bookings");
        }
    }
}
