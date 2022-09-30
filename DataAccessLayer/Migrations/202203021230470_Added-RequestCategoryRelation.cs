namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequestCategoryRelation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Requests", name: "RequestCategory_RequestCategory_Id", newName: "RequestCategory_ID");
            RenameIndex(table: "dbo.Requests", name: "IX_RequestCategory_RequestCategory_Id", newName: "IX_RequestCategory_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Requests", name: "IX_RequestCategory_ID", newName: "IX_RequestCategory_RequestCategory_Id");
            RenameColumn(table: "dbo.Requests", name: "RequestCategory_ID", newName: "RequestCategory_RequestCategory_Id");
        }
    }
}
