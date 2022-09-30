namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Messages", new[] { "Department_Id" });
            RenameColumn(table: "dbo.Messages", name: "Department_Id", newName: "Department_Department_Id");
            RenameColumn(table: "dbo.Messages", name: "User_Id", newName: "User_User_Id");
            RenameIndex(table: "dbo.Messages", name: "IX_User_Id", newName: "IX_User_User_Id");
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Request_Id = c.Int(nullable: false, identity: true),
                        Request_Subject = c.String(maxLength: 250),
                        Request_Priority = c.Int(nullable: false),
                        Request_Status = c.Boolean(nullable: false),
                        Request_Undertaken = c.Boolean(nullable: false),
                        Request_Undertaken_Date = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Create_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Request_Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_Group_Id = c.Int(nullable: false, identity: true),
                        User_Group_Name = c.String(maxLength: 50),
                        User_Group_Note = c.String(maxLength: 100),
                        Is_Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.User_Group_Id);
            
            CreateTable(
                "dbo.UserPermissions",
                c => new
                    {
                        User_Permission_Id = c.Int(nullable: false, identity: true),
                        Is_Authorized = c.Boolean(nullable: false),
                        Is_Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.User_Permission_Id);
            
            AddColumn("dbo.Messages", "Message_Image", c => c.String());
            AddColumn("dbo.Messages", "Sent_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "Receiving_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Is_Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Create_Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Messages", "Department_Department_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Department_Department_Id");
            AddForeignKey("dbo.Messages", "Department_Department_Id", "dbo.Departments", "Department_Id");
            DropColumn("dbo.Messages", "Message_Subject");
            DropColumn("dbo.Messages", "Message_Image1");
            DropColumn("dbo.Messages", "Message_Image2");
            DropColumn("dbo.Messages", "Message_Image3");
            DropColumn("dbo.Messages", "Message_Severity");
            DropColumn("dbo.Messages", "Message_Answer");
            DropColumn("dbo.Messages", "Message_Answer_Owner_Id");
            DropColumn("dbo.Messages", "Message_Answer_Owner_Name");
            DropColumn("dbo.Messages", "Message_Answer_Owner_Surname");
            DropColumn("dbo.Messages", "Message_Undertaken");
            DropColumn("dbo.Messages", "Message_Undertaken_Date");
            DropColumn("dbo.Messages", "IsActive");
            DropColumn("dbo.Messages", "Create_Date");
            DropColumn("dbo.Messages", "End_Date");
            DropColumn("dbo.Messages", "User_Name");
            DropColumn("dbo.Messages", "User_Surname");
            DropColumn("dbo.Messages", "Department_Name");
            DropColumn("dbo.Users", "User_Admin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Department_Name", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "User_Admin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "Department_Name", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "User_Surname", c => c.String());
            AddColumn("dbo.Messages", "User_Name", c => c.String());
            AddColumn("dbo.Messages", "End_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "Create_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "Message_Undertaken_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "Message_Undertaken", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "Message_Answer_Owner_Surname", c => c.String(maxLength: 50));
            AddColumn("dbo.Messages", "Message_Answer_Owner_Name", c => c.String(maxLength: 50));
            AddColumn("dbo.Messages", "Message_Answer_Owner_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "Message_Answer", c => c.String());
            AddColumn("dbo.Messages", "Message_Severity", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "Message_Image3", c => c.String(maxLength: 100));
            AddColumn("dbo.Messages", "Message_Image2", c => c.String(maxLength: 100));
            AddColumn("dbo.Messages", "Message_Image1", c => c.String(maxLength: 100));
            AddColumn("dbo.Messages", "Message_Subject", c => c.String(maxLength: 100));
            DropForeignKey("dbo.Messages", "Department_Department_Id", "dbo.Departments");
            DropIndex("dbo.Messages", new[] { "Department_Department_Id" });
            AlterColumn("dbo.Messages", "Department_Department_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Create_Date");
            DropColumn("dbo.Users", "Is_Active");
            DropColumn("dbo.Messages", "Receiving_Date");
            DropColumn("dbo.Messages", "Sent_Date");
            DropColumn("dbo.Messages", "Message_Image");
            DropTable("dbo.UserPermissions");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Requests");
            RenameIndex(table: "dbo.Messages", name: "IX_User_User_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Messages", name: "User_User_Id", newName: "User_Id");
            RenameColumn(table: "dbo.Messages", name: "Department_Department_Id", newName: "Department_Id");
            CreateIndex("dbo.Messages", "Department_Id");
            AddForeignKey("dbo.Messages", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
    }
}
