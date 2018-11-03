namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BAOHANH")]
    public partial class BAOHANH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BAOHANH()
        {
            CHITIETBHs = new HashSet<CHITIETBH>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Mã bảo hành")]

        public short BH_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Tên bảo hành")]
        public string BH_TEN { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string BH_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETBH> CHITIETBHs { get; set; }
    }
}
