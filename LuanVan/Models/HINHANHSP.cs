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
        [StringLength(10)]
        [Display(Name = "Mã hình ảnh")]
        
        public string HA_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã chi tiết sản phẩm")]
        
        public string CTSP_ID { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Nội dung")]
        
        public byte[] HA_ND { get; set; }

        public virtual CHITIETSANPHAM CHITIETSANPHAM { get; set; }
    }
}
