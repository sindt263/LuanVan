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
        [Display(Name = "Mã phiếu")]
        public string PN_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhân viên")]
        public string NV_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhà cung cấp")]
        public string NCC_ID { get; set; }

        [Display(Name = "Ngày nhập")]
        public DateTime? PN_NGAY { get; set; }

        [StringLength(500)]
        [Display(Name = "Ghi chú")]
        public string PN_GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETNHAP> CHITIETNHAPs { get; set; }

        public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
