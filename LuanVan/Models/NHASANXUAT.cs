namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHASANXUAT")]
    public partial class NHASANXUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHASANXUAT()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [StringLength(10)]
        [Required]
        [Display(Name = "Mã số")]
        public string NSX_ID { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Tên")]
        public string NSX_TEN { get; set; }

        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string NSX_SDT { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Quốc gia")]
        public string NSX_QUOCGIA { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Ghi chú")]
        public string NSX_GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
