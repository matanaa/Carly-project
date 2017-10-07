namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Author", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Author", c => c.String(nullable: false));
        }
    }
}
