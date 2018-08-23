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
    public class CHITIETBHsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CHITIETBHs
        public ActionResult Index()
        {
            var cHITIETBHs = db.CHITIETBHs.Include(c => c.BAOHANH).Include(c => c.SANPHAM).Include(c => c.TRANGTHAIBH);
            return View(cHITIETBHs.ToList());
        }

        // GET: CHITIETBHs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETBH cHITIETBH = db.CHITIETBHs.Find(id);
            if (cHITIETBH == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETBH);
        }

        // GET: CHITIETBHs/Create
        public ActionResult Create()
        {
            ViewBag.BH_ID = new SelectList(db.BAOHANHs, "BH_ID", "BH_TEN");
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN");
            ViewBag.TTBH_ID = new SelectList(db.TRANGTHAIBHs, "TTBH_ID", "TTBH_TEN");
            return View();
        }

        // POST: CHITIETBHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTBH_ID,SP_ID,BH_ID,TTBH_ID,CTBH_NGAYBH,CTBH_NGAYTRA,CTBH_GHICHU")] CHITIETBH cHITIETBH)
        {
            string SP_ID = Request["SP_ID"];
            string result = db.Database.SqlQuery<string>("select SP_ID from SanPham where SP_ID ='" + SP_ID + "'").SingleOrDefault();
            if (result != null)
            {
                cHITIETBH.CTBH_ID = db.autottang("ChiTietBH", "CTBH_ID", db.CHITIETBHs.Count()).ToString();
                db.CHITIETBHs.Add(cHITIETBH);
                db.SaveChanges();
                ModelState.AddModelError("", "Đã thêm " + result);

            }
            else
            {
                ModelState.AddModelError("", "Lỗi !");
            }
            ViewBag.BH_ID = new SelectList(db.BAOHANHs, "BH_ID", "BH_TEN", cHITIETBH.BH_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", cHITIETBH.SP_ID);
            ViewBag.TTBH_ID = new SelectList(db.TRANGTHAIBHs, "TTBH_ID", "TTBH_TEN", cHITIETBH.TTBH_ID);
            return View(cHITIETBH);
        }

        // GET: CHITIETBHs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETBH cHITIETBH = db.CHITIETBHs.Find(id);
            if (cHITIETBH == null)
            {
                return HttpNotFound();
            }
            ViewBag.BH_ID = new SelectList(db.BAOHANHs, "BH_ID", "BH_TEN", cHITIETBH.BH_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", cHITIETBH.SP_ID);
            ViewBag.TTBH_ID = new SelectList(db.TRANGTHAIBHs, "TTBH_ID", "TTBH_TEN", cHITIETBH.TTBH_ID);
            return View(cHITIETBH);
        }

        // POST: CHITIETBHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTBH_ID,SP_ID,BH_ID,TTBH_ID,CTBH_NGAYBH,CTBH_NGAYTRA,CTBH_GHICHU")] CHITIETBH cHITIETBH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETBH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BH_ID = new SelectList(db.BAOHANHs, "BH_ID", "BH_TEN", cHITIETBH.BH_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", cHITIETBH.SP_ID);
            ViewBag.TTBH_ID = new SelectList(db.TRANGTHAIBHs, "TTBH_ID", "TTBH_TEN", cHITIETBH.TTBH_ID);
            return View(cHITIETBH);
        }

        // GET: CHITIETBHs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETBH cHITIETBH = db.CHITIETBHs.Find(id);
            if (cHITIETBH == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETBH);
        }

        // POST: CHITIETBHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETBH cHITIETBH = db.CHITIETBHs.Find(id);
            db.CHITIETBHs.Remove(cHITIETBH);
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
