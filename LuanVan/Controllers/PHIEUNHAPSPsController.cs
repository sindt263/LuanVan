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
    public class PHIEUNHAPSPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: PHIEUNHAPSPs
        public ActionResult Index()
        {
            var pHIEUNHAPSPs = db.PHIEUNHAPSPs.Include(p => p.NHACUNGCAP).Include(p => p.NHANVIEN);
            return View(pHIEUNHAPSPs.ToList());
        }

        // GET: PHIEUNHAPSPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            if (pHIEUNHAPSP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAPSP);
        }

        // GET: PHIEUNHAPSPs/Create
        public ActionResult Create()
        {
            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN");
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN");
            return View();
        }

        // POST: PHIEUNHAPSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PN_ID,NV_ID,NCC_ID,PN_NGAY,PN_GHICHU")] PHIEUNHAPSP pHIEUNHAPSP)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUNHAPSPs.Add(pHIEUNHAPSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN", pHIEUNHAPSP.NCC_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", pHIEUNHAPSP.NV_ID);
            return View(pHIEUNHAPSP);
        }

        // GET: PHIEUNHAPSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            if (pHIEUNHAPSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN", pHIEUNHAPSP.NCC_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", pHIEUNHAPSP.NV_ID);
            return View(pHIEUNHAPSP);
        }

        // POST: PHIEUNHAPSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PN_ID,NV_ID,NCC_ID,PN_NGAY,PN_GHICHU")] PHIEUNHAPSP pHIEUNHAPSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUNHAPSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN", pHIEUNHAPSP.NCC_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", pHIEUNHAPSP.NV_ID);
            return View(pHIEUNHAPSP);
        }

        // GET: PHIEUNHAPSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            if (pHIEUNHAPSP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAPSP);
        }

        // POST: PHIEUNHAPSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            db.PHIEUNHAPSPs.Remove(pHIEUNHAPSP);
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
