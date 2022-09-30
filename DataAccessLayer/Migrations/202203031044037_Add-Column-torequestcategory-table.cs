namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumntorequestcategorytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestCategories", "Department_Description", c => c.String(maxLength: 250));
            AddColumn("dbo.RequestCategories", "Create_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestCategories", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestCategories", "IsActive");
            DropColumn("dbo.RequestCategories", "Create_Date");
            DropColumn("dbo.RequestCategories", "Department_Description");
        }
    }
}
