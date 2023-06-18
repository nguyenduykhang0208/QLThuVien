namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.chitietmuon",
                c => new
                    {
                        maphieumuon = c.Int(nullable: false),
                        masach = c.Int(nullable: false),
                        ngaymuon = c.DateTime(nullable: false),
                        ngaytra = c.DateTime(),
                        songaytre = c.Int(),
                        tinhtrang = c.String(),
                        sotienphat = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => new { t.maphieumuon, t.masach })
                .ForeignKey("dbo.phieumuon", t => t.maphieumuon, cascadeDelete: true)
                .ForeignKey("dbo.sach", t => t.masach, cascadeDelete: true)
                .Index(t => t.maphieumuon)
                .Index(t => t.masach);
            
            CreateTable(
                "dbo.phieumuon",
                c => new
                    {
                        maphieumuon = c.Int(nullable: false, identity: true),
                        ngaymuon = c.DateTime(nullable: false),
                        tongtienphat = c.Decimal(storeType: "money"),
                        soluongmuon = c.Int(),
                        username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.maphieumuon)
                .ForeignKey("dbo.AspNetUsers", t => t.username)
                .Index(t => t.username);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Phone = c.String(),
                        gioitinh = c.String(),
                        diachi = c.String(),
                        matsach = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.sach",
                c => new
                    {
                        masach = c.Int(nullable: false, identity: true),
                        tensach = c.String(nullable: false, maxLength: 200),
                        anh = c.String(),
                        namxb = c.String(maxLength: 50),
                        manxb = c.Int(nullable: false),
                        sotrang = c.Int(),
                        soluong = c.Int(nullable: false),
                        mieuta = c.String(),
                        TrangThai = c.Boolean(nullable: false),
                        giabia = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.masach)
                .ForeignKey("dbo.nhaxuatban", t => t.manxb, cascadeDelete: true)
                .Index(t => t.manxb);
            
            CreateTable(
                "dbo.nhaxuatban",
                c => new
                    {
                        manxb = c.Int(nullable: false, identity: true),
                        tennxb = c.String(maxLength: 100),
                        diachinxb = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.manxb);
            
            CreateTable(
                "dbo.tacgia",
                c => new
                    {
                        matacgia = c.Int(nullable: false, identity: true),
                        tentacgia = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.matacgia);
            
            CreateTable(
                "dbo.theloai",
                c => new
                    {
                        matheloai = c.Int(nullable: false, identity: true),
                        tentheloai = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.matheloai);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.tacgiasaches",
                c => new
                    {
                        tacgia_matacgia = c.Int(nullable: false),
                        sach_masach = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.tacgia_matacgia, t.sach_masach })
                .ForeignKey("dbo.tacgia", t => t.tacgia_matacgia, cascadeDelete: true)
                .ForeignKey("dbo.sach", t => t.sach_masach, cascadeDelete: true)
                .Index(t => t.tacgia_matacgia)
                .Index(t => t.sach_masach);
            
            CreateTable(
                "dbo.theloaisaches",
                c => new
                    {
                        theloai_matheloai = c.Int(nullable: false),
                        sach_masach = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.theloai_matheloai, t.sach_masach })
                .ForeignKey("dbo.theloai", t => t.theloai_matheloai, cascadeDelete: true)
                .ForeignKey("dbo.sach", t => t.sach_masach, cascadeDelete: true)
                .Index(t => t.theloai_matheloai)
                .Index(t => t.sach_masach);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.theloaisaches", "sach_masach", "dbo.sach");
            DropForeignKey("dbo.theloaisaches", "theloai_matheloai", "dbo.theloai");
            DropForeignKey("dbo.tacgiasaches", "sach_masach", "dbo.sach");
            DropForeignKey("dbo.tacgiasaches", "tacgia_matacgia", "dbo.tacgia");
            DropForeignKey("dbo.sach", "manxb", "dbo.nhaxuatban");
            DropForeignKey("dbo.chitietmuon", "masach", "dbo.sach");
            DropForeignKey("dbo.chitietmuon", "maphieumuon", "dbo.phieumuon");
            DropForeignKey("dbo.phieumuon", "username", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.theloaisaches", new[] { "sach_masach" });
            DropIndex("dbo.theloaisaches", new[] { "theloai_matheloai" });
            DropIndex("dbo.tacgiasaches", new[] { "sach_masach" });
            DropIndex("dbo.tacgiasaches", new[] { "tacgia_matacgia" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.sach", new[] { "manxb" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.phieumuon", new[] { "username" });
            DropIndex("dbo.chitietmuon", new[] { "masach" });
            DropIndex("dbo.chitietmuon", new[] { "maphieumuon" });
            DropTable("dbo.theloaisaches");
            DropTable("dbo.tacgiasaches");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.theloai");
            DropTable("dbo.tacgia");
            DropTable("dbo.nhaxuatban");
            DropTable("dbo.sach");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.phieumuon");
            DropTable("dbo.chitietmuon");
        }
    }
}
