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
    public class HINHANHKMsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: HINHANHKMs
        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var HAKM = new HINHANHKMsController();
            var mode = HAKM.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<HINHANHKM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<HINHANHKM> model = db.HINHANHKMs.Include(h => h.KHUYENMAI); ;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.HAKM_ID.Contains(searchTerm) || x.KHUYENMAI.KM_TEN.Contains(searchTerm) );

            }

            return model.OrderByDescending(x => x.HAKM_ID).ToPagedList(page, pageSize);
        }
        // GET: HINHANHKMs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHKM hINHANHKM = db.HINHANHKMs.Find(id);
            if (hINHANHKM == null)
            {
                return HttpNotFound();
            }
            return View(hINHANHKM);
        }

        // GET: HINHANHKMs/Create
        public ActionResult Create()
        {
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN");
            return View();
        }

        // POST: HINHANHKMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HAKM_ID,KM_ID,HAKM_ND")] HINHANHKM hINHANHKM)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    Int32 length = file.ContentLength;
                    byte[] tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);
                    hINHANHKM.HAKM_ND = tempImage;
                    hINHANHKM.HAKM_ID = db.autottang("HinhAnhKM", "HAKM_ID", db.HINHANHKMs.Count()).ToString();

                }
                db.HINHANHKMs.Add(hINHANHKM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", hINHANHKM.KM_ID);
            return View(hINHANHKM);
        }

        // GET: HINHANHKMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHKM hINHANHKM = db.HINHANHKMs.Find(id);
            if (hINHANHKM == null)
            {
                return HttpNotFound();
            }
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", hINHANHKM.KM_ID);
            return View(hINHANHKM);
        }

        // POST: HINHANHKMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HAKM_ID,KM_ID,HAKM_ND")] HINHANHKM hINHANHKM)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 1)
                {

                    Int32 length = file.ContentLength;
                    byte[] tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);
                    hINHANHKM.HAKM_ND = tempImage;
                }
                db.Entry(hINHANHKM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", hINHANHKM.KM_ID);
            return View(hINHANHKM);
        }

        // GET: HINHANHKMs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHKM hINHANHKM = db.HINHANHKMs.Find(id);
            if (hINHANHKM == null)
            {
                return HttpNotFound();
            }
            return View(hINHANHKM);
        }

        // POST: HINHANHKMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HINHANHKM hINHANHKM = db.HINHANHKMs.Find(id);
            db.HINHANHKMs.Remove(hINHANHKM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult getImageHA(string id)
        {
            string strID = Request.QueryString["ID"];
            if (id != null)
            {
                //var ha = db.HinhAnhHoatDongs.Where(h => h.HD_IDHoatDong == ID).FirstOrDefault();
                var ha = (from p in db.HINHANHKMs where p.KM_ID == id select p);
                foreach (var i in ha)
                {
                    if (i == null || i.HAKM_ND == null)
                    {
                        ModelState.AddModelError("", "Loi");
                    }
                    //Response.ContentType = "image/jpeg";
                    Response.OutputStream.Write(i.HAKM_ND.ToArray(), 0, i.HAKM_ND.Length);
                    Response.Flush();
                }
            }
            return View();
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
