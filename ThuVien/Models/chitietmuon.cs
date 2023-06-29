namespace ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chitietmuon")]
    public partial class chitietmuon
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maphieumuon { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int masach { get; set; }

        public int soluong { get; set; }

        //public int sosachmat { get; set; }


        public virtual phieumuon phieumuon { get; set; }

        public virtual sach sach { get; set; }
    }
}