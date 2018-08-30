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
    public class NHOMSANPHAMsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: NHOMSANPHAMs
        public ActionResult Index()
        {
            return View(db.NHOMSANPHAMs.ToList());
        }

        // GET: NHOMSANPHAMs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOMSANPHAM nHOMSANPHAM = db.NHOMSANPHAMs.Find(id);
            if (nHOMSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(nHOMSANPHAM);
        }

        // GET: NHOMSANPHAMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NHOMSANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NSP_ID,NSP_TEN,NSP_MOTA")] NHOMSANPHAM nHOMSANPHAM)
        {
            if (ModelState.IsValid)
            {
                nHOMSANPHAM.NSP_ID = db.autottang("nhomSanPham", "NSP_ID", db.NHOMSANPHAMs.Count()).ToString();
                db.NHOMSANPHAMs.Add(nHOMSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHOMSANPHAM);
        }

        // GET: NHOMSANPHAMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOMSANPHAM nHOMSANPHAM = db.NHOMSANPHAMs.Find(id);
            if (nHOMSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(nHOMSANPHAM);
        }

        // POST: NHOMSANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NSP_ID,NSP_TEN,NSP_MOTA")] NHOMSANPHAM nHOMSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHOMSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHOMSANPHAM);
        }

        // GET: NHOMSANPHAMs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOMSANPHAM nHOMSANPHAM = db.NHOMSANPHAMs.Find(id);
            if (nHOMSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(nHOMSANPHAM);
        }

        // POST: NHOMSANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHOMSANPHAM nHOMSANPHAM = db.NHOMSANPHAMs.Find(id);
            db.NHOMSANPHAMs.Remove(nHOMSANPHAM);
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
