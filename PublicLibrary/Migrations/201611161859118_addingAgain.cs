namespace PublicLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Readers", "AccountNumber", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
