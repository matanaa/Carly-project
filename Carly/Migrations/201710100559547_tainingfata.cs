namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tainingfata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainingDatas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        word = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrainingDatas");
        }
    }
}
