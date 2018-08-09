namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHUYENMAI")]
    public partial class KHUYENMAI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHUYENMAI()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [StringLength(10)]
        public string KM_ID { get; set; }

        [StringLength(100)]
        public string KM_TEN { get; set; }

        [DataType(DataType.Date)]
        public DateTime? KM_NGAYBATDAU { get; set; }

        public int? KM_THOIGIAN { get; set; }

        [DataType(DataType.Date)]
        public DateTime? KM_NGAYKETTHUC { get; set; }

        [StringLength(500)]
        public string KM_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
