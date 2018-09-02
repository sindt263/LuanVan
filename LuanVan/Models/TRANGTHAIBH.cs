namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRANGTHAIBH")]
    public partial class TRANGTHAIBH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRANGTHAIBH()
        {
            CHITIETBHs = new HashSet<CHITIETBH>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Mã số")]
        public short TTBH_ID { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Tên")]
        public string TTBH_TEN { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Mô tả")]
        public string TTBH_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETBH> CHITIETBHs { get; set; }
    }
}
