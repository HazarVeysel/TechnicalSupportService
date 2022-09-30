namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageRalationToRequestTable : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Messages", "Department_Department_Id", "dbo.Departments");
            //DropIndex("dbo.Messages", new[] { "Department_Department_Id" });
            //RenameColumn(table: "dbo.Messages", name: "Department_Department_Id", newName: "Department_Id");
            //RenameColumn(table: "dbo.Messages", name: "User_User_Id", newName: "User_Id");
            //RenameIndex(table: "dbo.Messages", name: "IX_User_User_Id", newName: "IX_User_Id");
            AddColumn("dbo.Messages", "Request_Id", c => c.Int(nullable: false));
            //AlterColumn("dbo.Messages", "Department_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "Request_Id");
            //CreateIndex("dbo.Messages", "Department_Id");
            AddForeignKey("dbo.Messages", "Request_Id", "dbo.Requests", "Request_Id", cascadeDelete: true);
            //AddForeignKey("dbo.Messages", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Messages", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Messages", new[] { "Department_Id" });
            DropIndex("dbo.Messages", new[] { "Request_Id" });
            AlterColumn("dbo.Messages", "Department_Id", c => c.Int());
            DropColumn("dbo.Messages", "Request_Id");
            RenameIndex(table: "dbo.Messages", name: "IX_User_Id", newName: "IX_User_User_Id");
            RenameColumn(table: "dbo.Messages", name: "User_Id", newName: "User_User_Id");
            RenameColumn(table: "dbo.Messages", name: "Department_Id", newName: "Department_Department_Id");
            CreateIndex("dbo.Messages", "Department_Department_Id");
            AddForeignKey("dbo.Messages", "Department_Department_Id", "dbo.Departments", "Department_Id");
        }
    }
}
