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
            BINHLUANCTSPs = new HashSet<BINHLUANCTSP>();
            DONHANGs = new HashSet<DONHANG>();
        }

        [Key]
        [StringLength(10)]
        public string KH_ID { get; set; }

        [StringLength(100)]
        public string KH_TEN { get; set; }

        [StringLength(100)]
        public string KH_EMAIL { get; set; }

        [StringLength(15)]
        public string KH_SDT { get; set; }

        [StringLength(200)]
        public string KH_DIACHI { get; set; }

        public DateTime? KH_NGAYSINH { get; set; }

        [StringLength(10)]
        public string KH_GIOITINH { get; set; }

        [StringLength(16)]
        public string KH_TAIKHOAN { get; set; }

        [StringLength(200)]
        public string KH_MATKHAU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUANCTSP> BINHLUANCTSPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
    }
}
