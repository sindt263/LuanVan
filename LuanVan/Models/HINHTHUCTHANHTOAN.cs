namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HINHTHUCTHANHTOAN")]
    public partial class HINHTHUCTHANHTOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HINHTHUCTHANHTOAN()
        {
            DONHANGs = new HashSet<DONHANG>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã hình thức thanh toán")]
        public short HTTT_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Tên HTTT")]
        public string HTTT_TEN { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string HTTT_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
    }
}
