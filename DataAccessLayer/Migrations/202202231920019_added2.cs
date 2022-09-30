namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "Request_Undertaken_Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "Request_Undertaken_Date", c => c.DateTime(nullable: false));
        }
    }
}
