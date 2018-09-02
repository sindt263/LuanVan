namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            DONHANGs = new HashSet<DONHANG>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Mã khách hàng")]
        public string KH_ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên khách hàng")]
        public string KH_TEN { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        //[Required]
        public string KH_EMAIL { get; set; }

        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        [Required]
        public string KH_SDT { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        [Required]
        public string KH_DIACHI { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        [Required]
        public DateTime? KH_NGAYSINH { get; set; }

        [StringLength(10)]
        [Display(Name = "Giới tính")]
        [Required]
        public string KH_GIOITINH { get; set; }

        [StringLength(16)]
        [Display(Name = "Tài khoản")]
        //[Required]
        public string KH_TAIKHOAN { get; set; }

        [StringLength(200)]
        [Display(Name = "Mật khẩu")]
        //[Required]
        public string KH_MATKHAU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
    }
}
