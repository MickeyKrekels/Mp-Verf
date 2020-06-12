namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentRating",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                        UpVote = c.Boolean(nullable: false),
                        DownVote = c.Boolean(nullable: false),
                        DataCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserComment", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentRating", "CommentId", "dbo.UserComment");
            DropIndex("dbo.CommentRating", new[] { "CommentId" });
            DropTable("dbo.CommentRating");
        }
    }
}
