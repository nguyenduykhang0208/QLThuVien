namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_cf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "tuchoi", c => c.Boolean(nullable: false));
            AddColumn("dbo.phieumuon", "lydotuchoi", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.phieumuon", "lydotuchoi");
            DropColumn("dbo.phieumuon", "tuchoi");
        }
    }
}
