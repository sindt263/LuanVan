namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HINHANHKM")]
    public partial class HINHANHKM
    {
        [Key]
        [StringLength(20)]
        public string HAKM_ID { get; set; }

        [StringLength(30)]
        public string KM_ID { get; set; }

        [Column(TypeName = "image")]
        public byte[] HAKM_ND { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }
    }
}
