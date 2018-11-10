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
            HINHANHKMs = new HashSet<HINHANHKM>();
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [StringLength(30)]
        public string KM_ID { get; set; }

        [StringLength(100)]
        public string KM_TEN { get; set; }

        public DateTime? KM_NGAYBATDAU { get; set; }

        public float? KM_GIATRI { get; set; }

        public DateTime? KM_NGAYKETTHUC { get; set; }

        [StringLength(500)]
        public string KM_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANHKM> HINHANHKMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
