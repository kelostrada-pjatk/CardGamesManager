namespace CardGames.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToCollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CardLists", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CardLists", "UserId");
            AddForeignKey("dbo.CardLists", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardLists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CardLists", new[] { "UserId" });
            DropColumn("dbo.CardLists", "UserId");
        }
    }
}
