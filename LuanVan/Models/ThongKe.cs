using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Models
{
    public class ThongKe
    {
        public string SP_ID { set; get; }
        public string TenSP { set; get; }
        public string KH_ID { set; get; }
        public string KH_TEN { set; get; }
        public int TongTien { set; get; }
        public int? SL { set; get; }
    }
}