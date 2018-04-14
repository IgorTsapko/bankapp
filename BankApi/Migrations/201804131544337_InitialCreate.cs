namespace BankApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankBranches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        PhoneNum = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(nullable: false),
                        CardName = c.String(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CVV = c.String(nullable: false, maxLength: 3),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        IsCredit = c.Boolean(nullable: false),
                        BankUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankUsers", t => t.BankUser_Id)
                .Index(t => t.BankUser_Id);
            
            CreateTable(
                "dbo.PayInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false),
                        Card_Id = c.Int(nullable: false),
                        BankUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardInfoes", t => t.Card_Id, cascadeDelete: true)
                .ForeignKey("dbo.BankUsers", t => t.BankUser_Id)
                .Index(t => t.Card_Id)
                .Index(t => t.BankUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayInfoes", "BankUser_Id", "dbo.BankUsers");
            DropForeignKey("dbo.PayInfoes", "Card_Id", "dbo.CardInfoes");
            DropForeignKey("dbo.CardInfoes", "BankUser_Id", "dbo.BankUsers");
            DropIndex("dbo.PayInfoes", new[] { "BankUser_Id" });
            DropIndex("dbo.PayInfoes", new[] { "Card_Id" });
            DropIndex("dbo.CardInfoes", new[] { "BankUser_Id" });
            DropTable("dbo.PayInfoes");
            DropTable("dbo.CardInfoes");
            DropTable("dbo.BankUsers");
            DropTable("dbo.BankBranches");
        }
    }
}
