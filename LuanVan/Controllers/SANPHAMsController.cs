using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using LuanVan.Controllers;
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
            //var sANPHAMs = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM).Include(n => n.CHITIETNHAPs);
            return View(mode);
        }
      
        public IEnumerable<SANPHAM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<SANPHAM> model = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM).Include(n => n.CHITIETNHAPs);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.DONGSANPHAM.DSP_TEN.Contains(searchTerm) || x.SP_TEN.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.SP_TRANGTHAI).ToPagedList(page, pageSize);
        }


        public IEnumerable<CHITIETSANPHAM> ListAllPaging1(string searchTerm, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs.Include(s => s.SANPHAMs);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.CTSP_ID.Contains(searchTerm) || x.CTSP_TEN.Contains(searchTerm) || x.CTSP_MOTA.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.CTSP_ID).ToPagedList(page, pageSize);
        }
        public ActionResult ViewSP(string searchTerm, int page = 1, int pageSize =100)
        {
            //var Model = db.NHASANXUATs.ToList();
            ViewBag.nsx = db.NHASANXUATs.ToList();

            var SanPhams = new SANPHAMsController();
            var mode = SanPhams.ListAllPaging1(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            foreach (var i in ViewBag.nsx)
            {
                string id = i.NSX_ID;
               
                ViewData[i.NSX_TEN] = (from p in db.SANPHAMs where p.NSX_ID == id select p.CTSP_ID).Distinct();
                foreach(var ii in ViewData[i.NSX_TEN])
                {
                    string ctsp_id = ii;
                    ViewData[ii] = (from p in db.CHITIETSANPHAMs where p.CTSP_ID == ctsp_id select p);
                }
                
            }
           
            ViewBag.KM = (from p in db.KHUYENMAIs where p.KM_NGAYKETTHUC >= DateTime.Now && p.KM_ID != "0" select p).OrderByDescending(a => a.KM_NGAYBATDAU);
            
            var PN_ID = (from p in db.PHIEUNHAPSPs select p).OrderByDescending(a => a.PN_NGAY);
            string SP_ID = "";
            foreach (var i in PN_ID)
            {
                SP_ID += db.Database.SqlQuery<string>("select SP_ID from ChiTietNhap where PN_ID ='"+i.PN_ID+"'").DefaultIfEmpty();
            }
            ViewBag.MaSP = SP_ID;

            //var sANPHAMs = db.CHITIETSANPHAMs.ToList();
            return View(mode);
        }

        public ViewResult SPbyNSX(string id)
        {
            ViewBag.NSX_ID = id;
            var model = (from nsx in db.NHASANXUATs join sp in db.SANPHAMs on nsx.NSX_ID equals sp.NSX_ID join ctsp in db.CHITIETSANPHAMs on sp.CTSP_ID equals ctsp.CTSP_ID where nsx.NSX_ID == id select ctsp.CTSP_ID).Distinct();
            
            return View(model);
        }
        // GET: SANPHAMs/Details/5
        public ActionResult Details(string id)
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

        // GET: SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.DSP_ID = new SelectList(db.DONGSANPHAMs, "DSP_ID", "DSP_TEN");
            ViewBag.GIA_ID = new SelectList(db.GIASPs, "GIA_ID", "GIA_ID");
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN");
            ViewBag.NSX_ID = new SelectList(db.NHASANXUATs, "NSX_ID", "NSX_TEN");
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN");
            ViewBag.NSP_ID = new SelectList(db.NHOMSANPHAMs, "NSP_ID", "NSP_TEN");
            return View();
        }

        // POST: SANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SP_ID,CTSP_ID,NSP_ID,KM_ID,GIA_ID,DSP_ID,NSX_ID,SP_TEN,SP_TRANGTHAI")] SANPHAM sANPHAM)
        {
            HINHANHSPsController hINHANHSPsController = new HINHANHSPsController();
            HINHANHSP hINHANHSP = new HINHANHSP();
            string CTSP = Request["CTSP_ID"];
            HttpPostedFileBase file = Request.Files["Image"];
            if (file != null)
            {
                sANPHAM.CTSP_ID = CTSP;
                sANPHAM.SP_TRANGTHAI = 1;
                Int32 length = file.ContentLength;
                byte[] tempImage = new byte[length];
                file.InputStream.Read(tempImage, 0, length);
                hINHANHSP.HA_ND = tempImage;
                hINHANHSP.HA_ID = sANPHAM.SP_ID;
                hINHANHSPsController.addHA(hINHANHSP);
            }
            if (ModelState.IsValid)
            {

                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                ModelState.AddModelError("", "Đã thêm sản phẩm " + CTSP);
            }

            ViewBag.DSP_ID = new SelectList(db.DONGSANPHAMs, "DSP_ID", "DSP_TEN", sANPHAM.DSP_ID);
            ViewBag.GIA_ID = new SelectList(db.GIASPs, "GIA_ID", "GIA_ID", sANPHAM.GIA_ID);
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", sANPHAM.KM_ID);
            ViewBag.NSX_ID = new SelectList(db.NHASANXUATs, "NSX_ID", "NSX_TEN", sANPHAM.NSX_ID);
            ViewBag.NSP_ID = new SelectList(db.NHOMSANPHAMs, "NSP_ID", "NSP_TEN", sANPHAM.NSP_ID);
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", sANPHAM.CTSP_ID);
            return View(sANPHAM);
        }
        public ActionResult CreateSP(string id1, string id2, string id3, string id4, string id5, string id6, string id7, string id10)
        {
            SANPHAM sANPHAM = new SANPHAM();

            if (ModelState.IsValid)
            {
                sANPHAM.SP_ID = id1;
                sANPHAM.NSP_ID = id2;
                sANPHAM.KM_ID = id3;
                sANPHAM.GIA_ID = id4;
                sANPHAM.DSP_ID = id5;
                sANPHAM.NSX_ID = id6;
                sANPHAM.SP_TEN = id7;
                sANPHAM.CTSP_ID = id10;
                sANPHAM.SP_TRANGTHAI = 1;
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                ModelState.AddModelError("", "Đã thêm sản phẩm " + sANPHAM.SP_ID);
            }


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
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", sANPHAM.CTSP_ID);
            ViewBag.NSP_ID = new SelectList(db.NHOMSANPHAMs, "NSP_ID", "NSP_TEN", sANPHAM.NSP_ID);
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SP_ID,CTSP_ID,NSP_ID,KM_ID,GIA_ID,DSP_ID,NSX_ID,SP_TEN,SP_TRANGTHAI")] SANPHAM sANPHAM)
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
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", sANPHAM.CTSP_ID);
            ViewBag.NSP_ID = new SelectList(db.NHOMSANPHAMs, "NSP_ID", "NSP_TEN", sANPHAM.NSP_ID);
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

        public void addSP(SANPHAM sANPHAM)
        {
            db.SANPHAMs.Add(sANPHAM);
            db.SaveChanges();
        }



        public int GetGia(string id)
        {
            int gia = db.Database.SqlQuery<int>("select Gia_Gia from GiaSP where Gia_ID ='" + id + "'").SingleOrDefault();
            return gia;
        }

        public RedirectToRouteResult ChuyenTrangThaiSPDaDuocDac(string id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(m => m.SP_ID == id);
            if (sANPHAM != null)
            {
                sANPHAM.SP_TRANGTHAI = 0;
            }
            return RedirectToAction("");
        }

        public string GetTenSP(string id)
        {
            string tensp = db.Database.SqlQuery<string>("select SP_TEN from SanPham where SP_ID ='" + id + "'").SingleOrDefault();
            return tensp;
        }
        public string GetCTSP(string id)
        {
            string tensp = db.Database.SqlQuery<string>("select CTSP_ID from SanPham where SP_ID ='" + id + "'").SingleOrDefault();
            return tensp;
        }
        public string GetCTSP_TEN(string id)
        {
            string tensp = db.Database.SqlQuery<string>("select CTSP_TEN from ChiTietSanPham where CTSP_ID ='" + id + "'").SingleOrDefault();
            return tensp;
        }

        public short KTKho (string id)
        {
            short TTSP = db.Database.SqlQuery<short>("select SP_TRANGTHAI from sanpham where CTSP_ID ='"+id+"' and SP_TRANGTHAI =1").Take(1).SingleOrDefault();
            if (TTSP ==1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
           
        }
        
    }
}
