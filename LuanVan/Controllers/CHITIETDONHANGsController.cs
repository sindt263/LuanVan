using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using PagedList;
using PagedList.Mvc;

namespace LuanVan.Controllers
{
    public class CHITIETDONHANGsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CHITIETDONHANGs
       

        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new CHITIETDONHANGsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<CHITIETDONHANG> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<CHITIETDONHANG> model = db.CHITIETDONHANGs.Include(c => c.DONHANG).Include(c => c.SANPHAM);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.DONHANG.DN_ID.Contains(searchTerm) || x.CTDH_ID.ToString().Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.DONHANG.DN_NGALAPDON).ToPagedList(page, pageSize);
        }
        public ActionResult IndexKH(string id)
        {
            ViewBag.DH_ID = id;
            var cHITIETDONHANGs = from p in db.CHITIETDONHANGs where p.DN_ID == id select p;
            return View(cHITIETDONHANGs.ToList());
        }

        // GET: CHITIETDONHANGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // GET: CHITIETDONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.DN_ID = new SelectList(db.DONHANGs, "DN_ID", "KH_ID");
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN");
            return View();
        }

        // POST: CHITIETDONHANGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTDH_ID,DN_ID,SP_ID,CTDH_DIACHIGIAO")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                cHITIETDONHANG.CTDH_ID = db.autottang("ChiTietDonHang", "CTDH_ID", db.CHITIETDONHANGs.Count()).ToString();
                db.CHITIETDONHANGs.Add(cHITIETDONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DN_ID = new SelectList(db.DONHANGs, "DN_ID", "KH_ID", cHITIETDONHANG.DN_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", cHITIETDONHANG.SP_ID);
            return View(cHITIETDONHANG);
        }

        // GET: CHITIETDONHANGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.DN_ID = new SelectList(db.DONHANGs, "DN_ID", "KH_ID", cHITIETDONHANG.DN_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", cHITIETDONHANG.SP_ID);
            return View(cHITIETDONHANG);
        }

        // POST: CHITIETDONHANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTDH_ID,DN_ID,SP_ID,CTDH_DIACHIGIAO")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETDONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DN_ID = new SelectList(db.DONHANGs, "DN_ID", "KH_ID", cHITIETDONHANG.DN_ID);
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", cHITIETDONHANG.SP_ID);
            return View(cHITIETDONHANG);
        }

        // GET: CHITIETDONHANGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // POST: CHITIETDONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            db.CHITIETDONHANGs.Remove(cHITIETDONHANG);
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

        public void EditHuy(string id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(sp => sp.SP_ID == id);
            if (sANPHAM != null)
            {
                sANPHAM.SP_TRANGTHAI = 1;
                db.SaveChanges();
            }
           
        }
    }
}
