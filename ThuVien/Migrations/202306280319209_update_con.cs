namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_con : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.chitietmuon", "soluongtra", c => c.Int(nullable: false));
            AlterColumn("dbo.phieumuon", "ngayhethan", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.phieumuon", "ngayhethan", c => c.DateTime(nullable: false));
            DropColumn("dbo.chitietmuon", "soluongtra");
        }
    }
}
