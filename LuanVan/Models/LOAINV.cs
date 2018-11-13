namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOAINV")]
    public partial class LOAINV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAINV()
        {
            NHANVIENs = new HashSet<NHANVIEN>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã loại nhân viên")]
        public short LNV_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Tên loại nhân viên")]
        public string LNV_TEN { get; set; }

        [StringLength(200)]
        [Display(Name = "Mô tả quyền")]
        public string LNV_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANVIEN> NHANVIENs { get; set; }
    }
}
