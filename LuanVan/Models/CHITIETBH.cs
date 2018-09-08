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
        
        public string CTBH_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        
        public string SP_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhân viên")]

        public string NV_ID { get; set; }

        [Display(Name = "Mã bảo hành")]
        
        public short? BH_ID { get; set; }

        [Display(Name = "Trạng thái")]
        
        public short? TTBH_ID { get; set; }

        [Display(Name = "Ngày bảo hành")]
        
        public DateTime? CTBH_NGAYBH { get; set; }

        [Display(Name = "Ngày hẹn trả")]
        
        public DateTime? CTBH_NGAYTRA { get; set; }

        [StringLength(255)]
        [Display(Name = "Ghi chú")]
        
        public string CTBH_GHICHU { get; set; }

        public virtual BAOHANH BAOHANH { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

        public virtual TRANGTHAIBH TRANGTHAIBH { get; set; }
    }
}
