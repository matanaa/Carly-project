namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarDetailsDataBaseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        OriginCountry = c.String(),
                        DegemName = c.String(),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarDetails");
        }
    }
}
