namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "trangthaiduyet", c => c.Boolean(nullable: false));
            AddColumn("dbo.phieumuon", "trangthaitra", c => c.Boolean(nullable: false));
            DropColumn("dbo.phieumuon", "TrangThai");
        }
        
        public override void Down()
        {
            AddColumn("dbo.phieumuon", "TrangThai", c => c.Boolean(nullable: false));
            DropColumn("dbo.phieumuon", "trangthaitra");
            DropColumn("dbo.phieumuon", "trangthaiduyet");
        }
    }
}
