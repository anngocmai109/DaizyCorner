namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatecreatedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "createdDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "createdDate");
        }
    }
}
