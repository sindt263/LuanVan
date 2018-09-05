namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRANGTHAIDONHANG")]
    public partial class TRANGTHAIDONHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRANGTHAIDONHANG()
        {
            DONHANGs = new HashSet<DONHANG>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        [Display(Name = "Mã số")]
        public short TTDH_ID { get; set; }

        [StringLength(20)]
        
        [Display(Name = "Tên")]
        public string TTDH_TEN { get; set; }

        [StringLength(255)]
        
        [Display(Name = "Mô tả")]
        public string TTDH_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
    }
}
