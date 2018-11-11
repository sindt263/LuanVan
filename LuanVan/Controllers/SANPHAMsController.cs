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
    public class SANPHAMsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: SANPHAMs
        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new SANPHAMsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<SANPHAM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<SANPHAM> model = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT); ;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.SP_ID.Contains(searchTerm) || x.SP_TEN.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.SP_ID).ToPagedList(page, pageSize);
        }
        // GET: SANPHAMs/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.BL = db.BINHLUANCTSPs.Where(a => a.SP_ID == id).Where(a => a.BL_TL == null).ToList();
            ViewBag.HAID = (from p in db.HINHANHSPs where p.SP_ID == id select p).Take(2);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.DSP_ID = new SelectList(db.DONGSANPHAMs, "DSP_ID", "DSP_TEN");
            ViewBag.GIA_ID = new SelectList(db.GIASPs, "GIA_ID", "GIA_ID");
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN");
            ViewBag.NSX_ID = new SelectList(db.NHASANXUATs, "NSX_ID", "NSX_TEN");
            return View();
        }

        // POST: SANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SP_ID,KM_ID,GIA_ID,NSX_ID,DSP_ID,SP_TEN,SP_CNMANGHINH,SP_DOPHANGIAI,SP_MANHINH,SP_CAMERASAU,SP_CAMERATRUOC,SP_HEDIEUHANH,SP_RAM,SP_ROM,SP_DUNGLUONGPIN,SP_SOSIM,SP_MOTA,SP_NGAYTAO,SP_THOIGIANBH")] SANPHAM sANPHAM)
        {
            HINHANHSPsController hINHANHSPsController = new HINHANHSPsController();
            HINHANHSP hINHANHSP = new HINHANHSP();
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                if (file != null)
                {

                    Int32 length = file.ContentLength;
                    byte[] tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);
                    hINHANHSP.HA_ND = tempImage;
                    hINHANHSP.HA_ID = db.autottang("HinhAnhSP", "HA_ID", db.HINHANHSPs.Count()).ToString();
                    //hINHANHSP.CTSP_ID = cHITIETSANPHAM.CTSP_ID;
                    hINHANHSPsController.addHA(hINHANHSP);
                }
                return RedirectToAction("Index");
            }

            ViewBag.DSP_ID = new SelectList(db.DONGSANPHAMs, "DSP_ID", "DSP_TEN", sANPHAM.DSP_ID);
            ViewBag.GIA_ID = new SelectList(db.GIASPs, "GIA_ID", "GIA_ID", sANPHAM.GIA_ID);
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", sANPHAM.KM_ID);
            ViewBag.NSX_ID = new SelectList(db.NHASANXUATs, "NSX_ID", "NSX_TEN", sANPHAM.NSX_ID);
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.DSP_ID = new SelectList(db.DONGSANPHAMs, "DSP_ID", "DSP_TEN", sANPHAM.DSP_ID);
            ViewBag.GIA_ID = new SelectList(db.GIASPs, "GIA_ID", "GIA_ID", sANPHAM.GIA_ID);
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", sANPHAM.KM_ID);
            ViewBag.NSX_ID = new SelectList(db.NHASANXUATs, "NSX_ID", "NSX_TEN", sANPHAM.NSX_ID);
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SP_ID,KM_ID,GIA_ID,NSX_ID,DSP_ID,SP_TEN,SP_CNMANGHINH,SP_DOPHANGIAI,SP_MANHINH,SP_CAMERASAU,SP_CAMERATRUOC,SP_HEDIEUHANH,SP_RAM,SP_ROM,SP_DUNGLUONGPIN,SP_SOSIM,SP_MOTA,SP_NGAYTAO,SP_THOIGIANBH")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DSP_ID = new SelectList(db.DONGSANPHAMs, "DSP_ID", "DSP_TEN", sANPHAM.DSP_ID);
            ViewBag.GIA_ID = new SelectList(db.GIASPs, "GIA_ID", "GIA_ID", sANPHAM.GIA_ID);
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", sANPHAM.KM_ID);
            ViewBag.NSX_ID = new SelectList(db.NHASANXUATs, "NSX_ID", "NSX_TEN", sANPHAM.NSX_ID);
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
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

        public int GetGia(string id)
        {
            string sp_id = db.Database.SqlQuery<string>("select SP_ID from SanPham where CTSP_ID ='" + id + "'").Take(1).SingleOrDefault();
            string gia_id = db.Database.SqlQuery<string>("select Gia_ID from SanPham where SP_ID ='" + sp_id + "'").Take(1).SingleOrDefault();
            int gia = db.Database.SqlQuery<int>("select Gia_GIA from GIASP where GIA_ID ='" + gia_id + "'").Take(1).SingleOrDefault();
            return gia;

        }
        public string GetSP_ID(string id)
        {
            string sp_id = db.Database.SqlQuery<string>("select SP_ID from SanPham where CTSP_ID ='" + id + "' and SP_TRANGTHAI = 1").Take(1).SingleOrDefault();
            return sp_id;

        }
        public string GetCTSP_TEN(string id)
        {
            string sp_id = db.Database.SqlQuery<string>("select CTSP_TEN from ChiTietSanPham where CTSP_ID ='" + id + "'").Take(1).SingleOrDefault();
            return sp_id;

        }

    }
}
