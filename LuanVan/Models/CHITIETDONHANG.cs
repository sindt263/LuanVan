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
        public string CTDH_ID { get; set; }

        [StringLength(10)]
        public string DN_ID { get; set; }

        [StringLength(20)]
        public string SP_ID { get; set; }

        [StringLength(200)]
        public string CTDH_DIACHIGIAO { get; set; }

        public virtual DONHANG DONHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
