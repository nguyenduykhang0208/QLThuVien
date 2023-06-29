﻿namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_b : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.phieumuon", "songaytre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.phieumuon", "songaytre", c => c.Int());
            DropColumn("dbo.phieumuon", "sosachmat");
        }
    }
}
