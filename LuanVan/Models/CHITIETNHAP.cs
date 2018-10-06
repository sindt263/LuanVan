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
        [Display(Name = "Mã chi tiết")]
        public string CTN_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã phiếu nhập")]
        public string PN_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string SP_ID { get; set; }

        [Display(Name = "Giá")]
        public int? CTN_GIA { get; set; }

        public virtual PHIEUNHAPSP PHIEUNHAPSP { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
