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
    public class LOAINVsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: LOAINVs
        public ActionResult Index()
        {
            return View(db.LOAINVs.ToList());
        }

        // GET: LOAINVs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAINV lOAINV = db.LOAINVs.Find(id);
            if (lOAINV == null)
            {
                return HttpNotFound();
            }
            return View(lOAINV);
        }

        // GET: LOAINVs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LOAINVs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LNV_ID,LNV_TEN,LNV_MOTA")] LOAINV lOAINV)
        {
            if (ModelState.IsValid)
            {
                db.LOAINVs.Add(lOAINV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAINV);
        }

        // GET: LOAINVs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAINV lOAINV = db.LOAINVs.Find(id);
            if (lOAINV == null)
            {
                return HttpNotFound();
            }
            return View(lOAINV);
        }

        // POST: LOAINVs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LNV_ID,LNV_TEN,LNV_MOTA")] LOAINV lOAINV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAINV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAINV);
        }

        // GET: LOAINVs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAINV lOAINV = db.LOAINVs.Find(id);
            if (lOAINV == null)
            {
                return HttpNotFound();
            }
            return View(lOAINV);
        }

        // POST: LOAINVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            LOAINV lOAINV = db.LOAINVs.Find(id);
            db.LOAINVs.Remove(lOAINV);
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
