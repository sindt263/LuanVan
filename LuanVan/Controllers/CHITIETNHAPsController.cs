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
    public class CHITIETNHAPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CHITIETNHAPs
        public ActionResult Index()
        {
            var cHITIETNHAPs = db.CHITIETNHAPs.Include(c => c.PHIEUNHAPSP).Include(c => c.SANPHAM);
            return View(cHITIETNHAPs.ToList());
        }

        // GET: CHITIETNHAPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETNHAP cHITIETNHAP = db.CHITIETNHAPs.Find(id);
            if (cHITIETNHAP == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETNHAP);
        }

        // GET: CHITIETNHAPs/Create
        public ActionResult Create()
        {
            ViewBag.PN_ID = new SelectList(db.PHIEUNHAPSPs, "PN_ID", "NV_ID");
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "NSP_ID");
            return View();
        }

        // POST: CHITIETNHAPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTN_ID,PN_ID,SP_ID,CTN_GIA")] CHITIETNHAP cHITIETNHAP)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETNHAPs.Add(cHITIETNHAP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PN_ID = new SelectList(db.PHIEUNHAPSPs, "PN_ID", "NV_ID", cHITIETNHAP.PN_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "NSP_ID", cHITIETNHAP.SP_ID);
            return View(cHITIETNHAP);
        }

        // GET: CHITIETNHAPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETNHAP cHITIETNHAP = db.CHITIETNHAPs.Find(id);
            if (cHITIETNHAP == null)
            {
                return HttpNotFound();
            }
            ViewBag.PN_ID = new SelectList(db.PHIEUNHAPSPs, "PN_ID", "NV_ID", cHITIETNHAP.PN_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "NSP_ID", cHITIETNHAP.SP_ID);
            return View(cHITIETNHAP);
        }

        // POST: CHITIETNHAPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTN_ID,PN_ID,SP_ID,CTN_GIA")] CHITIETNHAP cHITIETNHAP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETNHAP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PN_ID = new SelectList(db.PHIEUNHAPSPs, "PN_ID", "NV_ID", cHITIETNHAP.PN_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "NSP_ID", cHITIETNHAP.SP_ID);
            return View(cHITIETNHAP);
        }

        // GET: CHITIETNHAPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETNHAP cHITIETNHAP = db.CHITIETNHAPs.Find(id);
            if (cHITIETNHAP == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETNHAP);
        }

        // POST: CHITIETNHAPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETNHAP cHITIETNHAP = db.CHITIETNHAPs.Find(id);
            db.CHITIETNHAPs.Remove(cHITIETNHAP);
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
