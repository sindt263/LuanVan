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
    public class CHITIETSANPHAMsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CHITIETSANPHAMs
        public ActionResult Index()
        {
            return View(db.CHITIETSANPHAMs.ToList());
        }

        // GET: CHITIETSANPHAMs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETSANPHAM cHITIETSANPHAM = db.CHITIETSANPHAMs.Find(id);
            if (cHITIETSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETSANPHAM);
        }

        // GET: CHITIETSANPHAMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CHITIETSANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTSP_ID,CTSP_TEN,CTSP_CNMANGHINH,CTSP_DOPHANGIAI,CTSP_MANHINH,CTSP_CAMERATRUOC,CTSP_CAMERASAU,CTSP_HEDIEUHANH,CTSP_RAM,CTSP_ROM,CTSP_DUNGLUONGPIN,CTSP_SOSIM")] CHITIETSANPHAM cHITIETSANPHAM)
        {
            if (ModelState.IsValid)
            {
                cHITIETSANPHAM.CTSP_ID = db.autottang("ChiTietSanPham", "CTSP_ID", db.CHITIETSANPHAMs.Count()).ToString();
                db.CHITIETSANPHAMs.Add(cHITIETSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cHITIETSANPHAM);
        }

        // GET: CHITIETSANPHAMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETSANPHAM cHITIETSANPHAM = db.CHITIETSANPHAMs.Find(id);
            if (cHITIETSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETSANPHAM);
        }

        // POST: CHITIETSANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTSP_ID,CTSP_TEN,CTSP_CNMANGHINH,CTSP_DOPHANGIAI,CTSP_MANHINH,CTSP_CAMERATRUOC,CTSP_CAMERASAU,CTSP_HEDIEUHANH,CTSP_RAM,CTSP_ROM,CTSP_DUNGLUONGPIN,CTSP_SOSIM")] CHITIETSANPHAM cHITIETSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHITIETSANPHAM);
        }

        // GET: CHITIETSANPHAMs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETSANPHAM cHITIETSANPHAM = db.CHITIETSANPHAMs.Find(id);
            if (cHITIETSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETSANPHAM);
        }

        // POST: CHITIETSANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETSANPHAM cHITIETSANPHAM = db.CHITIETSANPHAMs.Find(id);
            db.CHITIETSANPHAMs.Remove(cHITIETSANPHAM);
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
