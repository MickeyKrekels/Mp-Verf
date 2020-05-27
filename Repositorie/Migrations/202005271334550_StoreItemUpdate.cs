namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreItemUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCart", "Amount", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingCart", "DataCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCart", "DataCreated");
            DropColumn("dbo.ShoppingCart", "Amount");
        }
    }
}
