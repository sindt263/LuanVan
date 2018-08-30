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
    public class HINHTHUCTHANHTOANsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: HINHTHUCTHANHTOANs
        public ActionResult Index()
        {
            return View(db.HINHTHUCTHANHTOANs.ToList());
        }

        // GET: HINHTHUCTHANHTOANs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHTHUCTHANHTOAN hINHTHUCTHANHTOAN = db.HINHTHUCTHANHTOANs.Find(id);
            if (hINHTHUCTHANHTOAN == null)
            {
                return HttpNotFound();
            }
            return View(hINHTHUCTHANHTOAN);
        }

        // GET: HINHTHUCTHANHTOANs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HINHTHUCTHANHTOANs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HTTT_ID,HTTT_TEN,HTTT_MOTA")] HINHTHUCTHANHTOAN hINHTHUCTHANHTOAN)
        {
            if (ModelState.IsValid)
            {
                hINHTHUCTHANHTOAN.HTTT_ID = Convert.ToInt16(db.autottang("HINHTHUCTHANHTOAN", "HTTT_ID", db.HINHTHUCTHANHTOANs.Count()));
                db.HINHTHUCTHANHTOANs.Add(hINHTHUCTHANHTOAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hINHTHUCTHANHTOAN);
        }

        // GET: HINHTHUCTHANHTOANs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHTHUCTHANHTOAN hINHTHUCTHANHTOAN = db.HINHTHUCTHANHTOANs.Find(id);
            if (hINHTHUCTHANHTOAN == null)
            {
                return HttpNotFound();
            }
            return View(hINHTHUCTHANHTOAN);
        }

        // POST: HINHTHUCTHANHTOANs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HTTT_ID,HTTT_TEN,HTTT_MOTA")] HINHTHUCTHANHTOAN hINHTHUCTHANHTOAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hINHTHUCTHANHTOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hINHTHUCTHANHTOAN);
        }

        // GET: HINHTHUCTHANHTOANs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHTHUCTHANHTOAN hINHTHUCTHANHTOAN = db.HINHTHUCTHANHTOANs.Find(id);
            if (hINHTHUCTHANHTOAN == null)
            {
                return HttpNotFound();
            }
            return View(hINHTHUCTHANHTOAN);
        }

        // POST: HINHTHUCTHANHTOANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            HINHTHUCTHANHTOAN hINHTHUCTHANHTOAN = db.HINHTHUCTHANHTOANs.Find(id);
            db.HINHTHUCTHANHTOANs.Remove(hINHTHUCTHANHTOAN);
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
