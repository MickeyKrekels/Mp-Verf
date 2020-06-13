namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReportText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentRating", "ReportText", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentRating", "ReportText");
        }
    }
}
