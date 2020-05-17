namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateshoppingCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoreItem", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.StoreItem", new[] { "Customer_Id" });
            DropColumn("dbo.StoreItem", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StoreItem", "Customer_Id", c => c.Guid());
            CreateIndex("dbo.StoreItem", "Customer_Id");
            AddForeignKey("dbo.StoreItem", "Customer_Id", "dbo.Customer", "Id");
        }
    }
}
