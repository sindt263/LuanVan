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
            BINHLUANCTSPs = new HashSet<BINHLUANCTSP>();
            CHITIETBHs = new HashSet<CHITIETBH>();
            DONHANGs = new HashSet<DONHANG>();
            PHIEUNHAPSPs = new HashSet<PHIEUNHAPSP>();
        }

        [Key]
        [StringLength(10)]
        public string NV_ID { get; set; }

        public short? LNV_ID { get; set; }

        [StringLength(100)]
        public string NV_TEN { get; set; }

        public DateTime? NV_NGAYSINH { get; set; }

        [StringLength(200)]
        public string NV_QUEQUAN { get; set; }

        [StringLength(200)]
        public string NV_DIACHI { get; set; }

        [StringLength(10)]
        public string NV_GIOITINH { get; set; }

        public DateTime? NV_NGAYKYHOPDONG { get; set; }

        public DateTime? NV_NGAYKETTHUCHOPDONG { get; set; }

        [StringLength(100)]
        public string NV_EMAIL { get; set; }

        [StringLength(200)]
        public string NV_MATKHAU { get; set; }

        [StringLength(16)]
        public string NV_TAIKHOAN { get; set; }

        [StringLength(15)]
        public string NV_SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUANCTSP> BINHLUANCTSPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETBH> CHITIETBHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        public virtual LOAINV LOAINV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPSP> PHIEUNHAPSPs { get; set; }
    }
}
