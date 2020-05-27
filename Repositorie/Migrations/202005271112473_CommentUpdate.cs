namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserComment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        DataCreated = c.DateTime(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        UserComment_Id = c.Guid(),
                        StoreItem_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserComment", t => t.UserComment_Id)
                .ForeignKey("dbo.StoreItem", t => t.StoreItem_Id)
                .Index(t => t.UserComment_Id)
                .Index(t => t.StoreItem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserComment", "StoreItem_Id", "dbo.StoreItem");
            DropForeignKey("dbo.UserComment", "UserComment_Id", "dbo.UserComment");
            DropIndex("dbo.UserComment", new[] { "StoreItem_Id" });
            DropIndex("dbo.UserComment", new[] { "UserComment_Id" });
            DropTable("dbo.UserComment");
        }
    }
}
