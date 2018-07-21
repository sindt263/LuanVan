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
    public class TRANGTHAIBHsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: TRANGTHAIBHs
        public ActionResult Index()
        {
            return View(db.TRANGTHAIBHs.ToList());
        }

        // GET: TRANGTHAIBHs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANGTHAIBH tRANGTHAIBH = db.TRANGTHAIBHs.Find(id);
            if (tRANGTHAIBH == null)
            {
                return HttpNotFound();
            }
            return View(tRANGTHAIBH);
        }

        // GET: TRANGTHAIBHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TRANGTHAIBHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TTBH_ID,TTBH_TEN,TTBH_MOTA")] TRANGTHAIBH tRANGTHAIBH)
        {
            if (ModelState.IsValid)
            {
                db.TRANGTHAIBHs.Add(tRANGTHAIBH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tRANGTHAIBH);
        }

        // GET: TRANGTHAIBHs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANGTHAIBH tRANGTHAIBH = db.TRANGTHAIBHs.Find(id);
            if (tRANGTHAIBH == null)
            {
                return HttpNotFound();
            }
            return View(tRANGTHAIBH);
        }

        // POST: TRANGTHAIBHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TTBH_ID,TTBH_TEN,TTBH_MOTA")] TRANGTHAIBH tRANGTHAIBH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRANGTHAIBH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRANGTHAIBH);
        }

        // GET: TRANGTHAIBHs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANGTHAIBH tRANGTHAIBH = db.TRANGTHAIBHs.Find(id);
            if (tRANGTHAIBH == null)
            {
                return HttpNotFound();
            }
            return View(tRANGTHAIBH);
        }

        // POST: TRANGTHAIBHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            TRANGTHAIBH tRANGTHAIBH = db.TRANGTHAIBHs.Find(id);
            db.TRANGTHAIBHs.Remove(tRANGTHAIBH);
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
