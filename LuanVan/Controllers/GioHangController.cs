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
        public ActionResult Index(string id)
        {
            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN");
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.diachi = db.Database.SqlQuery<string>("select ctdn.CTDH_DIACHIGIAO from CHITIETDONHANG ctdn inner join DONHANG dn on ctdn.DN_ID = dn.DN_ID and dn.KH_ID ='" + Session["KH_ID"] + "' group by ctdn.CTDH_DIACHIGIAO").LastOrDefault();
            Session["muathanhcong"] = null;

            return View(giohang);

        }

        [HttpPost]
        public ActionResult Index(string HTTT_ID, string TXTSL, string CTDH_DIACHIGIAO, string DN_SDT)
        {
            ViewBag.sdt = DN_SDT;
            ViewBag.diachi = CTDH_DIACHIGIAO;

            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN");
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            ViewBag.diachi = db.Database.SqlQuery<string>("select ctdn.CTDH_DIACHIGIAO from CHITIETDONHANG ctdn inner join DONHANG dn on ctdn.DN_ID = dn.DN_ID and dn.KH_ID ='" + Session["KH_ID"] + "' group by ctdn.CTDH_DIACHIGIAO").LastOrDefault();


            if (ModelState.IsValid)
            {
                if (DN_SDT.Length <= 9 || !DN_SDT.StartsWith("09") && !DN_SDT.StartsWith("08") )
                {
                    ModelState.AddModelError("", "Vui lòng điền nhập đúng SĐT !");
                }
                else if(string.IsNullOrEmpty(CTDH_DIACHIGIAO))
                {
                    ModelState.AddModelError("", "Vui lòng điền nhập địa chỉ !");
                }
                else
                {

                    SANPHAMsController sANPHAM = new SANPHAMsController();

                    DONHANG dONHANG = new DONHANG();

                    if (Session["DN_ID"] == null)
                    {
                        Session["DN_ID"] = db.autottang("DonHang", "DN_ID", db.DONHANGs.Count());
                        //dONHANG.NV_ID = Session["NV_ID"].ToString();
                        dONHANG.DN_ID = Session["DN_ID"].ToString();
                        dONHANG.DN_SDT = DN_SDT;
                        dONHANG.TTDH_ID = 4;
                        if (Session["KH_ID"] != null)
                        {
                            string KH_ID = Session["KH_ID"].ToString();
                            dONHANG.KH_ID = KH_ID;
                        }

                        dONHANG.DN_NGALAPDON = DateTime.Now;
                        dONHANG.DN_GHICHU = "Khách đặc Online";
                        dONHANG.HTTT_ID = Convert.ToInt16(HTTT_ID);
                        dONHANG.DN_SL = Convert.ToInt32(TXTSL);
                        db.DONHANGs.Add(dONHANG);
                        db.SaveChanges();
                    }

                    string DN_ID = Session["DN_ID"].ToString();
                    CHITIETDONHANG cHITIETDONHANG = new CHITIETDONHANG();
                    List<string> XoaItem = new List<string>();
                    foreach (var i in giohang)
                    {
                        string CTDH_ID = db.autottang("CHITIETDONHANG", "CTDH_ID", db.CHITIETDONHANGs.Count()).ToString();
                        XoaItem.Add(i.SanPhamID);
                        string SP_ID = i.SanPhamID;
                        short TT = db.Database.SqlQuery<short>("select SP_TRANGTHAI from SanPham where SP_ID ='" + SP_ID + "'").SingleOrDefault();
                        if (TT == 1)
                        {
                            db.Database.ExecuteSqlCommand("Insert into ChiTietDonHang (CTDH_ID,DN_ID,SP_ID,CTDH_DIACHIGIAO) values('" + CTDH_ID + "','" + DN_ID + "','" + SP_ID + "',N'" + CTDH_DIACHIGIAO + "')");
                            db.Database.ExecuteSqlCommand("update sanpham set SP_TRANGTHAI =0 where SP_ID ='" + SP_ID + "'");

                            ModelState.AddModelError("", "Xạc nhận mua " + i.TenSanPham + " thành công");
                        }
                        //CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == i.SanPhamID);
                        //giohang.Remove(itemXoa);
                    }
                    ModelState.AddModelError("", "Đã thêm chờ hàng vui lòng chờ duyệt đơn !");
                    Session["muathanhcong"] = 1;
                    foreach (var i in XoaItem)
                    {
                        
                        CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == i);
                        giohang.Remove(itemXoa);
                    }
                }

            }
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
                string KM_ID = db.Database.SqlQuery<string>("select KM_ID from SanPham where CTSP_ID ='" + sp.CTSP_ID + "'").Take(1).SingleOrDefault();
                float KM_GIATRI = db.Database.SqlQuery<float>("select KM_GIATRI from KhuyenMai where KM_ID ='" + KM_ID + "' and KM_NgayKetThuc >= GETDATE()").SingleOrDefault();
                float giamgia = cHITIETSANPH.GetGia(sp.CTSP_ID) * KM_GIATRI;
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
            return RedirectToAction("Index");
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