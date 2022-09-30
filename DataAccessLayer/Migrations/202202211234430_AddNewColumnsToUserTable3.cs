namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnsToUserTable3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Attached_Person", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "City", c => c.String(maxLength: 20));
            AddColumn("dbo.Users", "Country", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Country");
            DropColumn("dbo.Users", "City");
            DropColumn("dbo.Users", "Attached_Person");
        }
    }
}
