using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using PagedList;
using PagedList.Mvc;

namespace LuanVan.Controllers
{
    public class KHACHHANGsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: KHACHHANGs

        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new KHACHHANGsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<KHACHHANG> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<KHACHHANG> model = db.KHACHHANGs;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.KH_ID.Contains(searchTerm) || x.KH_TEN.Contains(searchTerm) || x.KH_SDT.ToString().Contains(searchTerm) || x.KH_TAIKHOAN.ToString().Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.KH_ID).ToPagedList(page, pageSize);
        }
        public ActionResult IndexKH()
        {
            if (Session["KH_ID"] != null)
            {
                string id = Session["KH_ID"].ToString();
                KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
                if (kHACHHANG == null)
                {
                    return HttpNotFound();
                }
                return View(kHACHHANG);
            }
            return RedirectToAction("LoginKH", "Logins");
        }

        // GET: KHACHHANGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: KHACHHANGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KHACHHANGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KH_ID,KH_TEN,KH_EMAIL,KH_SDT,KH_DIACHI,KH_NGAYSINH,KH_GIOITINH,KH_TAIKHOAN,KH_MATKHAU,KH_TRANGTHAI")] KHACHHANG kHACHHANG)
        {
          
            string MK = Request["KH_MATKHAU"];
            string MK1 = Request["KH_MATKHAU1"];
            string TK = Request["KH_TAIKHOAN"];
            string result = db.Database.SqlQuery<string>("select KH_TAIKHOAN from KHACHHANG where KH_TAIKHOAN='" + TK + "'").SingleOrDefault();
            if (result != null)
            {
                ModelState.AddModelError("", "Tài khoản đã tồn tại !");
            }
            else if (MK != MK1)
            {
                ModelState.AddModelError("", "Nhập mật khẩu chưa đúng !");
            }
            if (ModelState.IsValid)
            {
                kHACHHANG.KH_ID = db.autottang("KhachHang", "KH_ID", db.KHACHHANGs.Count()).ToString();
                kHACHHANG.KH_TRANGTHAI = "Bình thường";
                kHACHHANG.KH_MATKHAU = Encrypt(MK);
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("LoginKH", "Logins");
            }

            return View();
        }

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


        // GET: KHACHHANGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KHACHHANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KH_ID,KH_TEN,KH_EMAIL,KH_SDT,KH_DIACHI,KH_NGAYSINH,KH_GIOITINH,KH_TAIKHOAN,KH_MATKHAU,KH_TRANGTHAI")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }

        public ActionResult EditKH()
        {
            string id = Session["KH_ID"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KHACHHANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKH([Bind(Include = "KH_ID,KH_TEN,KH_EMAIL,KH_SDT,KH_DIACHI,KH_NGAYSINH,KH_GIOITINH,KH_TAIKHOAN,KH_MATKHAU,KH_TRANGTHAI")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexKH");
            }
            return View(kHACHHANG);
        }
        public ActionResult EditPass()
        {

            if (Session["KH_ID"] == null)
            {

                return RedirectToAction("LoginKH", "Logins");
            }
            string id = Session["KH_ID"].ToString();
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }


        public JsonResult TKEditPass(string id, string id1)
        {

            if (id.Length <= 5)
            {
                string output = "Mật khẩu phải dài hơn 6 ký tự !";
                return Json(output, JsonRequestBehavior.AllowGet);

            }
            else if (id1.Length <= 0)
            {
                string output = "Không được để trống mật khẩu xác nhận !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else if (id != id1)
            {
                string output = "Mật khẩu không khớp nhau !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string kh_id = Session["KH_ID"].ToString();
                KHACHHANG kHACHHANG = db.KHACHHANGs.Find(kh_id);
                if (kHACHHANG != null)
                {
                    kHACHHANG.KH_MATKHAU = id;
                    db.SaveChanges();

                }
                string output = "Mật khẩu đã được thay đổi !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: KHACHHANGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KHACHHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            db.KHACHHANGs.Remove(kHACHHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult KTMK(string id, string id1)
        {
            if(id.Length<=5 || id1.Length <= 5)
            {
                string output = "Mật khẩu quá ngắn !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }else
            if (id != id1)
            {
                string output = "Mật khẩu và mật khẩu xác nhận chưa trùng khớp !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string output = "Chấp nhận !";
                Session["MKOK"] = 1;
                return Json(output, JsonRequestBehavior.AllowGet);
                
            }
        }

        public JsonResult KTTK(string KH_TAIKHOAN)
        {
            string result = db.Database.SqlQuery<string>("select KH_TAIKHOAN from KHACHHANG where KH_TAIKHOAN='" + KH_TAIKHOAN + "'").SingleOrDefault();
            if (result != null)
            {
                string output = "Tài khoản đã tồn tại !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string output = "Có thể sử dụng tài khoản này!";
                Session["TKOK"] = 1;
                return Json(output, JsonRequestBehavior.AllowGet);
            }
        }

        public ViewResult CreateKH()
        {
            return View();
        }

        [HttpPost, ActionName("CreateKH")]
        [ValidateAntiForgeryToken]
        public ActionResult  CreateKH(string KH_TEN, string KH_EMAIL, string KH_DIACHI,string KH_NGAYSINH, string KH_TAIKHOAN, string KH_MATKHAU, string KH_MATKHAU1,string KH_GIOITINH,string KH_SDT)
        {
            try
            {
                var KHEMAIL = db.Database.SqlQuery<string>("select KH_EMAIL from KhachHang where KH_EMAIL = '" + KH_EMAIL + "'").FirstOrDefault();
                string result = db.Database.SqlQuery<string>("select KH_TAIKHOAN from KHACHHANG where KH_TAIKHOAN='" + KH_TAIKHOAN + "'").SingleOrDefault();
                if (KHEMAIL != null)
                {
                    ModelState.AddModelError("", "Email này đã được sử dụng !");
                }
                else if (result != null)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại ");
                    //string output = "Tài khoản đã tồn tại !";
                    //return Json(output, JsonRequestBehavior.AllowGet);
                }
                else if (KH_MATKHAU != KH_MATKHAU1 || KH_MATKHAU.Length <= 5)
                {
                    ModelState.AddModelError("", "Mật khẩu chưa đúng");
                    //string output = "Nhập mật khẩu chưa đúng !";
                    //return Json(output, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    KHACHHANG kHACHHANG = new KHACHHANG();
                    kHACHHANG.KH_ID = db.autottang("KhachHang", "KH_ID", db.KHACHHANGs.Count()).ToString();
                    kHACHHANG.KH_TEN = KH_TEN;
                    kHACHHANG.KH_EMAIL = KH_EMAIL;
                    kHACHHANG.KH_DIACHI = KH_DIACHI;
                    kHACHHANG.KH_TAIKHOAN = KH_TAIKHOAN;
                    kHACHHANG.KH_MATKHAU = Encrypt(KH_MATKHAU);
                    kHACHHANG.KH_GIOITINH = KH_GIOITINH;
                    kHACHHANG.KH_SDT = KH_SDT;
                    kHACHHANG.KH_TRANGTHAI = "Bình thường";
                    kHACHHANG.KH_NGAYSINH = Convert.ToDateTime(KH_NGAYSINH);
                    db.KHACHHANGs.Add(kHACHHANG);
                    db.SaveChanges();
                    //Session["success"] = "1";
                    //string output = "Đăng ký thành công !";
                    //return Json(output, JsonRequestBehavior.AllowGet);
                    ModelState.AddModelError("", "Đăng ký thành công");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Có lỗi tròn lúc nhập dữ liệu, vui lòng điền đúng theo yêu cầu");
                //string output = "Có lỗi trong quá trình nhập dữ liệu !";
                //return Json(output, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public JsonResult KTEMAIL(string KH_EMAIL)
        {
            var result = db.Database.SqlQuery<string>("select KH_EMAIL from KhachHang where KH_EMAIL = '"+KH_EMAIL+"'").FirstOrDefault();
            if (result == null)
            {
                string output = "Có thể sử dụng email này !";
                Session["EMAILOK"] = 1;
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string output = "Email đã tồn tại !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
