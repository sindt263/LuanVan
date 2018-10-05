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
    public class BINHLUANCTSPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BINHLUANCTSPs
        public ActionResult Index()
        {
            return View(db.BINHLUANCTSPs.ToList());
        }
        public ActionResult IndexKH(string id)
        {
            return View(db.BINHLUANCTSPs.Where(a=>a.CTSP_ID ==id).ToList());
        }

        // GET: BINHLUANCTSPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BINHLUANCTSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BL_ID,CTSP_ID,KH_ID,BL_ND,BL_THOIGIAN")] BINHLUANCTSP bINHLUANCTSP)
        {
            if (ModelState.IsValid)
            {
                db.BINHLUANCTSPs.Add(bINHLUANCTSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bINHLUANCTSP);
        }

        public ActionResult CreateKH(string id, string id2)
        {
            BINHLUANCTSP bINHLUANCTSP = new BINHLUANCTSP();
            if (ModelState.IsValid)
            {
                bINHLUANCTSP.BL_ID = db.autottang("BinhLuanCTSP", "BL_ID", db.BINHLUANCTSPs.Count()).ToString();
                bINHLUANCTSP.BL_ND = id;
                bINHLUANCTSP.CTSP_ID = id2;
                bINHLUANCTSP.BL_THOIGIAN = DateTime.Now;
                bINHLUANCTSP.KH_ID = Session["KH_ID"].ToString();
                db.BINHLUANCTSPs.Add(bINHLUANCTSP);
                db.SaveChanges();
                return RedirectToAction("IndexKH/"+id2);
            }

            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            return View(bINHLUANCTSP);
        }

        // POST: BINHLUANCTSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BL_ID,CTSP_ID,KH_ID,BL_ND,BL_THOIGIAN")] BINHLUANCTSP bINHLUANCTSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bINHLUANCTSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            return View(bINHLUANCTSP);
        }

        // POST: BINHLUANCTSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            db.BINHLUANCTSPs.Remove(bINHLUANCTSP);
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

        public string GetTenKH(string id)
        {
            string ten = db.Database.SqlQuery<string>("select KH_TEN from khachhang kh inner join BinhLuanCTSP bl on kh.KH_ID = bl.KH_ID where bl.KH_ID = '" + id + "'").FirstOrDefault();
            return ten;
        }
    }
}
