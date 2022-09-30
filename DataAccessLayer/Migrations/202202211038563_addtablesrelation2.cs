namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablesrelation2 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Users", "UserGroup_User_Group_Id", "dbo.UserGroups");
            //DropIndex("dbo.Users", new[] { "UserGroup_User_Group_Id" });
            //RenameColumn(table: "dbo.Users", name: "UserGroup_User_Group_Id", newName: "User_Group_Id");
            AddColumn("dbo.UserPermissions", "User_Group_Id", c => c.Int(nullable: false));
            //AlterColumn("dbo.Users", "User_Group_Id", c => c.Int(nullable: false));
            //CreateIndex("dbo.Users", "User_Group_Id");
            CreateIndex("dbo.UserPermissions", "User_Group_Id");
            AddForeignKey("dbo.UserPermissions", "User_Group_Id", "dbo.UserGroups", "User_Group_Id", cascadeDelete: true);
            //AddForeignKey("dbo.Users", "User_Group_Id", "dbo.UserGroups", "User_Group_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "User_Group_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserPermissions", "User_Group_Id", "dbo.UserGroups");
            DropIndex("dbo.UserPermissions", new[] { "User_Group_Id" });
            DropIndex("dbo.Users", new[] { "User_Group_Id" });
            AlterColumn("dbo.Users", "User_Group_Id", c => c.Int());
            DropColumn("dbo.UserPermissions", "User_Group_Id");
            RenameColumn(table: "dbo.Users", name: "User_Group_Id", newName: "UserGroup_User_Group_Id");
            CreateIndex("dbo.Users", "UserGroup_User_Group_Id");
            AddForeignKey("dbo.Users", "UserGroup_User_Group_Id", "dbo.UserGroups", "User_Group_Id");
        }
    }
}
