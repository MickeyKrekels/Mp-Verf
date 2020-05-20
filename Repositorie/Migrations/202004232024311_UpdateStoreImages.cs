namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStoreImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreImage", "ImageData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreImage", "ImageData");
        }
    }
}
