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
                        CardInCardList_CardId = c.Int(),
                        CardInCardList_CardListId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardInCardLists", t => new { t.CardInCardList_CardId, t.CardInCardList_CardListId })
                .Index(t => new { t.CardInCardList_CardId, t.CardInCardList_CardListId });
            
            AddColumn("dbo.CardLists", "CardInCardList_CardId", c => c.Int());
            AddColumn("dbo.CardLists", "CardInCardList_CardListId", c => c.Int());
            CreateIndex("dbo.CardLists", new[] { "CardInCardList_CardId", "CardInCardList_CardListId" });
            AddForeignKey("dbo.CardLists", new[] { "CardInCardList_CardId", "CardInCardList_CardListId" }, "dbo.CardInCardLists", new[] { "CardId", "CardListId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardInCardLists", "CardListId", "dbo.CardLists");
            DropForeignKey("dbo.Cards", new[] { "CardInCardList_CardId", "CardInCardList_CardListId" }, "dbo.CardInCardLists");
            DropForeignKey("dbo.CardInCardLists", "CardId", "dbo.Cards");
            DropForeignKey("dbo.CardLists", new[] { "CardInCardList_CardId", "CardInCardList_CardListId" }, "dbo.CardInCardLists");
            DropIndex("dbo.Cards", new[] { "CardInCardList_CardId", "CardInCardList_CardListId" });
            DropIndex("dbo.CardInCardLists", new[] { "CardListId" });
            DropIndex("dbo.CardInCardLists", new[] { "CardId" });
            DropIndex("dbo.CardLists", new[] { "CardInCardList_CardId", "CardInCardList_CardListId" });
            DropColumn("dbo.CardLists", "CardInCardList_CardListId");
            DropColumn("dbo.CardLists", "CardInCardList_CardId");
            DropTable("dbo.Cards");
            DropTable("dbo.CardInCardLists");
        }
    }
}
