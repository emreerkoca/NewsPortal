namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        News_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.News", t => t.News_ID)
                .Index(t => t.News_ID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Reads = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        ImageStr = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "User_Id", "dbo.User");
            DropForeignKey("dbo.Image", "News_ID", "dbo.News");
            DropIndex("dbo.News", new[] { "User_Id" });
            DropIndex("dbo.Image", new[] { "News_ID" });
            DropTable("dbo.News");
            DropTable("dbo.Image");
        }
    }
}
