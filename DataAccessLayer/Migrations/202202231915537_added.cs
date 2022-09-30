namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "Request_Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "Request_Status", c => c.Boolean(nullable: false));
        }
    }
}
