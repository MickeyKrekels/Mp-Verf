namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStoreItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreItem", "Customer_Id", c => c.Guid());
            CreateIndex("dbo.StoreItem", "Customer_Id");
            AddForeignKey("dbo.StoreItem", "Customer_Id", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreItem", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.StoreItem", new[] { "Customer_Id" });
            DropColumn("dbo.StoreItem", "Customer_Id");
        }
    }
}
