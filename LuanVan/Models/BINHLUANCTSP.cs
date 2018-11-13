namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BINHLUANCTSP")]
    public partial class BINHLUANCTSP
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã bình luận")]
        public string BL_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhân viên")]
        public string NV_ID { get; set; }

        [StringLength(30)]
        [Display(Name = "Mã sản phẩm")]
        public string SP_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã khách hàng")]
        public string KH_ID { get; set; }

        [StringLength(255)]
        [Display(Name = "Nội dung bình luận")]
        public string BL_ND { get; set; }

        [Display(Name = "Thời gian")]
        [DataType(DataType.DateTime)]
        public DateTime? BL_THOIGIAN { get; set; }

        [StringLength(20)]
        [Display(Name = "Bình luận trước")]
        public string BL_TL { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
