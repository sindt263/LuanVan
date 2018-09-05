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
        [StringLength(10)]
        [Display(Name = "Mã đơn hàng")]
        
        public string DN_ID { get; set; }

        [Display(Name = "Mã trạng thái")]
        
        public short? TTDH_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã khách hàng")]
        
        public string KH_ID { get; set; }

        
        [Display(Name ="Hình thức thanh toán")]
        public short? HTTT_ID { get; set; }

        [DataType(DataType.Date)]
        
        [Display(Name = "Ngày lập đơn")]
        public DateTime? DN_NGALAPDON { get; set; }

        [StringLength(255)]
        [Display(Name = "Ghi chú")]
        public string DN_GHICHU { get; set; }

        [Display(Name = "số lượng")]
        
        public int? DN_SL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }

        public virtual HINHTHUCTHANHTOAN HINHTHUCTHANHTOAN { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual TRANGTHAIDONHANG TRANGTHAIDONHANG { get; set; }
    }
}
