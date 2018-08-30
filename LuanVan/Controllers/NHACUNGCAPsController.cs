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
    public class NHACUNGCAPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: NHACUNGCAPs
        public ActionResult Index()
        {
            return View(db.NHACUNGCAPs.ToList());
        }

        // GET: NHACUNGCAPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGCAP nHACUNGCAP = db.NHACUNGCAPs.Find(id);
            if (nHACUNGCAP == null)
            {
                return HttpNotFound();
            }
            return View(nHACUNGCAP);
        }

        // GET: NHACUNGCAPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NHACUNGCAPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NCC_ID,NCC_TEN,NCC_SDT,NCC_DIACHI,NCC_GHICHU")] NHACUNGCAP nHACUNGCAP)
        {
            if (ModelState.IsValid)
            {
                nHACUNGCAP.NCC_ID = db.autottang("NhaCungCap", "NCC_ID", db.NHACUNGCAPs.Count()).ToString();
                db.NHACUNGCAPs.Add(nHACUNGCAP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHACUNGCAP);
        }

        // GET: NHACUNGCAPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGCAP nHACUNGCAP = db.NHACUNGCAPs.Find(id);
            if (nHACUNGCAP == null)
            {
                return HttpNotFound();
            }
            return View(nHACUNGCAP);
        }

        // POST: NHACUNGCAPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NCC_ID,NCC_TEN,NCC_SDT,NCC_DIACHI,NCC_GHICHU")] NHACUNGCAP nHACUNGCAP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHACUNGCAP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHACUNGCAP);
        }

        // GET: NHACUNGCAPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGCAP nHACUNGCAP = db.NHACUNGCAPs.Find(id);
            if (nHACUNGCAP == null)
            {
                return HttpNotFound();
            }
            return View(nHACUNGCAP);
        }

        // POST: NHACUNGCAPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHACUNGCAP nHACUNGCAP = db.NHACUNGCAPs.Find(id);
            db.NHACUNGCAPs.Remove(nHACUNGCAP);
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
