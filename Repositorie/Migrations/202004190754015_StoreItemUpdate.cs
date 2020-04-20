namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreItemUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreImage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreItem_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreItem", t => t.StoreItem_Id)
                .Index(t => t.StoreItem_Id);
            
            AddColumn("dbo.StoreItem", "Brand", c => c.String());
            DropColumn("dbo.StoreItem", "BrandName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoreItem", "BrandName", c => c.String());
            DropForeignKey("dbo.StoreImage", "StoreItem_Id", "dbo.StoreItem");
            DropIndex("dbo.StoreImage", new[] { "StoreItem_Id" });
            DropColumn("dbo.StoreItem", "Brand");
            DropTable("dbo.StoreImage");
        }
    }
}
