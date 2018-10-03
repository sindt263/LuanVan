using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using PagedList;
using PagedList.Mvc;

namespace LuanVan.Controllers
{
    public class KHUYENMAIsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: KHUYENMAIs

        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new KHUYENMAIsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<KHUYENMAI> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<KHUYENMAI> model = db.KHUYENMAIs;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.KM_ID.Contains(searchTerm) || x.KM_TEN.Contains(searchTerm) || x.KM_MOTA.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.KM_NGAYBATDAU).ToPagedList(page, pageSize);
        }
        // GET: KHUYENMAIs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI kHUYENMAI = db.KHUYENMAIs.Find(id);
            if (kHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(kHUYENMAI);
        }

        // GET: KHUYENMAIs/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: KHUYENMAIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KM_ID,KM_TEN,KM_NGAYBATDAU,KM_GIATRI,KM_NGAYKETTHUC,KM_MOTA")] KHUYENMAI kHUYENMAI)
        {
            string id = Request["KM_ID"];
            HINHANHKM hINHANHKM = new HINHANHKM();
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                Int32 length = file.ContentLength;
                byte[] tempImage = new byte[length];
                file.InputStream.Read(tempImage, 0, length);
                hINHANHKM.HAKM_ND = tempImage;
                hINHANHKM.KM_ID = id;
                hINHANHKM.HAKM_ID = db.autottang("HinhAnhKM", "HAKM_ID", db.HINHANHKMs.Count()).ToString();
                db.HINHANHKMs.Add(hINHANHKM);

                db.KHUYENMAIs.Add(kHUYENMAI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHUYENMAI);
        }
       
        // GET: KHUYENMAIs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI kHUYENMAI = db.KHUYENMAIs.Find(id);
            if (kHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(kHUYENMAI);
        }

        // POST: KHUYENMAIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KM_ID,KM_TEN,KM_NGAYBATDAU,KM_GIATRI,KM_NGAYKETTHUC,KM_MOTA")] KHUYENMAI kHUYENMAI)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(kHUYENMAI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHUYENMAI);
        }
         public ActionResult EditMany(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI kHUYENMAI = db.KHUYENMAIs.Find(id);
            if (kHUYENMAI == null)
            {
                return HttpNotFound();
            }

            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN");
            return View(kHUYENMAI);
        }

        // POST: KHUYENMAIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMany([Bind(Include = "KM_ID,KM_TEN,KM_NGAYBATDAU,KM_GIATRI,KM_NGAYKETTHUC,KM_MOTA")] KHUYENMAI kHUYENMAI)
        {
            string ctsp_id = Request["CTSP_ID"];  
            if (ModelState.IsValid)
            {
                db.Entry(kHUYENMAI).State = EntityState.Modified;
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("update sanpham set KM_ID ='"+kHUYENMAI.KM_ID+"' where CTSP_ID ='" + ctsp_id + "'");
                return RedirectToAction("Index");
            }

            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN");
            return View(kHUYENMAI);
        }

        // GET: KHUYENMAIs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI kHUYENMAI = db.KHUYENMAIs.Find(id);
            if (kHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(kHUYENMAI);
        }

        // POST: KHUYENMAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHUYENMAI kHUYENMAI = db.KHUYENMAIs.Find(id);
            db.KHUYENMAIs.Remove(kHUYENMAI);
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
