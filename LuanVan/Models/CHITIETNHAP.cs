namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETNHAP")]
    public partial class CHITIETNHAP
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã số")]
        [Required]
        public string CTN_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã phiếu nhập")]
        [Required]
        public string PN_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        [Required]
        public string SP_ID { get; set; }

        [Display(Name = "Giá nhập")]
        [Required]
        public int? CTN_GIA { get; set; }

        public virtual PHIEUNHAPSP PHIEUNHAPSP { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
