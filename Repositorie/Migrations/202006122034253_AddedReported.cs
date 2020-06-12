namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReported : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentRating", "Reported", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserComment", "TimesVoted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserComment", "TimesVoted", c => c.Int(nullable: false));
            DropColumn("dbo.CommentRating", "Reported");
        }
    }
}
