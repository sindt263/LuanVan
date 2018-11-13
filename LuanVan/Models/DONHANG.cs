namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DONHANG")]
    public partial class DONHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONHANG()
        {
            CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã đơn hàng")]
        public int DN_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhân viên")]
        public string NV_ID { get; set; }

        [Display(Name = "Mã trạng thái")]
        public short? TTDH_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã khách hàng")]
        public string KH_ID { get; set; }

        [Display(Name = "Mã hình thức thanh toán")]
        public short? HTTT_ID { get; set; }

        [Display(Name = "Ngày lập đơn")]
        [DataType(DataType.Date)]
        public DateTime? DN_NGALAPDON { get; set; }

        [StringLength(255)]
        public string DN_GHICHU { get; set; }

        [StringLength(20)]
        public string DN_SDT { get; set; }

        [StringLength(200)]
        public string DN_DIACHI { get; set; }

        [StringLength(20)]
        public string DN_MATHE { get; set; }

        [StringLength(50)]
        public string DN_CHUTHE { get; set; }

        [StringLength(10)]
        public string DN_NGAYCAP { get; set; }

        [StringLength(100)]
        public string DN_EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }

        public virtual HINHTHUCTHANHTOAN HINHTHUCTHANHTOAN { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual TRANGTHAIDONHANG TRANGTHAIDONHANG { get; set; }
    }
}
