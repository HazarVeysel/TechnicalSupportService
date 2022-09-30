namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editdatetimes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Sent_Date", c => c.DateTime());
            AlterColumn("dbo.Messages", "Receiving_Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Receiving_Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Messages", "Sent_Date", c => c.DateTime(nullable: false));
        }
    }
}
