namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReportTextv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentRating", "storeItemId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentRating", "storeItemId");
        }
    }
}
