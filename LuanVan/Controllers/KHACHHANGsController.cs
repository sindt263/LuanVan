using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;

namespace LuanVan.Controllers
{
    public class KHACHHANGsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: KHACHHANGs
        public ActionResult Index()
        {
            return View(db.KHACHHANGs.ToList());
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
        public ActionResult Create([Bind(Include = "KH_ID,KH_TEN,KH_EMAIL,KH_SDT,KH_DIACHI,KH_NGAYSINH,KH_GIOITINH,KH_TAIKHOAN,KH_MATKHAU")] KHACHHANG kHACHHANG)
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

                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();

                return RedirectToAction("LoginKH", "Logins");
            }

            return View(kHACHHANG);
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
        public ActionResult Edit([Bind(Include = "KH_ID,KH_TEN,KH_EMAIL,KH_SDT,KH_DIACHI,KH_NGAYSINH,KH_GIOITINH,KH_TAIKHOAN,KH_MATKHAU")] KHACHHANG kHACHHANG)
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
        public ActionResult EditKH([Bind(Include = "KH_ID,KH_TEN,KH_EMAIL,KH_SDT,KH_DIACHI,KH_NGAYSINH,KH_GIOITINH,KH_TAIKHOAN,KH_MATKHAU")] KHACHHANG kHACHHANG)
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
            
            if (id.Length <= 0)
            {
                string output = "Không được để trống xấc nhận !";
                return Json(output, JsonRequestBehavior.AllowGet);

            }
            else if (id1.Length <= 0)
            {
                string output = "Không được để trống xấc nhận !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else if (id != id1)
            {
                string output = "Mật khẩu xác nhận không giống nhau !";
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

        public ActionResult KTMK(string id, string id1)
        {
            if (id != id1)
            {
                ModelState.AddModelError("", "Mật khẩu và mật khẩu xác nhận chưa trùng khớp !");
            }
            else
            {
                ModelState.AddModelError("", "Chấp nhận !");
            }
            return View();
        }

        public JsonResult KTTK(string id)
        {
            string result = db.Database.SqlQuery<string>("select KH_TAIKHOAN from KHACHHANG where KH_TAIKHOAN='" + id + "'").SingleOrDefault();
            if (result != null)
            {
                string output = "Tài khoản đã tồn tại !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string output = "Có thể sử dụng !";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
