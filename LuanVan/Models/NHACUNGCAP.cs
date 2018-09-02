﻿namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHACUNGCAP")]
    public partial class NHACUNGCAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHACUNGCAP()
        {
            PHIEUNHAPSPs = new HashSet<PHIEUNHAPSP>();
        }

        [Key]
        [StringLength(10)]
        [Required]
        [Display(Name = "Mã số")]
        public string NCC_ID { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Tên")]
        public string NCC_TEN { get; set; }

        [StringLength(15)]
        [Required]
        [Display(Name = "Số điện thoại")]
        public string NCC_SDT { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Địa chỉ")]
        public string NCC_DIACHI { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Ghi chú")]
        public string NCC_GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPSP> PHIEUNHAPSPs { get; set; }
    }
}
