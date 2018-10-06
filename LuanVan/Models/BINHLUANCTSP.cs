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

        [StringLength(20)]
        [Display(Name = "Mã chi tiết")]
        public string CTSP_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã nhân viên")]
        public string NV_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã khách hàng")]
        public string KH_ID { get; set; }

        [StringLength(255)]
        [Display(Name = "Nội dung")]
        public string BL_ND { get; set; }

        [StringLength(20)]
        [Display(Name = "Câu hỏi trước")]
        public string BL_TL { get; set; }

        [Display(Name = "Thời gian")]
        [DataType(DataType.Date)]
        public DateTime? BL_THOIGIAN { get; set; }

        public virtual CHITIETSANPHAM CHITIETSANPHAM { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
