namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            BINHLUANCTSPs = new HashSet<BINHLUANCTSP>();
            CHITIETSANPHAMs = new HashSet<CHITIETSANPHAM>();
            HINHANHSPs = new HashSet<HINHANHSP>();
        }

        [Key]
        [StringLength(30)]
        public string SP_ID { get; set; }

        [StringLength(30)]
        public string KM_ID { get; set; }

        [StringLength(20)]
        public string GIA_ID { get; set; }

        [StringLength(10)]
        public string NSX_ID { get; set; }

        [StringLength(10)]
        public string DSP_ID { get; set; }

        [StringLength(255)]
        public string SP_TEN { get; set; }

        [StringLength(100)]
        public string SP_CNMANGHINH { get; set; }

        [StringLength(100)]
        public string SP_DOPHANGIAI { get; set; }

        [StringLength(10)]
        public string SP_MANHINH { get; set; }

        [StringLength(100)]
        public string SP_CAMERASAU { get; set; }

        [StringLength(100)]
        public string SP_CAMERATRUOC { get; set; }

        [StringLength(100)]
        public string SP_HEDIEUHANH { get; set; }

        [StringLength(100)]
        public string SP_RAM { get; set; }

        [StringLength(100)]
        public string SP_ROM { get; set; }

        [StringLength(20)]
        public string SP_DUNGLUONGPIN { get; set; }

        [StringLength(20)]
        public string SP_SOSIM { get; set; }

        [StringLength(3500)]
        public string SP_MOTA { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SP_NGAYTAO { get; set; }

        public int? SP_THOIGIANBH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUANCTSP> BINHLUANCTSPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }

        public virtual DONGSANPHAM DONGSANPHAM { get; set; }

        public virtual GIASP GIASP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANHSP> HINHANHSPs { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }

        public virtual NHASANXUAT NHASANXUAT { get; set; }
    }
}
