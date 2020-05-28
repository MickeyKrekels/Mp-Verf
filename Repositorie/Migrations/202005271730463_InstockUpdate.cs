namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstockUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreItem", "InStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreItem", "InStock");
        }
    }
}
