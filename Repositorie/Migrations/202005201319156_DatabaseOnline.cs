namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseOnline : DbMigration
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
                "dbo.StoreImage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageData = c.Binary(),
                        StoreItem_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoreItem", t => t.StoreItem_Id)
                .Index(t => t.StoreItem_Id);
            
            CreateTable(
                "dbo.StoreItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Discription = c.String(),
                        Brand = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Specification", "StoreItem_Id", "dbo.StoreItem");
            DropForeignKey("dbo.StoreImage", "StoreItem_Id", "dbo.StoreItem");
            DropIndex("dbo.Specification", new[] { "StoreItem_Id" });
            DropIndex("dbo.StoreImage", new[] { "StoreItem_Id" });
            DropTable("dbo.Specification");
            DropTable("dbo.StoreItem");
            DropTable("dbo.StoreImage");
            DropTable("dbo.Customer");
            DropTable("dbo.Admin");
        }
    }
}
