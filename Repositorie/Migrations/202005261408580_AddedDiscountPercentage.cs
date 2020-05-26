namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDiscountPercentage : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.ShoppingCart", "Customer_Id", "dbo.Customer");
            //DropIndex("dbo.ShoppingCart", new[] { "Customer_Id" });
            AddColumn("dbo.StoreItem", "DiscountPercentage", c => c.Int(nullable: false));
            //AlterColumn("dbo.ShoppingCart", "Customer_Id", c => c.Guid(nullable: false));
           // CreateIndex("dbo.ShoppingCart", "Customer_Id");
            //AddForeignKey("dbo.ShoppingCart", "Customer_Id", "dbo.Customer", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.ShoppingCart", "Customer_Id", "dbo.Customer");
            //DropIndex("dbo.ShoppingCart", new[] { "Customer_Id" });
            //AlterColumn("dbo.ShoppingCart", "Customer_Id", c => c.Guid());
            DropColumn("dbo.StoreItem", "DiscountPercentage");
            //CreateIndex("dbo.ShoppingCart", "Customer_Id");
            //AddForeignKey("dbo.ShoppingCart", "Customer_Id", "dbo.Customer", "Id");
        }
    }
}
