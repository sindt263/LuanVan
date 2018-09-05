namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            PHIEUNHAPSPs = new HashSet<PHIEUNHAPSP>();
        }

        [Key]
        [StringLength(10)]
        
        [Display(Name = "Mã nhân viên")]
        public string NV_ID { get; set; }

        
        [Display(Name = "Mã quyền")]
        public short? LNV_ID { get; set; }

        [StringLength(100)]
        
        [Display(Name = "Họ tên")]
        public string NV_TEN { get; set; }

        
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? NV_NGAYSINH { get; set; }

        [StringLength(200)]
        
        [Display(Name = "Quê quán")]
        public string NV_QUEQUAN { get; set; }

        [StringLength(200)]
        
        [Display(Name = "Đại chỉ")]
        public string NV_DIACHI { get; set; }

        [StringLength(10)]
        
        [Display(Name = "Giới tính")]
        public string NV_GIOITINH { get; set; }

        
        [Display(Name = "Ngày ký hợp đồng")]
        public DateTime? NV_NGAYKYHOPDONG { get; set; }

        
        [Display(Name = "Ngày kết thúc hợp đồng")]
        public DateTime? NV_NGAYKETTHUCHOPDONG { get; set; }

        [StringLength(100)]
        
        [Display(Name = "Email")]
        public string NV_EMAIL { get; set; }

        [StringLength(200)]
        
        [Display(Name = "Mật khẩu")]
        public string NV_MATKHAU { get; set; }

        [StringLength(16)]
        
        [Display(Name = "Tài khoản")]
        public string NV_TAIKHOAN { get; set; }

        public virtual LOAINV LOAINV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPSP> PHIEUNHAPSPs { get; set; }
    }
}
