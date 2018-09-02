namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETSANPHAM")]
    public partial class CHITIETSANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHITIETSANPHAM()
        {
            HINHANHSPs = new HashSet<HINHANHSP>();
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [StringLength(20)]
        [Display(Name = "Mã chi tiết")]
        [Required]
        public string CTSP_ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên")]
        [Required]
        public string CTSP_TEN { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Công nghệ màn hình")]
        public string CTSP_CNMANGHINH { get; set; }

        [StringLength(100)]
        [Display(Name = "Độ phân giải")]
        [Required]
        public string CTSP_DOPHANGIAI { get; set; }

        [Display(Name = "Kích thước màn hình")]
        [Required]
        public double? CTSP_MANHINH { get; set; }

        [StringLength(10)]
        [Required]
        [Display(Name = "Camera trước")]
        public string CTSP_CAMERATRUOC { get; set; }

        [StringLength(10)]
        [Display(Name = "Camera sau")]
        [Required]
        public string CTSP_CAMERASAU { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Hệ điều hành")]
        public string CTSP_HEDIEUHANH { get; set; }

        [Display(Name = "Bộ nhớ RAM")]
        [Required]
        public short? CTSP_RAM { get; set; }

        [Display(Name = "Bộ nhớ ROM")]
        [Required]
        public short? CTSP_ROM { get; set; }

        [Display(Name = "Dung lượng PIN")]
        [Required]
        public int? CTSP_DUNGLUONGPIN { get; set; }

        [Display(Name = "Số SIM")]
        [Required]
        public short? CTSP_SOSIM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANHSP> HINHANHSPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
