namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryTAble : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 150),
                        ParentID = c.Int(nullable: false),
                        URL = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.News", "Category_ID", c => c.Int());
            CreateIndex("dbo.News", "Category_ID");
            AddForeignKey("dbo.News", "Category_ID", "dbo.Category", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Category_ID", "dbo.Category");
            DropIndex("dbo.News", new[] { "Category_ID" });
            DropColumn("dbo.News", "Category_ID");
            DropTable("dbo.Category");
        }
    }
}
