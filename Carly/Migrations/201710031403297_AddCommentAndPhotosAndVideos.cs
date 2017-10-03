namespace Carly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentAndPhotosAndVideos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DegemID = c.Int(nullable: false),
                        Author = c.String(nullable: false),
                        ContentInfo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Degems", t => t.DegemID, cascadeDelete: true)
                .Index(t => t.DegemID);
            
            AddColumn("dbo.Degems", "ImagesLinks", c => c.String());
            AddColumn("dbo.Degems", "VideoLink", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "DegemID", "dbo.Degems");
            DropIndex("dbo.Comments", new[] { "DegemID" });
            DropColumn("dbo.Degems", "VideoLink");
            DropColumn("dbo.Degems", "ImagesLinks");
            DropTable("dbo.Comments");
        }
    }
}
