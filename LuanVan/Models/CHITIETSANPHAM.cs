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
        public string CTSP_ID { get; set; }

        [StringLength(100)]
        public string CTSP_TEN { get; set; }

        [StringLength(100)]
        public string CTSP_CNMANGHINH { get; set; }

        [StringLength(100)]
        public string CTSP_DOPHANGIAI { get; set; }

        public double? CTSP_MANHINH { get; set; }

        [StringLength(10)]
        public string CTSP_CAMERATRUOC { get; set; }

        [StringLength(10)]
        public string CTSP_CAMERASAU { get; set; }

        [StringLength(100)]
        public string CTSP_HEDIEUHANH { get; set; }

        public short? CTSP_RAM { get; set; }

        public short? CTSP_ROM { get; set; }

        public int? CTSP_DUNGLUONGPIN { get; set; }

        public short? CTSP_SOSIM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANHSP> HINHANHSPs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
