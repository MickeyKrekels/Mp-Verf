namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReportTextv4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserComment", "StoreItem_Id", "dbo.StoreItem");
            DropIndex("dbo.UserComment", new[] { "StoreItem_Id" });
            AlterColumn("dbo.UserComment", "StoreItem_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.UserComment", "StoreItem_Id");
            AddForeignKey("dbo.UserComment", "StoreItem_Id", "dbo.StoreItem", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserComment", "StoreItem_Id", "dbo.StoreItem");
            DropIndex("dbo.UserComment", new[] { "StoreItem_Id" });
            AlterColumn("dbo.UserComment", "StoreItem_Id", c => c.Guid());
            CreateIndex("dbo.UserComment", "StoreItem_Id");
            AddForeignKey("dbo.UserComment", "StoreItem_Id", "dbo.StoreItem", "Id");
        }
    }
}
