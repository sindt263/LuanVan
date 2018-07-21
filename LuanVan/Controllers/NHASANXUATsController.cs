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
    public class NHASANXUATsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: NHASANXUATs
        public ActionResult Index()
        {
            return View(db.NHASANXUATs.ToList());
        }

        // GET: NHASANXUATs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            if (nHASANXUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHASANXUAT);
        }

        // GET: NHASANXUATs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NHASANXUATs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NSX_ID,NSX_TEN,NSX_SDT,NSX_QUOCGIA,NSX_GHICHU")] NHASANXUAT nHASANXUAT)
        {
            if (ModelState.IsValid)
            {
                db.NHASANXUATs.Add(nHASANXUAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHASANXUAT);
        }

        // GET: NHASANXUATs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            if (nHASANXUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHASANXUAT);
        }

        // POST: NHASANXUATs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NSX_ID,NSX_TEN,NSX_SDT,NSX_QUOCGIA,NSX_GHICHU")] NHASANXUAT nHASANXUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHASANXUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHASANXUAT);
        }

        // GET: NHASANXUATs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            if (nHASANXUAT == null)
            {
                return HttpNotFound();
            }
            return View(nHASANXUAT);
        }

        // POST: NHASANXUATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHASANXUAT nHASANXUAT = db.NHASANXUATs.Find(id);
            db.NHASANXUATs.Remove(nHASANXUAT);
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
