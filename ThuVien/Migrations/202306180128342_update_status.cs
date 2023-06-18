namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "TrangThai", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.phieumuon", "TrangThai");
        }
    }
}
