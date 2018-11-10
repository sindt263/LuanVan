using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using LuanVan.Controllers;
using PagedList;
using PagedList.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace LuanVan.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        //public ActionResult Index()
        //{
        //    ViewBag.NSX = db.NHASANXUATs.ToList();
        //    ViewBag.sp = db.CHITIETSANPHAMs.ToList();
        //    var sp = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM).Include(n => n.CHITIETNHAPs).OrderByDescending(s => s.CTSP_ID);
        //    return View(sp);
        //}
        private string key = "SSSShop";

        /// <summary>
        /// Mã hóa chuỗi có mật khẩu
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa</returns>
        public string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public ViewResult test()
        {
            ViewBag.a =Encrypt("123456");
            return View();
        }
        [HttpPost]
        public ViewResult test(string id)
        {
            int male = 0; ;
            for (int i = 1; i <= 4; i++)
            {
                string YourRadioButton = Request.Form[i.ToString()];
                if (YourRadioButton == "male")
                {
                    male++;
                }
            }
            ViewBag.YourRadioButton = male;

            return View();
        }

        public ActionResult Index()
        {
            //ViewSP
            ViewBag.nsx = db.NHASANXUATs.ToList();



            ViewBag.KM = (from p in db.KHUYENMAIs where p.KM_NGAYKETTHUC >= DateTime.Now && p.KM_ID != "0" select p).OrderByDescending(a => a.KM_NGAYBATDAU);


            ViewBag.SPMoi = db.SANPHAMs.OrderByDescending(n => n.SP_NGAYTAO).Take(6);


            return View(db.SANPHAMs.ToList());
        }

        public ActionResult SearchSP(string searchTerm, int id = 0, int id2 = 0, int page = 1, int pageSize = 100)
        {
            List<SoSanhSP> sosanh = Session["sosanh"] as List<SoSanhSP>;
            ViewBag.sosanh = sosanh;

            var SanPhams = new HomeController();
            var mode = SanPhams.ListAllPaging(searchTerm, id, id2, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public ViewResult HienSoSanh(string id)
        {
            CHITIETSANPHAM ctsp = db.CHITIETSANPHAMs.Find(id);
            return View(ctsp);
        }
        public RedirectToRouteResult ThemSoSanh(string CTSP_ID)
        {
            if (Session["sosanh"] == null) // Nếu sản phẩm  chưa được khởi tạo
            {
                Session["sosanh"] = new List<SoSanhSP>();  // Khởi tạo Session là 1 List<SoSanh>
            }

            List<SoSanhSP> sosanh = Session["sosanh"] as List<SoSanhSP>;  // Gán qua biến sosanh dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (sosanh.FirstOrDefault(m => m.CTSP_ID == CTSP_ID) == null) // ko co sp nay trong gio hang
            {
                if (sosanh.Count >= 2)
                {
                    sosanh.Remove(sosanh.LastOrDefault());
                }
                CHITIETSANPHAMsController cHITIETSANPH = new CHITIETSANPHAMsController();
                CHITIETSANPHAM sp = db.CHITIETSANPHAMs.Find(CTSP_ID);  // tim sp theo id
                SoSanhSP ss = new SoSanhSP()
                {
                    CTSP_ID = CTSP_ID,

                };  // Tạo ra 1 CartItem mới

                sosanh.Add(ss);  // Thêm CartItem vào giỏ 
            }
            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("SearchSP");
        }

        public RedirectToRouteResult XoaDoiTuong(string CTSP_ID)
        {
            List<SoSanhSP> sosanh = Session["sosanh"] as List<SoSanhSP>;
            SoSanhSP itemXoa = sosanh.FirstOrDefault(m => m.CTSP_ID == CTSP_ID);
            if (itemXoa != null)
            {
                sosanh.Remove(itemXoa);
            }
            return RedirectToAction("SearchSP");
        }


        public IEnumerable<CHITIETSANPHAM> ListAllPaging(string searchTerm, int id, int id2, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs;

            if (id != 0 && id2 != 0 && string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.SP_ID equals ctsp.SP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA >= id && gia.GIA_GIA <= id2 select ctsp).Distinct();
                return result.OrderByDescending(x => x.SANPHAM.SP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if (id == 0 && id2 == 0 && !string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.SP_ID equals ctsp.SP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where (ctsp.CTSP_TEN.Contains(searchTerm) || sp.SP_MOTA.Contains(searchTerm)) select ctsp).Distinct();
                return result.OrderByDescending(x => x.SANPHAM.SP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if (id != 0 && id2 == 0 && string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.SP_ID equals ctsp.SP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA >= id select ctsp).Distinct();
                return result.OrderByDescending(x => x.SANPHAM.SP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if (id != 0 && id2 == 0 && !string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.SP_ID equals ctsp.SP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA >= id && (ctsp.CTSP_TEN.Contains(searchTerm) || sp.SP_MOTA.Contains(searchTerm)) select ctsp).Distinct();
                return result.OrderByDescending(x => x.SANPHAM.SP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if (id == 0 && id2 != 0 && string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.SP_ID equals ctsp.SP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA <= id2 select ctsp).Distinct();
                return result.OrderByDescending(x => x.SANPHAM.SP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if (id == 0 && id2 != 0 && !string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.SP_ID equals ctsp.SP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA <= id2 && (ctsp.CTSP_TEN.Contains(searchTerm) || sp.SP_MOTA.Contains(searchTerm)) select ctsp).Distinct();
                return result.OrderByDescending(x => x.SANPHAM.SP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else
            {
                return model;
            }



        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PagesEmpty()
        {
            return View();
        }

        public int count(string id)
        {
            string sp_id = db.Database.SqlQuery<string>("select SP_ID from SanPham where CTSP_ID='" + id + "'").Take(1).SingleOrDefault();
            int result = db.Database.SqlQuery<int>("select count(*) from ChiTietDonHang where SP_ID ='" + sp_id + "'").SingleOrDefault();
            return result;
        }


    }
}