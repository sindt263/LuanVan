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
        public string CTSP_ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên")]
        
        public string CTSP_TEN { get; set; }

        [StringLength(100)]
        
        [Display(Name = "Công nghệ màn hình")]
        public string CTSP_CNMANGHINH { get; set; }

        [StringLength(100)]
        [Display(Name = "Độ phân giải")]
        
        public string CTSP_DOPHANGIAI { get; set; }

        [Display(Name = "Kích thước màn hình")]
        
        public double? CTSP_MANHINH { get; set; }

        [StringLength(50)]
        
        [Display(Name = "Camera trước")]
        public string CTSP_CAMERATRUOC { get; set; }

        [StringLength(50)]
        [Display(Name = "Camera sau")]        
        public string CTSP_CAMERASAU { get; set; }

        [StringLength(100)]        
        [Display(Name = "Hệ điều hành")]
        public string CTSP_HEDIEUHANH { get; set; }

        [StringLength(50)]
        [Display(Name = "Bộ nhớ RAM")]        
        public string CTSP_RAM { get; set; }

        [Display(Name = "Bộ nhớ ROM")]
        [StringLength(50)]
        public string CTSP_ROM { get; set; }

        [Display(Name = "Dung lượng PIN")]
        public int? CTSP_DUNGLUONGPIN { get; set; }

        [Display(Name = "Số SIM")]
        [StringLength(50)]
        public string CTSP_SOSIM { get; set; }

        [StringLength(2555)]
        [Display(Name = "Mô tả chi tiết")]
        public string CTSP_MOTA { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime? CTSP_NGAYTAO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANHSP> HINHANHSPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
