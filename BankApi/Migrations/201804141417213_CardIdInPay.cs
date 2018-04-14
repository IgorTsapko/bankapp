namespace BankApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CardIdInPay : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PayInfoes", "Card_Id", "dbo.CardInfoes");
            DropIndex("dbo.PayInfoes", new[] { "Card_Id" });
            AddColumn("dbo.PayInfoes", "CardId", c => c.Int(nullable: false));
            DropColumn("dbo.PayInfoes", "Card_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PayInfoes", "Card_Id", c => c.Int(nullable: false));
            DropColumn("dbo.PayInfoes", "CardId");
            CreateIndex("dbo.PayInfoes", "Card_Id");
            AddForeignKey("dbo.PayInfoes", "Card_Id", "dbo.CardInfoes", "Id", cascadeDelete: true);
        }
    }
}
