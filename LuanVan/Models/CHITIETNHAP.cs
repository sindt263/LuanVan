namespace LuanVan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETNHAP")]
    public partial class CHITIETNHAP
    {
        [Key]
        [StringLength(20)]
        public string CTN_ID { get; set; }

        [StringLength(20)]
        public string PN_ID { get; set; }

        [StringLength(30)]
        public string CTSP_ID { get; set; }

        public int? CTN_GIA { get; set; }

        public virtual CHITIETSANPHAM CHITIETSANPHAM { get; set; }

        public virtual PHIEUNHAPSP PHIEUNHAPSP { get; set; }
    }
}
