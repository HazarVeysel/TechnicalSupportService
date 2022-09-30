namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPriority_ID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Priority_ID", c => c.Int());
            CreateIndex("dbo.Requests", "Priority_ID");
            AddForeignKey("dbo.Requests", "Priority_ID", "dbo.Priorities", "Priority_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "Priority_ID", "dbo.Priorities");
            DropIndex("dbo.Requests", new[] { "Priority_ID" });
            DropColumn("dbo.Requests", "Priority_ID");
        }
    }
}
