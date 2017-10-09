namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelteAdminTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Admins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        email = c.String(),
                        roleName = c.String(),
                        RequireUniqueEmail = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
