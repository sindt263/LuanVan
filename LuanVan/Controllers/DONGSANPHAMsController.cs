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
    public class DONGSANPHAMsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: DONGSANPHAMs
       
         public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new DONGSANPHAMsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<DONGSANPHAM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<DONGSANPHAM> model = db.DONGSANPHAMs;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.DSP_ID.Contains(searchTerm) || x.DSP_TEN.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.DSP_ID).ToPagedList(page, pageSize);
        }
        // GET: DONGSANPHAMs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONGSANPHAM dONGSANPHAM = db.DONGSANPHAMs.Find(id);
            if (dONGSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(dONGSANPHAM);
        }

        // GET: DONGSANPHAMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DONGSANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DSP_ID,DSP_TEN,DSP_MOTA")] DONGSANPHAM dONGSANPHAM)
        {
            if (ModelState.IsValid)
            {
                dONGSANPHAM.DSP_ID = db.autottang("DongSanPham", "DSP_ID", db.DONGSANPHAMs.Count()).ToString();
                db.DONGSANPHAMs.Add(dONGSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dONGSANPHAM);
        }

        // GET: DONGSANPHAMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONGSANPHAM dONGSANPHAM = db.DONGSANPHAMs.Find(id);
            if (dONGSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(dONGSANPHAM);
        }

        // POST: DONGSANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DSP_ID,DSP_TEN,DSP_MOTA")] DONGSANPHAM dONGSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONGSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dONGSANPHAM);
        }

        // GET: DONGSANPHAMs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONGSANPHAM dONGSANPHAM = db.DONGSANPHAMs.Find(id);
            if (dONGSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(dONGSANPHAM);
        }

        // POST: DONGSANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DONGSANPHAM dONGSANPHAM = db.DONGSANPHAMs.Find(id);
            db.DONGSANPHAMs.Remove(dONGSANPHAM);
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
