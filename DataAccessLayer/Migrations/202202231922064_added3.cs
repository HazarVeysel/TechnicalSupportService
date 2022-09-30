namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Requests", new[] { "Department_Id" });
            AlterColumn("dbo.Requests", "End_Date", c => c.DateTime());
            AlterColumn("dbo.Requests", "Department_Id", c => c.Int());
            CreateIndex("dbo.Requests", "Department_Id");
            AddForeignKey("dbo.Requests", "Department_Id", "dbo.Departments", "Department_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Requests", new[] { "Department_Id" });
            AlterColumn("dbo.Requests", "Department_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "End_Date", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Requests", "Department_Id");
            AddForeignKey("dbo.Requests", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
    }
}
