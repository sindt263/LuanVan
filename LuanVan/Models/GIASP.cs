namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIASP")]
    public partial class GIASP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIASP()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Mã giá")]
        
        public string GIA_ID { get; set; }

        [Display(Name = "Giá")]
        
        public int? GIA_GIA { get; set; }

        [Display(Name = "Ngày cập nhật")]
        
        public DateTime? GIA_NGAYCAPNHAT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
