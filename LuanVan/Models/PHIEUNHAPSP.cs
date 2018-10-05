namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUNHAPSP")]
    public partial class PHIEUNHAPSP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUNHAPSP()
        {
            CHITIETNHAPs = new HashSet<CHITIETNHAP>();
        }

        [Key]
        [StringLength(10)]
        public string PN_ID { get; set; }

        [StringLength(10)]
        public string NV_ID { get; set; }

        [StringLength(10)]
        public string NCC_ID { get; set; }

        public DateTime? PN_NGAY { get; set; }

        [StringLength(500)]
        public string PN_GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNHAP> CHITIETNHAPs { get; set; }

        public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
