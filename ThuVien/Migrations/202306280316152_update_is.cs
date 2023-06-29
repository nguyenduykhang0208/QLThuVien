namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_is : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "ngayhethan", c => c.DateTime(nullable: false));
            DropColumn("dbo.chitietmuon", "ngaymuon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.chitietmuon", "ngaymuon", c => c.DateTime());
            DropColumn("dbo.phieumuon", "ngayhethan");
        }
    }
}
