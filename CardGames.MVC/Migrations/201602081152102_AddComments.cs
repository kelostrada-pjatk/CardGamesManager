namespace CardGames.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Content = c.String(),
                        DeckId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardLists", t => t.DeckId, cascadeDelete: true)
                .Index(t => t.DeckId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "DeckId", "dbo.CardLists");
            DropIndex("dbo.Comments", new[] { "DeckId" });
            DropTable("dbo.Comments");
        }
    }
}
