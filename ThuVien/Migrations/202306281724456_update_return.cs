namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_return : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "ngaytra", c => c.DateTime());
            AddColumn("dbo.phieumuon", "songaytre", c => c.Int());
            DropColumn("dbo.chitietmuon", "soluongtra");
            DropColumn("dbo.chitietmuon", "ngaytra");
            DropColumn("dbo.chitietmuon", "songaytre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.chitietmuon", "songaytre", c => c.Int());
            AddColumn("dbo.chitietmuon", "ngaytra", c => c.DateTime());
            AddColumn("dbo.chitietmuon", "soluongtra", c => c.Int(nullable: false));
            DropColumn("dbo.phieumuon", "songaytre");
            DropColumn("dbo.phieumuon", "ngaytra");
        }
    }
}
