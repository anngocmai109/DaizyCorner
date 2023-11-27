namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFeeForOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Province", c => c.String());
            AddColumn("dbo.tb_Order", "District", c => c.String());
            AddColumn("dbo.tb_Order", "Ward", c => c.String());
            AddColumn("dbo.tb_Order", "TransportFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "TransportFee");
            DropColumn("dbo.tb_Order", "Ward");
            DropColumn("dbo.tb_Order", "District");
            DropColumn("dbo.tb_Order", "Province");
        }
    }
}
