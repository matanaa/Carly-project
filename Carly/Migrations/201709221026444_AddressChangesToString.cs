namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressChangesToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Address");
        }
    }
}
