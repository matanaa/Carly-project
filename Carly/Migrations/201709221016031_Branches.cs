namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Branches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchId = c.Int(nullable: false, identity: true),
                        BranchName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Branches");
        }
    }
}
