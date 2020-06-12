namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentRatingsV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentRating", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.UserComment", "ProductRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserComment", "ProductRating", c => c.Int(nullable: false));
            DropColumn("dbo.CommentRating", "Rating");
        }
    }
}
