namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletePriority : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "Request_Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "Request_Priority", c => c.Int(nullable: false));
        }
    }
}
