namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentScoreUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserComment", "UserComment_Id", "dbo.UserComment");
            DropIndex("dbo.UserComment", new[] { "UserComment_Id" });
            AddColumn("dbo.UserComment", "ProductRating", c => c.Int(nullable: false));
            DropColumn("dbo.UserComment", "UserComment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserComment", "UserComment_Id", c => c.Guid());
            DropColumn("dbo.UserComment", "ProductRating");
            CreateIndex("dbo.UserComment", "UserComment_Id");
            AddForeignKey("dbo.UserComment", "UserComment_Id", "dbo.UserComment", "Id");
        }
    }
}
