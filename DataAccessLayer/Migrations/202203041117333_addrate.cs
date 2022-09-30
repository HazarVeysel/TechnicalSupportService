namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Message_Image1", c => c.String());
            AddColumn("dbo.Messages", "Message_Image2", c => c.String());
            AddColumn("dbo.Messages", "Message_Image3", c => c.String());
            AddColumn("dbo.Requests", "Rate", c => c.Int());
            AddColumn("dbo.Requests", "Comment", c => c.String(maxLength: 500));
            DropColumn("dbo.Messages", "Message_Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Message_Image", c => c.String());
            DropColumn("dbo.Requests", "Comment");
            DropColumn("dbo.Requests", "Rate");
            DropColumn("dbo.Messages", "Message_Image3");
            DropColumn("dbo.Messages", "Message_Image2");
            DropColumn("dbo.Messages", "Message_Image1");
        }
    }
}
