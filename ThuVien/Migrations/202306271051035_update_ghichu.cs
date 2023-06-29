namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_ghichu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "ghichu", c => c.String(maxLength: 2000));
            DropColumn("dbo.chitietmuon", "tinhtrang");
            DropColumn("dbo.chitietmuon", "sotienphat");
            DropColumn("dbo.phieumuon", "lydotuchoi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.phieumuon", "lydotuchoi", c => c.String(maxLength: 2000));
            AddColumn("dbo.chitietmuon", "sotienphat", c => c.Decimal(storeType: "money"));
            AddColumn("dbo.chitietmuon", "tinhtrang", c => c.String());
            DropColumn("dbo.phieumuon", "ghichu");
        }
    }
}
