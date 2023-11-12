namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletecreatedDate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "createdDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "createdDate", c => c.DateTime(nullable: false));
        }
    }
}
