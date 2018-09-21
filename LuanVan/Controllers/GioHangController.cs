using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;

namespace LuanVan.Controllers
{
    public class GioHangController : Controller
    {
        DataContext db = new DataContext();
        // GET: GioHang
        public ActionResult Index()
        {
            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN");
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);

        }

        public RedirectToRouteResult ThemVaoGio(string SanPhamID)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                CHITIETSANPHAMsController cHITIETSANPH = new CHITIETSANPHAMsController();
                SANPHAM sp = db.SANPHAMs.Find(SanPhamID);  // tim sp theo sanPhamID
                string GiaID = sp.GIA_ID;
                string KM_ID = db.Database.SqlQuery<string>("select KM_ID from SanPham where SP_ID ='" + sp.CTSP_ID + "'").Take(1).SingleOrDefault();
                float KM_GIATRI = db.Database.SqlQuery<float>("select KM_GIATRI from KhuyenMai where KM_ID ='" + KM_ID + "' and KM_NgayKetThuc >= GETDATE()").SingleOrDefault();
                float giamgia = cHITIETSANPH.GetGia(sp.CTSP_ID)* KM_GIATRI;
                float newprice = cHITIETSANPH.GetGia(sp.CTSP_ID) - giamgia;
                CartItem newItem = new CartItem()
                {
                    SanPhamID = SanPhamID,
                    CTSP_ID = sp.CTSP_ID,
                    TenSanPham = sp.SP_TEN,
                    DonGia = db.Database.SqlQuery<int>("select Gia_Gia from GiaSP where Gia_ID='" + GiaID + "'").SingleOrDefault(),
                    DonGiaKM = newprice,
                    SoLuong = 1,

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                cardItem.SoLuong++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("");
        }

        public RedirectToRouteResult SuaSoLuong(string SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemSua != null)
            {
                itemSua.SoLuong = soluongmoi;
            }
            return RedirectToAction("Index");

        }

        public RedirectToRouteResult XoaKhoiGio(string SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
       
    }
}