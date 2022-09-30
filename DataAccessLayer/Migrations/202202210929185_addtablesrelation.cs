namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablesrelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserGroup_User_Group_Id", c => c.Int());
            AddColumn("dbo.Requests", "User_Id", c => c.Int());
            AddColumn("dbo.Requests", "Department_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "User_Id");
            CreateIndex("dbo.Requests", "Department_Id");
            CreateIndex("dbo.Users", "UserGroup_User_Group_Id");
            AddForeignKey("dbo.Requests", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "User_Id", "dbo.Users", "User_Id");
            AddForeignKey("dbo.Users", "UserGroup_User_Group_Id", "dbo.UserGroups", "User_Group_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserGroup_User_Group_Id", "dbo.UserGroups");
            DropForeignKey("dbo.Requests", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Requests", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "UserGroup_User_Group_Id" });
            DropIndex("dbo.Requests", new[] { "Department_Id" });
            DropIndex("dbo.Requests", new[] { "User_Id" });
            DropColumn("dbo.Requests", "Department_Id");
            DropColumn("dbo.Requests", "User_Id");
            DropColumn("dbo.Users", "UserGroup_User_Group_Id");
        }
    }
}
