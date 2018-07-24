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
            string id = Session["KH_ID"].ToString();
            return View(db.KHACHHANGs.Where(k=>k.KH_ID == id).ToList());
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

            if (ModelState.IsValid)
            {
                kHACHHANG.KH_ID = db.autottang("KhachHang", "KH_ID", db.KHACHHANGs.Count()).ToString();
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult EditPass([Bind(Include = "KH_ID,KH_TEN,KH_EMAIL,KH_SDT,KH_DIACHI,KH_NGAYSINH,KH_GIOITINH,KH_TAIKHOAN,KH_MATKHAU")] KHACHHANG kHACHHANG)
        {
            string MK = Request["KH_MATKHAU"];
            string MK1 = Request["KH_MATKHAU1"];
            if(MK.Length <=0 )
            {
                ModelState.AddModelError("KH_MATKHAU", "Không được để trống mật khẩu ~~");
               
            }else if(MK1.Length <= 0)
            {
                ModelState.AddModelError("KH_MATKHAU1", "Không được để trống xấc nhận ~~");
            }
            else if(MK != MK1)
            {
                ModelState.AddModelError("", "Mật khẩu xác nhận không giống nhau ");
            }
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexKH");
            }
            return View(kHACHHANG);
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
    }
}
