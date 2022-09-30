namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editundertaken : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "Request_Undertaken", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "Request_Undertaken", c => c.Boolean(nullable: false));
        }
    }
}
