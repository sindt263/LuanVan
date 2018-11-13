namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HINHANHSP")]
    public partial class HINHANHSP
    {
        [Key]
        [StringLength(30)]
        [Display(Name = "Mã hình ảnh")]
        public string HA_ID { get; set; }

        [StringLength(30)]
        [Display(Name = "Mã sản phẩm")]
        public string SP_ID { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Hình ảnh")]
        public byte[] HA_ND { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
