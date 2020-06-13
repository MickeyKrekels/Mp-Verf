namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReportTextv3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommentRating", "ReportText", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommentRating", "ReportText", c => c.Boolean(nullable: false));
        }
    }
}
