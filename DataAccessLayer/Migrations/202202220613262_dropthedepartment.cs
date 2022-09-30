namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropthedepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "Department_Id" });
            RenameColumn(table: "dbo.Users", name: "Department_Id", newName: "Department_Department_Id");
            AlterColumn("dbo.Users", "Department_Department_Id", c => c.Int());
            CreateIndex("dbo.Users", "Department_Department_Id");
            AddForeignKey("dbo.Users", "Department_Department_Id", "dbo.Departments", "Department_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Department_Department_Id", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "Department_Department_Id" });
            AlterColumn("dbo.Users", "Department_Department_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Users", name: "Department_Department_Id", newName: "Department_Id");
            CreateIndex("dbo.Users", "Department_Id");
            AddForeignKey("dbo.Users", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
    }
}
