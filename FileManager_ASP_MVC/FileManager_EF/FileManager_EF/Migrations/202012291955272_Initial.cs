namespace FileManager_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Extensions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Icon = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        DescriptionFile = c.String(),
                        FolderId = c.Int(nullable: false),
                        ExtensionId = c.Int(nullable: false),
                        Content = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Extensions", t => t.ExtensionId, cascadeDelete: true)
                .ForeignKey("dbo.Folders", t => t.FolderId, cascadeDelete: true)
                .Index(t => t.FolderId)
                .Index(t => t.ExtensionId);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FolderName = c.String(),
                        ParentFolder = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "FolderId", "dbo.Folders");
            DropForeignKey("dbo.Files", "ExtensionId", "dbo.Extensions");
            DropIndex("dbo.Files", new[] { "ExtensionId" });
            DropIndex("dbo.Files", new[] { "FolderId" });
            DropTable("dbo.Folders");
            DropTable("dbo.Files");
            DropTable("dbo.Extensions");
        }
    }
}
