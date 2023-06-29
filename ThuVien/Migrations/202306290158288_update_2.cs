namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.sach", "sotrang", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.sach", "sotrang", c => c.Int());
        }
    }
}
