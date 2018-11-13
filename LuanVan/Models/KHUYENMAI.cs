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
        [Display(Name = "Mã khuyến mãi")]
        public string KM_ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên khuyến mãi")]
        public string KM_TEN { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        public DateTime? KM_NGAYBATDAU { get; set; }
        [Display(Name = "Giá trị")]
        public float? KM_GIATRI { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        public DateTime? KM_NGAYKETTHUC { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả khuyến mãi")]
        public string KM_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANHKM> HINHANHKMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
