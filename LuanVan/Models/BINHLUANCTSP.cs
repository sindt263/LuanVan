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
        public string BL_ID { get; set; }

        [StringLength(10)]
        public string NV_ID { get; set; }

        [StringLength(30)]
        public string SP_ID { get; set; }

        [StringLength(10)]
        public string KH_ID { get; set; }

        [StringLength(255)]
        public string BL_ND { get; set; }

        public DateTime? BL_THOIGIAN { get; set; }

        [StringLength(20)]
        public string BL_TL { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
