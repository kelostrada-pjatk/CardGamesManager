namespace CardGames.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardInList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardInCardLists",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        CardListId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardId, t.CardListId })
                .ForeignKey("dbo.Cards", t => t.CardId, cascadeDelete: true)
                .ForeignKey("dbo.CardLists", t => t.CardListId, cascadeDelete: true)
                .Index(t => t.CardId)
                .Index(t => t.CardListId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardInCardLists", "CardListId", "dbo.CardLists");
            DropForeignKey("dbo.CardInCardLists", "CardId", "dbo.Cards");
            DropIndex("dbo.CardInCardLists", new[] { "CardListId" });
            DropIndex("dbo.CardInCardLists", new[] { "CardId" });
            DropTable("dbo.Cards");
            DropTable("dbo.CardInCardLists");
        }
    }
}
