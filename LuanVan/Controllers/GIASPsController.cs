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
    public class GIASPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: GIASPs
        public ActionResult Index()
        {
            return View(db.GIASPs.ToList());
        }

        // GET: GIASPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIASP gIASP = db.GIASPs.Find(id);
            if (gIASP == null)
            {
                return HttpNotFound();
            }
            return View(gIASP);
        }

        // GET: GIASPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GIASPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GIA_ID,GIA_GIA,GIA_NGAYCAPNHAT")] GIASP gIASP)
        {
            if (ModelState.IsValid)
            {
                db.GIASPs.Add(gIASP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gIASP);
        }

        // GET: GIASPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIASP gIASP = db.GIASPs.Find(id);
            if (gIASP == null)
            {
                return HttpNotFound();
            }
            return View(gIASP);
        }

        // POST: GIASPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GIA_ID,GIA_GIA,GIA_NGAYCAPNHAT")] GIASP gIASP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIASP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gIASP);
        }

        // GET: GIASPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIASP gIASP = db.GIASPs.Find(id);
            if (gIASP == null)
            {
                return HttpNotFound();
            }
            return View(gIASP);
        }

        // POST: GIASPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GIASP gIASP = db.GIASPs.Find(id);
            db.GIASPs.Remove(gIASP);
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
