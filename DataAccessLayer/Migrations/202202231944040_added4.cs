namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Messages", new[] { "Department_Id" });
            AlterColumn("dbo.Messages", "Department_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Department_Id");
            AddForeignKey("dbo.Messages", "Department_Id", "dbo.Departments", "Department_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Messages", new[] { "Department_Id" });
            AlterColumn("dbo.Messages", "Department_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "Department_Id");
            AddForeignKey("dbo.Messages", "Department_Id", "dbo.Departments", "Department_Id", cascadeDelete: true);
        }
    }
}
