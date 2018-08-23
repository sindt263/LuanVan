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
    public class BAOHANHsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BAOHANHs
        public ActionResult Index()
        {
            return View(db.BAOHANHs.ToList());
        }

        // GET: BAOHANHs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAOHANH bAOHANH = db.BAOHANHs.Find(id);
            if (bAOHANH == null)
            {
                return HttpNotFound();
            }
            return View(bAOHANH);
        }

        // GET: BAOHANHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BAOHANHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BH_ID,BH_TEN,BH_MOTA")] BAOHANH bAOHANH)
        {
            string id = Request["BH_ID"];
           
            if (ModelState.IsValid)
            {
                bAOHANH.BH_ID = Convert.ToInt16(db.autottang("BaoHanh", "BH_ID", db.BAOHANHs.Count()));
                db.BAOHANHs.Add(bAOHANH);
                db.SaveChanges();
                ModelState.AddModelError("", "Đã thêm " + id);


            }
            else
            {
                ModelState.AddModelError("", "ID bị trùng " + id);
            }

            return View(bAOHANH);
        }

        // GET: BAOHANHs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAOHANH bAOHANH = db.BAOHANHs.Find(id);
            if (bAOHANH == null)
            {
                return HttpNotFound();
            }
            return View(bAOHANH);
        }

        // POST: BAOHANHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BH_ID,BH_TEN,BH_MOTA")] BAOHANH bAOHANH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bAOHANH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bAOHANH);
        }

        // GET: BAOHANHs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAOHANH bAOHANH = db.BAOHANHs.Find(id);
            if (bAOHANH == null)
            {
                return HttpNotFound();
            }
            return View(bAOHANH);
        }

        // POST: BAOHANHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            BAOHANH bAOHANH = db.BAOHANHs.Find(id);
            db.BAOHANHs.Remove(bAOHANH);
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
