namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BranchAddressCityCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "City", c => c.String(nullable: false));
            AddColumn("dbo.Branches", "Country", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Country");
            DropColumn("dbo.Branches", "City");
        }
    }
}
