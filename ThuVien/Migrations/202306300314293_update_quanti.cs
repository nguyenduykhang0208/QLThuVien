namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_quanti : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.chitietmuon", "sotralai", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.chitietmuon", "sotralai");
        }
    }
}
