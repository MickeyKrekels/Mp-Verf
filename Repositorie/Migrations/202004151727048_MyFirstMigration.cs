namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyFirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoreItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        BrandName = c.String(),
                        Discription = c.String(),
                        Price = c.Single(nullable: false),
                        Customer_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Specification",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        StoreItem_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreItem", t => t.StoreItem_Id)
                .Index(t => t.StoreItem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreItem", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.Specification", "StoreItem_Id", "dbo.StoreItem");
            DropIndex("dbo.Specification", new[] { "StoreItem_Id" });
            DropIndex("dbo.StoreItem", new[] { "Customer_Id" });
            DropTable("dbo.Specification");
            DropTable("dbo.StoreItem");
            DropTable("dbo.Customer");
            DropTable("dbo.Admin");
        }
    }
}
