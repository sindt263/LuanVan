namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HINHANHKM")]
    public partial class HINHANHKM
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã hình ảnh")]
        public string HAKM_ID { get; set; }

        [StringLength(30)]
        [Display(Name = "Mã khuyến mãi")]
        public string KM_ID { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Hình ảnh")]
        public byte[] HAKM_ND { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }
    }
}
