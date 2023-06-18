namespace ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("phieumuon")]
    public partial class phieumuon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public phieumuon()
        {
            chitietmuons = new HashSet<chitietmuon>();
        }

        [Key]
        public int maphieumuon { get; set; }

        public DateTime ngaymuon { get; set; }

        [Column(TypeName = "money")]
        public decimal? tongtienphat { get; set; }

        public int? soluongmuon { get; set; }

        [ForeignKey("ApplicationUser")]
        [StringLength(256)]
        public string username { get; set; }

        public bool trangthaiduyet { get; set; }
        public bool trangthaitra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietmuon> chitietmuons { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
