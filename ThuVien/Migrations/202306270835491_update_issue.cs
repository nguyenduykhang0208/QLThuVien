namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_issue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieumuon", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.phieumuon", "UserName");
        }
    }
}
