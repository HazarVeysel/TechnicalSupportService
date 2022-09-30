namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userphoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "User_Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "User_Photo");
        }
    }
}
