using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Models
{
    public class CartItem
    {
        public string HinhID { get; set; }
       
        public string SanPhamID { get; set; }
        public string SP_ID { get; set; }
        public string TenSanPham { get; set; }
        public int DonGia { get; set; }
        public float DonGiaKM { get; set; }
        public int SoLuong { get; set; }
        
        public float ThanhTien
        {
            get
            {
                return SoLuong * DonGiaKM;
            }
        }

    }
}