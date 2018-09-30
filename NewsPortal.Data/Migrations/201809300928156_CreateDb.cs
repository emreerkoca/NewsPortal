namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 150),
                        ParentID = c.Int(nullable: false),
                        URL = c.String(maxLength: 150),
                        UploadDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Reads = c.Int(nullable: false),
                        ImageStr = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Category_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.Category_ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .Index(t => t.Category_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        News_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.News", t => t.News_ID)
                .Index(t => t.News_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        UploadDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Role_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.Role_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 150),
                        UploadDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "User_ID", "dbo.User");
            DropForeignKey("dbo.News", "User_ID", "dbo.User");
            DropForeignKey("dbo.User", "Role_ID", "dbo.Role");
            DropForeignKey("dbo.Image", "News_ID", "dbo.News");
            DropForeignKey("dbo.News", "Category_ID", "dbo.Category");
            DropIndex("dbo.User", new[] { "Role_ID" });
            DropIndex("dbo.Image", new[] { "News_ID" });
            DropIndex("dbo.News", new[] { "User_ID" });
            DropIndex("dbo.News", new[] { "Category_ID" });
            DropIndex("dbo.Category", new[] { "User_ID" });
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Image");
            DropTable("dbo.News");
            DropTable("dbo.Category");
        }
    }
}
