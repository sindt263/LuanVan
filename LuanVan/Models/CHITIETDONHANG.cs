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
        [StringLength(10)]
        [Display(Name = "Mã đơn hàng")]
        public string CTDH_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã đơn hàng")]
        public string DN_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string SP_ID { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa chỉ giao")]
        public string CTDH_DIACHIGIAO { get; set; }

         [StringLength(20)]
        [Display(Name = "Mã thẻ")]
        public string CTDH_MATHE { get; set; }

         [StringLength(50)]
        [Display(Name = "Chủ thẻ")]
        public string CTDH_CHUTHE { get; set; }

        [StringLength(10)]
        [Display(Name = "Ngày cấp")]
        public string CTDH_NGAYCAP { get; set; }

        public virtual DONHANG DONHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
