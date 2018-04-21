namespace LibraryAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCheckoutBooksManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Checkouts", "BookID", "dbo.Books");
            DropIndex("dbo.Checkouts", new[] { "BookID" });
            CreateTable(
                "dbo.CheckoutBooks",
                c => new
                    {
                        Checkout_ID = c.Int(nullable: false),
                        Book_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Checkout_ID, t.Book_ID })
                .ForeignKey("dbo.Checkouts", t => t.Checkout_ID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .Index(t => t.Checkout_ID)
                .Index(t => t.Book_ID);
            
            DropColumn("dbo.Checkouts", "BookID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Checkouts", "BookID", c => c.Int());
            DropForeignKey("dbo.CheckoutBooks", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.CheckoutBooks", "Checkout_ID", "dbo.Checkouts");
            DropIndex("dbo.CheckoutBooks", new[] { "Book_ID" });
            DropIndex("dbo.CheckoutBooks", new[] { "Checkout_ID" });
            DropTable("dbo.CheckoutBooks");
            CreateIndex("dbo.Checkouts", "BookID");
            AddForeignKey("dbo.Checkouts", "BookID", "dbo.Books", "ID");
        }
    }
}
