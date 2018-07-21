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
    public class TRANGTHAIDONHANGsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: TRANGTHAIDONHANGs
        public ActionResult Index()
        {
            return View(db.TRANGTHAIDONHANGs.ToList());
        }

        // GET: TRANGTHAIDONHANGs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANGTHAIDONHANG tRANGTHAIDONHANG = db.TRANGTHAIDONHANGs.Find(id);
            if (tRANGTHAIDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(tRANGTHAIDONHANG);
        }

        // GET: TRANGTHAIDONHANGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TRANGTHAIDONHANGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TTDH_ID,TTDH_TEN,TTDH_MOTA")] TRANGTHAIDONHANG tRANGTHAIDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.TRANGTHAIDONHANGs.Add(tRANGTHAIDONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tRANGTHAIDONHANG);
        }

        // GET: TRANGTHAIDONHANGs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANGTHAIDONHANG tRANGTHAIDONHANG = db.TRANGTHAIDONHANGs.Find(id);
            if (tRANGTHAIDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(tRANGTHAIDONHANG);
        }

        // POST: TRANGTHAIDONHANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TTDH_ID,TTDH_TEN,TTDH_MOTA")] TRANGTHAIDONHANG tRANGTHAIDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRANGTHAIDONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRANGTHAIDONHANG);
        }

        // GET: TRANGTHAIDONHANGs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANGTHAIDONHANG tRANGTHAIDONHANG = db.TRANGTHAIDONHANGs.Find(id);
            if (tRANGTHAIDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(tRANGTHAIDONHANG);
        }

        // POST: TRANGTHAIDONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            TRANGTHAIDONHANG tRANGTHAIDONHANG = db.TRANGTHAIDONHANGs.Find(id);
            db.TRANGTHAIDONHANGs.Remove(tRANGTHAIDONHANG);
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
