namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Role_ID", c => c.Int());
            CreateIndex("dbo.User", "Role_ID");
            AddForeignKey("dbo.User", "Role_ID", "dbo.Role", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Role_ID", "dbo.Role");
            DropIndex("dbo.User", new[] { "Role_ID" });
            DropColumn("dbo.User", "Role_ID");
        }
    }
}
