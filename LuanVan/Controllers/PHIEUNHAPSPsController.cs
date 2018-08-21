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
    public class PHIEUNHAPSPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: PHIEUNHAPSPs
        public ActionResult Index()
        {
            var pHIEUNHAPSPs = db.PHIEUNHAPSPs.Include(p => p.NHACUNGCAP).Include(p => p.NHANVIEN);
            return View(pHIEUNHAPSPs.ToList());
        }

        // GET: PHIEUNHAPSPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            if (pHIEUNHAPSP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAPSP);
        }

        // GET: PHIEUNHAPSPs/Create
        public ActionResult Create()
        {
            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN");
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN");
            return View();
        }

        // POST: PHIEUNHAPSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PN_ID,NV_ID,NCC_ID,PN_NGAY,PN_GHICHU")] PHIEUNHAPSP pHIEUNHAPSP)
        {
            string NCC_ID = Request["NCC_ID"];
            string PN_NGAY = Request["PN_NGAY"];
            string PN_GHICHU = Request["PN_GHICHU"];
            if (ModelState.IsValid)
            {
                pHIEUNHAPSP.PN_ID = db.autottang("PHIEUNHAPSP", "PN_ID", db.PHIEUNHAPSPs.Count()).ToString();
                db.PHIEUNHAPSPs.Add(pHIEUNHAPSP);

                db.SaveChanges();

                Session["PNSP"] = pHIEUNHAPSP.PN_ID;
                return RedirectToAction("Index");
               
            }

            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN", pHIEUNHAPSP.NCC_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", pHIEUNHAPSP.NV_ID);
            return View(pHIEUNHAPSP);
        }
        public ActionResult CreatePN(string id, string id2, string id3,string id4)
        {
            string testid = db.Database.SqlQuery<string>("select PN_ID from PhieuNhapsp where PN_ID = '"+id4+"'").SingleOrDefault();
            if (testid != null)
            {
                ModelState.AddModelError("TrungPN", "Mã phiếu nhập bị trùng");
            }
            else
            {
                PHIEUNHAPSP pHIEUNHAPSP = new PHIEUNHAPSP();
                if (ModelState.IsValid)
                {
                    //pHIEUNHAPSP.PN_ID = db.autottang("PHIEUNHAPSP", "PN_ID", db.PHIEUNHAPSPs.Count()).ToString();
                    pHIEUNHAPSP.PN_ID = id4;
                    pHIEUNHAPSP.NCC_ID = id;
                    pHIEUNHAPSP.PN_NGAY = DateTime.Now;
                    pHIEUNHAPSP.PN_GHICHU = id3;
                    //pHIEUNHAPSP.NV_ID = "";
                    db.PHIEUNHAPSPs.Add(pHIEUNHAPSP);

                    db.SaveChanges();

                    Session["PNSP"] = pHIEUNHAPSP.PN_ID;
                    ModelState.AddModelError("", "Phiếu nhập " + pHIEUNHAPSP.PN_ID + " Đã được tạo");

                }
                else
                {
                    ModelState.AddModelError("", "Lỗi !");
                }
            }
            return View();
        }

        // GET: PHIEUNHAPSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            if (pHIEUNHAPSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN", pHIEUNHAPSP.NCC_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", pHIEUNHAPSP.NV_ID);
            return View(pHIEUNHAPSP);
        }

        // POST: PHIEUNHAPSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PN_ID,NV_ID,NCC_ID,PN_NGAY,PN_GHICHU")] PHIEUNHAPSP pHIEUNHAPSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUNHAPSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NCC_ID = new SelectList(db.NHACUNGCAPs, "NCC_ID", "NCC_TEN", pHIEUNHAPSP.NCC_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", pHIEUNHAPSP.NV_ID);
            return View(pHIEUNHAPSP);
        }

        // GET: PHIEUNHAPSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            if (pHIEUNHAPSP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAPSP);
        }

        // POST: PHIEUNHAPSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PHIEUNHAPSP pHIEUNHAPSP = db.PHIEUNHAPSPs.Find(id);
            db.PHIEUNHAPSPs.Remove(pHIEUNHAPSP);
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
