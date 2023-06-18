namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "masv", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "masv");
        }
    }
}
