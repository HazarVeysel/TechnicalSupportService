namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimage5tomessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Message_Image4", c => c.String());
            AddColumn("dbo.Messages", "Message_Image5", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Message_Image5");
            DropColumn("dbo.Messages", "Message_Image4");
        }
    }
}
