namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestCategories", "Category_Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.RequestCategories", "IsActive", c => c.Boolean());
            DropColumn("dbo.RequestCategories", "Department_Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestCategories", "Department_Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.RequestCategories", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.RequestCategories", "Category_Description");
        }
    }
}
