namespace LibraryAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Born = c.Int(),
                        Died = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        YearPublished = c.Int(),
                        Condition = c.String(),
                        AuthorID = c.Int(),
                        GenreID = c.Int(),
                        ISBN = c.String(),
                        IsCheckedOut = c.Boolean(nullable: false),
                        DueBackDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authors", t => t.AuthorID)
                .ForeignKey("dbo.Genres", t => t.GenreID)
                .Index(t => t.AuthorID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Checkouts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(),
                        TimeStamp = c.DateTime(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.BookID)
                .Index(t => t.BookID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Checkouts", "BookID", "dbo.Books");
            DropForeignKey("dbo.Books", "GenreID", "dbo.Genres");
            DropForeignKey("dbo.Books", "AuthorID", "dbo.Authors");
            DropIndex("dbo.Checkouts", new[] { "BookID" });
            DropIndex("dbo.Books", new[] { "GenreID" });
            DropIndex("dbo.Books", new[] { "AuthorID" });
            DropTable("dbo.Checkouts");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
