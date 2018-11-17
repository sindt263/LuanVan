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
            BINHLUANCTSPs = new HashSet<BINHLUANCTSP>();
            CHITIETSANPHAMs = new HashSet<CHITIETSANPHAM>();
            HINHANHSPs = new HashSet<HINHANHSP>();
        }

        [Key]
        [StringLength(30)]
        [Display(Name = "Mã sản phẩm")]
        public string SP_ID { get; set; }

        [StringLength(30)]
        [Display(Name = "Mã khuyến mãi")]
        public string KM_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã giá")]
        public string GIA_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhà sản xuất")]
        public string NSX_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã dòng sản phẩm")]
        public string DSP_ID { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên sản phẩm")]
        public string SP_TEN { get; set; }

        [StringLength(100)]
        [Display(Name = "Công nghệ màn hình")]
        public string SP_CNMANGHINH { get; set; }

        [StringLength(100)]
        [Display(Name = "Độ phân giải")]
        public string SP_DOPHANGIAI { get; set; }

        [StringLength(10)]
        [Display(Name = "Màn hình")]
        public string SP_MANHINH { get; set; }

        [StringLength(100)]
        [Display(Name = "Camera sau")]
        public string SP_CAMERASAU { get; set; }

        [StringLength(100)]
        [Display(Name = "Camera trước")]
        public string SP_CAMERATRUOC { get; set; }

        [StringLength(100)]
        [Display(Name = "Hệ điều hành")]
        public string SP_HEDIEUHANH { get; set; }

        [StringLength(100)]
        [Display(Name = "RAM")]
        public string SP_RAM { get; set; }

        [StringLength(100)]
        [Display(Name = "ROM")]
        public string SP_ROM { get; set; }

        [StringLength(20)]
        [Display(Name = "Dung lượng PIN")]
        public string SP_DUNGLUONGPIN { get; set; }

        [StringLength(20)]
        [Display(Name = "Số SIM")]
        public string SP_SOSIM { get; set; }

        [StringLength(3500)]
        [Display(Name = "Mô tả")]
        public string SP_MOTA { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime? SP_NGAYTAO { get; set; }

        [Display(Name = "Thời gian bảo hành")]
        public int? SP_THOIGIANBH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUANCTSP> BINHLUANCTSPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }

        public virtual DONGSANPHAM DONGSANPHAM { get; set; }

        public virtual GIASP GIASP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANHSP> HINHANHSPs { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }

        public virtual NHASANXUAT NHASANXUAT { get; set; }
    }
}
