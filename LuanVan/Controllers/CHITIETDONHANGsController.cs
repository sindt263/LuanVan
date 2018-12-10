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
            IQueryable<CHITIETDONHANG> model = db.CHITIETDONHANGs.Include(c => c.DONHANG).Include(c => c.CHITIETSANPHAM);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.DONHANG.DN_ID.ToString().Contains(searchTerm) );

            }

            return model.OrderByDescending(x => x.DONHANG.DN_NGALAPDON).ToPagedList(page, pageSize);
        }

        public ActionResult IndexKH(string id)
        {
           if(id == null)
            {
                return RedirectToAction("","Home");
            }
            ViewBag.DH_ID = id;
            int DN_ID = Convert.ToInt32(id);
            var cHITIETDONHANGs = from p in db.CHITIETDONHANGs where p.DN_ID == DN_ID select p;
           
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
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN");
            return View();
        }

        // POST: CHITIETDONHANGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTDH_ID,DN_ID,CTSP_ID,CTDH_DIACHIGIAO")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                cHITIETDONHANG.CTDH_ID = db.autottang("ChiTietDonHang", "CTDH_ID", db.CHITIETDONHANGs.Count()).ToString();
                db.CHITIETDONHANGs.Add(cHITIETDONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DN_ID = new SelectList(db.DONHANGs, "DN_ID", "KH_ID", cHITIETDONHANG.DN_ID);
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", cHITIETDONHANG.CTSP_ID);
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
            ViewBag.DN_ID = new SelectList(db.DONHANGs, "DN_ID", "DN_ID", cHITIETDONHANG.DN_ID);
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", cHITIETDONHANG.CTSP_ID);
            return View(cHITIETDONHANG);
        }

        // POST: CHITIETDONHANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTDH_ID,DN_ID,CTSP_ID,CTDH_DIACHIGIAO")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETDONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DN_ID = new SelectList(db.DONHANGs, "DN_ID", "DN_ID", cHITIETDONHANG.DN_ID);
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", cHITIETDONHANG.CTSP_ID);
            return View(cHITIETDONHANG);
        }

        // GET: CHITIETDONHANGs/Delete/5
        public ActionResult Delete(string id,string id2)
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
            db.Database.ExecuteSqlCommand("delete from ChiTietDonHang where CTDH_ID ='" + id + "' and CTSP_ID ='" + id2 + "'");
            db.Database.ExecuteSqlCommand("update ChiTietSanPham set CTSP_TRANGTHAI = 1 where CTSP_ID = '" + id2 + "'");
            return RedirectToAction("Index", "DonHangs");
        }

        // POST: CHITIETDONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            
            db.CHITIETDONHANGs.Remove(cHITIETDONHANG);
            db.SaveChanges();
            return RedirectToAction("Index","DonHang");
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
            CHITIETSANPHAM cHITIETSANPHAM = db.CHITIETSANPHAMs.FirstOrDefault(sp => sp.CTSP_ID == id);
            if (cHITIETSANPHAM != null)
            {
                cHITIETSANPHAM.CTSP_TRANGTHAI = 1;
                db.SaveChanges();
            }
           
        }
    }
}
