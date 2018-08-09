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
        public string CTBH_ID { get; set; }

        [StringLength(20)]
        public string SP_ID { get; set; }

        public short? BH_ID { get; set; }

        public short? TTBH_ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CTBH_NGAYBH { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CTBH_NGAYTRA { get; set; }

        [StringLength(255)]
        public string CTBH_GHICHU { get; set; }

        public virtual BAOHANH BAOHANH { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

        public virtual TRANGTHAIBH TRANGTHAIBH { get; set; }
    }
}
