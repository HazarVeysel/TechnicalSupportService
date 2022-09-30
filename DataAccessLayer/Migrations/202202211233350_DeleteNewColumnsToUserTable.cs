namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteNewColumnsToUserTable : DbMigration
    {
        public override void Up()
        {
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Country", c => c.String(maxLength: 20));
            AddColumn("dbo.Users", "City", c => c.String(maxLength: 20));
            AddColumn("dbo.Users", "Attached_Person", c => c.String(maxLength: 50));
        }
    }
}
