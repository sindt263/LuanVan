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
    public class DONHANGsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: DONHANGs
        public ActionResult Index()
        {
            var dONHANGs = db.DONHANGs.Include(d => d.HINHTHUCTHANHTOAN).Include(d => d.KHACHHANG).Include(d => d.TRANGTHAIDONHANG);
            return View(dONHANGs.ToList());
        }

        // GET: DONHANGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // GET: DONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN");
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN");
            ViewBag.TTDH_ID = new SelectList(db.TRANGTHAIDONHANGs, "TTDH_ID", "TTDH_TEN");
            return View();
        }

        // POST: DONHANGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DN_ID,TTDH_ID,KH_ID,HTTT_ID,DN_NGALAPDON,DN_GHICHU,DN_SL")] DONHANG dONHANG)
        {
            string KH_ID = Request["KH_ID"];
            string result = db.Database.SqlQuery<string>("select KH_ID from KhachHang where KH_ID='" + KH_ID + "'").SingleOrDefault();

            if (result != null)
            {
                if (ModelState.IsValid)
                {
                    dONHANG.DN_ID = db.autottang("DonHang", "DN_ID", db.DONHANGs.Count()).ToString();
                    db.DONHANGs.Add(dONHANG);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("KH_ID", "Khách hàng không tồn tại");
            }

            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN", dONHANG.HTTT_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", dONHANG.KH_ID);
            ViewBag.TTDH_ID = new SelectList(db.TRANGTHAIDONHANGs, "TTDH_ID", "TTDH_TEN", dONHANG.TTDH_ID);
            return View(dONHANG);
        }

        // GET: DONHANGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN", dONHANG.HTTT_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", dONHANG.KH_ID);
            ViewBag.TTDH_ID = new SelectList(db.TRANGTHAIDONHANGs, "TTDH_ID", "TTDH_TEN", dONHANG.TTDH_ID);
            return View(dONHANG);
        }

        // POST: DONHANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DN_ID,TTDH_ID,KH_ID,HTTT_ID,DN_NGALAPDON,DN_GHICHU,DN_SL")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN", dONHANG.HTTT_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", dONHANG.KH_ID);
            ViewBag.TTDH_ID = new SelectList(db.TRANGTHAIDONHANGs, "TTDH_ID", "TTDH_TEN", dONHANG.TTDH_ID);
            return View(dONHANG);
        }

        // GET: DONHANGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // POST: DONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DONHANG dONHANG = db.DONHANGs.Find(id);
            db.DONHANGs.Remove(dONHANG);
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

        public JsonResult KTKH(string id)
        {
            string result = db.Database.SqlQuery<string>("select KH_ID from KhachHang where KH_ID='" + id + "'").SingleOrDefault();
            if(result != null)
            {
                string output = "Đã xác định khách hàng " +id;
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string output = "Khánh hàng " + id + " không tồn tại";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
