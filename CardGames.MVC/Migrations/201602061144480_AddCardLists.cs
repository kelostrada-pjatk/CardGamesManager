namespace CardGames.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardLists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Public = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Editions", "EditionCardListId", c => c.Int(nullable: false));
            CreateIndex("dbo.Editions", "EditionCardListId");
            AddForeignKey("dbo.Editions", "EditionCardListId", "dbo.CardLists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Editions", "EditionCardListId", "dbo.CardLists");
            DropIndex("dbo.Editions", new[] { "EditionCardListId" });
            DropColumn("dbo.Editions", "EditionCardListId");
            DropTable("dbo.CardLists");
        }
    }
}
