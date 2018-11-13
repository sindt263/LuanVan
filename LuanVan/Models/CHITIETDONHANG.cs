namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETDONHANG")]
    public partial class CHITIETDONHANG
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã chi tiết")]
        public string CTDH_ID { get; set; }

        [Display(Name = "Mã đơn hàng")]
        public int? DN_ID { get; set; }

        [StringLength(30)]
        [Display(Name = "Mã sản phẩm")]
        public string CTSP_ID { get; set; }

        public virtual CHITIETSANPHAM CHITIETSANPHAM { get; set; }

        public virtual DONHANG DONHANG { get; set; }
    }
}
