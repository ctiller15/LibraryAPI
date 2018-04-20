namespace LibraryAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookStatusPropertyToCheckout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checkouts", "BookStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Checkouts", "BookStatus");
        }
    }
}
