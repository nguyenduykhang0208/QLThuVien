namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_phieumuon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "sosachmat", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.phieumuon", "sosachmat");
        }
    }
}
