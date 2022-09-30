namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUndertaker_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Request_Undertaker_Id", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Request_Undertaker_Id");
        }
    }
}
