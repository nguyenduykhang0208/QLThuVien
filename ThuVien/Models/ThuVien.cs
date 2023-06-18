using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ThuVien.Models
{
    public partial class ThuVien : DbContext
    {
        public ThuVien()
            : base("name=ThuVien")
        {
        }

        public virtual DbSet<chitietmuon> chitietmuons { get; set; }
        //public virtual DbSet<docgia> docgias { get; set; }
        public virtual DbSet<nhaxuatban> nhaxuatbans { get; set; }
        public virtual DbSet<phieumuon> phieumuons { get; set; }
        public virtual DbSet<sach> saches { get; set; }
        public virtual DbSet<tacgia> tacgias { get; set; }
        public virtual DbSet<theloai> theloais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<chitietmuon>()
                .Property(e => e.sotienphat)
                .HasPrecision(19, 4);

            //modelBuilder.Entity<docgia>()
            //    .HasMany(e => e.phieumuons)
            //    .WithRequired(e => e.docgia)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<nhaxuatban>()
                .HasMany(e => e.saches)
                .WithRequired(e => e.nhaxuatban)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<phieumuon>()
                .Property(e => e.tongtienphat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<phieumuon>()
                .HasMany(e => e.chitietmuons)
                .WithRequired(e => e.phieumuon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sach>()
                .Property(e => e.giabia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<sach>()
                .HasMany(e => e.chitietmuons)
                .WithRequired(e => e.sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sach>()
                .HasMany(e => e.tacgias)
                .WithMany(e => e.saches)
                .Map(m => m.ToTable("sachtacgia").MapLeftKey("sach_id").MapRightKey("tacgia_id"));

            modelBuilder.Entity<sach>()
                .HasMany(e => e.theloais)
                .WithMany(e => e.saches)
                .Map(m => m.ToTable("sachtheloai").MapLeftKey("sach_id").MapRightKey("theloai_id"));
        }
    }
}
