namespace Repositorie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimesVotedAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserComment", "TimesVoted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserComment", "TimesVoted");
        }
    }
}
