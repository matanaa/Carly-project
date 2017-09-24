namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false),
                        OriginCountry = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Degems",
                c => new
                    {
                        DegemId = c.Int(nullable: false, identity: true),
                        DegemName = c.String(nullable: false),
                        Color = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        BrandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DegemId)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .Index(t => t.BrandID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Degems", "BrandID", "dbo.Brands");
            DropIndex("dbo.Degems", new[] { "BrandID" });
            DropTable("dbo.Degems");
            DropTable("dbo.Brands");
        }
    }
}
