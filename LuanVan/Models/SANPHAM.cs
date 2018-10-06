namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CHITIETBHs = new HashSet<CHITIETBH>();
            CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
            CHITIETNHAPs = new HashSet<CHITIETNHAP>();
        }

        [Key]
        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string SP_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã chi tiết")]
        public string CTSP_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã khuyến mãi")]
        public string KM_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã giá")]
        public string GIA_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã dòng")]
        public string DSP_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhà sản xuất")]
        public string NSX_ID { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên")]
        public string SP_TEN { get; set; }

        [Display(Name = "Trạng thái")]
        public short? SP_TRANGTHAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETBH> CHITIETBHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNHAP> CHITIETNHAPs { get; set; }

        public virtual CHITIETSANPHAM CHITIETSANPHAM { get; set; }

        public virtual DONGSANPHAM DONGSANPHAM { get; set; }

        public virtual GIASP GIASP { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }

        public virtual NHASANXUAT NHASANXUAT { get; set; }
    }
}
