namespace BankApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankUsers", "UserIdentityId", c => c.String());
            AddColumn("dbo.CardInfoes", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CardInfoes", "IsActive");
            DropColumn("dbo.BankUsers", "UserIdentityId");
        }
    }
}
