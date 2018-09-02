namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETBH")]
    public partial class CHITIETBH
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã số")]
        [Required]
        public string CTBH_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        [Required]
        public string SP_ID { get; set; }

        [Display(Name = "Mã bảo hành")]
        [Required]
        public short? BH_ID { get; set; }

        [Display(Name = "Trạng thái")]
        [Required]
        public short? TTBH_ID { get; set; }

        [Display(Name = "Ngày bảo hành")]
        [Required]
        public DateTime? CTBH_NGAYBH { get; set; }

        [Display(Name = "Ngày hẹn trả")]
        [Required]
        public DateTime? CTBH_NGAYTRA { get; set; }

        [StringLength(255)]
        [Display(Name = "Ghi chú")]
        [Required]
        public string CTBH_GHICHU { get; set; }

        public virtual BAOHANH BAOHANH { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

        public virtual TRANGTHAIBH TRANGTHAIBH { get; set; }
    }
}
