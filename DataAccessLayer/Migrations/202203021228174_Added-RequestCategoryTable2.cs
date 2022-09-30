namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequestCategoryTable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestCategories",
                c => new
                    {
                        RequestCategory_Id = c.Int(nullable: false, identity: true),
                        RequestCategory_Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RequestCategory_Id);
            
            AddColumn("dbo.Requests", "RequestCategory_RequestCategory_Id", c => c.Int());
            CreateIndex("dbo.Requests", "RequestCategory_RequestCategory_Id");
            AddForeignKey("dbo.Requests", "RequestCategory_RequestCategory_Id", "dbo.RequestCategories", "RequestCategory_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RequestCategory_RequestCategory_Id", "dbo.RequestCategories");
            DropIndex("dbo.Requests", new[] { "RequestCategory_RequestCategory_Id" });
            DropColumn("dbo.Requests", "RequestCategory_RequestCategory_Id");
            DropTable("dbo.RequestCategories");
        }
    }
}
