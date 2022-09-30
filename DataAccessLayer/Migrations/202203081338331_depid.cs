namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class depid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "Department_Id" });
            AlterColumn("dbo.Users", "Department_Id", c => c.Int());
            CreateIndex("dbo.Users", "Department_Id");
            AddForeignKey("dbo.Users", "Department_Id", "dbo.Departments", "Department_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "Department_Id" });
            AlterColumn("dbo.Users", "Department_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "Department_Id");
            AddForeignKey("dbo.Users", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
    }
}
