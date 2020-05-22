namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.StoreItem", new[] { "Customer_Id" });
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StoreItemId = c.Guid(nullable: false),
                        Customer_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Customer_Id);
            
            //DropColumn("dbo.StoreItem", "Customer_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.StoreItem", "Customer_Id", c => c.Guid());
            DropIndex("dbo.ShoppingCart", new[] { "Customer_Id" });
            DropTable("dbo.ShoppingCart");
            //CreateIndex("dbo.StoreItem", "Customer_Id");
        }
    }
}
