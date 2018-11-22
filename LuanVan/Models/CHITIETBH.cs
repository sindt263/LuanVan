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
        [Display(Name = "Mã bảo hành")]
        public string CTBH_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã khách hàng")]
        public string KH_ID { get; set; }

        [Display(Name = "Loại bảo hành")]
        public short? BH_ID { get; set; }

        [StringLength(30)]
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage ="Mã sản phẩm là bắt buộc.")]
        public string CTSP_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhân viên")]
        public string NV_ID { get; set; }

        [Display(Name = "Mã trạng thái")]
        public short? TTBH_ID { get; set; }

        [Display(Name = "Ngày bảo hành")]
        [DataType(DataType.Date)]
        public DateTime? CTBH_NGAYBH { get; set; }

        [Display(Name = "Ngày trả")]
        [DataType(DataType.Date)]
        public DateTime? CTBH_NGAYTRA { get; set; }

        [StringLength(255)]
        [Display(Name = "Ghi chú")]
        public string CTBH_GHICHU { get; set; }

        public virtual BAOHANH BAOHANH { get; set; }

        public virtual CHITIETSANPHAM CHITIETSANPHAM { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual TRANGTHAIBH TRANGTHAIBH { get; set; }
    }
}
