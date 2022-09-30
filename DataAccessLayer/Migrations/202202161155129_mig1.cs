namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "User_Id", c => c.Int());
            AddColumn("dbo.Messages", "User_Name", c => c.String());
            AddColumn("dbo.Messages", "User_Surname", c => c.String());
            AddColumn("dbo.Messages", "Department_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "Department_Name", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Department_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Department_Name", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "User_Id");
            CreateIndex("dbo.Messages", "Department_Id");
            CreateIndex("dbo.Users", "Department_Id");
            AddForeignKey("dbo.Messages", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "User_Id", "dbo.Users", "User_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Messages", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "Department_Id" });
            DropIndex("dbo.Messages", new[] { "Department_Id" });
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropColumn("dbo.Users", "Department_Name");
            DropColumn("dbo.Users", "Department_Id");
            DropColumn("dbo.Messages", "Department_Name");
            DropColumn("dbo.Messages", "Department_Id");
            DropColumn("dbo.Messages", "User_Surname");
            DropColumn("dbo.Messages", "User_Name");
            DropColumn("dbo.Messages", "User_Id");
        }
    }
}
