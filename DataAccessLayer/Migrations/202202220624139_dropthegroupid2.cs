namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropthegroupid2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "User_Group_Id", "dbo.UserGroups");
            DropIndex("dbo.Users", new[] { "User_Group_Id" });
            RenameColumn(table: "dbo.Users", name: "User_Group_Id", newName: "User_Group_ID");
            AlterColumn("dbo.Users", "User_Group_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "User_Group_ID");
            AddForeignKey("dbo.Users", "User_Group_ID", "dbo.UserGroups", "User_Group_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "User_Group_ID", "dbo.UserGroups");
            DropIndex("dbo.Users", new[] { "User_Group_ID" });
            AlterColumn("dbo.Users", "User_Group_ID", c => c.Int());
            RenameColumn(table: "dbo.Users", name: "User_Group_ID", newName: "UserGroup_User_Group_Id");
            CreateIndex("dbo.Users", "UserGroup_User_Group_Id");
            AddForeignKey("dbo.Users", "UserGroup_User_Group_Id", "dbo.UserGroups", "User_Group_Id");
        }
    }
}
