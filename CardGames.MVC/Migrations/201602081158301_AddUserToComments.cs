namespace CardGames.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropColumn("dbo.Comments", "UserId");
        }
    }
}
