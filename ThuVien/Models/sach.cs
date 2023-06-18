namespace ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sach")]
    public partial class sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sach()
        {
            chitietmuons = new HashSet<chitietmuon>();
            tacgias = new HashSet<tacgia>();
            theloais = new HashSet<theloai>();
        }

        [Key]
        public int masach { get; set; }

        [Required]
        [StringLength(200)]
        public string tensach { get; set; }

        public string anh { get; set; }

        [StringLength(50)]
        public string namxb { get; set; }

        public int manxb { get; set; }

        public int? sotrang { get; set; }

        public int soluong { get; set; }

        public string mieuta { get; set; }

        public bool TrangThai { get; set; }

        [Column(TypeName = "money")]
        public decimal giabia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietmuon> chitietmuons { get; set; }

        public virtual nhaxuatban nhaxuatban { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tacgia> tacgias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<theloai> theloais { get; set; }
    }
}
