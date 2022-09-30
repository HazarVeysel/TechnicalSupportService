namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnToDepartmentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "Department_Director", c => c.String(maxLength: 100));
            AddColumn("dbo.Departments", "Is_Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "Is_Active");
            DropColumn("dbo.Departments", "Department_Director");
        }
    }
}
