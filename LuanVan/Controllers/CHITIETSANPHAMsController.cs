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
    public class CHITIETSANPHAMsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CHITIETSANPHAMs
        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new CHITIETSANPHAMsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;
            //var sANPHAMs = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM).Include(n => n.CHITIETNHAPs);
            return View(mode);
        }

        public IEnumerable<CHITIETSANPHAM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs.Include(c => c.SANPHAM);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.CTSP_ID.Contains(searchTerm) || x.CTSP_TEN.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.CTSP_ID).ToPagedList(page, pageSize);
        }


        public IEnumerable<CHITIETSANPHAM> ListAllPaging1(string searchTerm, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.CTSP_ID.Contains(searchTerm) || x.CTSP_TEN.Contains(searchTerm) || x.SANPHAM.SP_MOTA.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.CTSP_ID).ToPagedList(page, pageSize);
        }
        public ActionResult ViewSP(string searchTerm, int page = 1, int pageSize = 100)
        {
            return RedirectToAction("", "");
            //ViewBag.nsx = db.NHASANXUATs.ToList();

            //var SanPhams = new SANPHAMsController();
            //var mode = SanPhams.ListAllPaging1(searchTerm, page, pageSize);
            //ViewBag.SearchTerm = searchTerm;

            //foreach (var i in ViewBag.nsx)
            //{
            //    string id = i.NSX_ID;

            //    ViewData[i.NSX_TEN] = (from p in db.SANPHAMs where p.NSX_ID == id select p.CTSP_ID).Distinct();
            //    foreach(var ii in ViewData[i.NSX_TEN])
            //    {
            //        string ctsp_id = ii;
            //        ViewData[ii] = (from p in db.CHITIETSANPHAMs where p.CTSP_ID == ctsp_id select p);
            //    }

            //}

            //ViewBag.KM = (from p in db.KHUYENMAIs where p.KM_NGAYKETTHUC >= DateTime.Now && p.KM_ID != "0" select p).OrderByDescending(a => a.KM_NGAYBATDAU);

            //var PN_ID = (from p in db.PHIEUNHAPSPs select p).OrderByDescending(a => a.PN_NGAY);
            //string SP_ID = "";
            //foreach (var i in PN_ID)
            //{
            //    SP_ID += db.Database.SqlQuery<string>("select SP_ID from ChiTietNhap where PN_ID ='"+i.PN_ID+"'").DefaultIfEmpty();
            //}
            //ViewBag.MaSP = SP_ID;

            ////var sANPHAMs = db.CHITIETSANPHAMs.ToList();
            //return View(mode);
        }


        public ViewResult SPbyNSX(string id)
        {
            ViewBag.NSX_ID = id;
            var model = (from nsx in db.NHASANXUATs join sp in db.SANPHAMs on nsx.NSX_ID equals sp.NSX_ID join ctsp in db.CHITIETSANPHAMs on sp.SP_ID equals ctsp.CTSP_ID where nsx.NSX_ID == id select ctsp).Distinct();

            return View(model);
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
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN");
            ViewBag.PN_ID = new SelectList(db.PHIEUNHAPSPs, "PN_ID", "PN_ID");
            return View();
        }

        // POST: CHITIETSANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTSP_ID,SP_ID,CTSP_TRANGTHAI,CTSP_TEN")] CHITIETSANPHAM cHITIETSANPHAM)
        {
            HINHANHSPsController hINHANHSPsController = new HINHANHSPsController();
            HINHANHSP hINHANHSP = new HINHANHSP();
            HttpPostedFileBase file = Request.Files["Image"];
            if (file != null)
            {
                cHITIETSANPHAM.CTSP_TRANGTHAI = 1;
                Int32 length = file.ContentLength;
                byte[] tempImage = new byte[length];
                file.InputStream.Read(tempImage, 0, length);
                hINHANHSP.HA_ND = tempImage;
                hINHANHSP.HA_ID = db.autottang("HinhAnhSP", "HA_ID", db.HINHANHSPs.Count()).ToString();
                hINHANHSPsController.addHA(hINHANHSP);
            }
            if (ModelState.IsValid)
            {
                db.CHITIETSANPHAMs.Add(cHITIETSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", cHITIETSANPHAM.SP_ID);
            return View(cHITIETSANPHAM);
        }


        public ActionResult CreateSP(string CTSP_ID, string SP_ID, string CTSP_TEN,string PN_ID, string CTN_GIA)
        {
            CHITIETSANPHAM sANPHAM = new CHITIETSANPHAM();
            CHITIETNHAP cHITIETNHAP = new CHITIETNHAP();
            CHITIETNHAPsController cHITIETNHAPs = new CHITIETNHAPsController();
            string a = db.KiemTraID("ChiTietSanPham", "CTSP_ID", CTSP_ID);
            if (!string.IsNullOrEmpty(a))
            {
                ModelState.AddModelError("", "Mã sản phẩm đã tồn tại !");
            }else
            if (ModelState.IsValid)
            {
                
                cHITIETNHAP.CTN_ID = db.autottang("CHITIETNHAP", "CTN_ID", db.CHITIETNHAPs.Count()).ToString();
                //cHITIETNHAP.PN_ID = Session["PNSP"].ToString();
                cHITIETNHAP.PN_ID = PN_ID;
                cHITIETNHAP.CTSP_ID = CTSP_ID;
                cHITIETNHAP.CTN_GIA = Convert.ToInt32(CTN_GIA);
                db.CHITIETNHAPs.Add(cHITIETNHAP);
                sANPHAM.CTSP_ID = CTSP_ID;
                sANPHAM.SP_ID = SP_ID;
                sANPHAM.CTSP_TEN = CTSP_TEN;
                sANPHAM.CTSP_TRANGTHAI = 1;
                db.CHITIETSANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                ModelState.AddModelError("", "Đã thêm sản phẩm " + sANPHAM.CTSP_ID);
            }


            return View(sANPHAM);
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
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_ID", cHITIETSANPHAM.SP_ID);
            return View(cHITIETSANPHAM);
        }

        // POST: CHITIETSANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTSP_ID,SP_ID,CTSP_TRANGTHAI,CTSP_TEN")] CHITIETSANPHAM cHITIETSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_ID", cHITIETSANPHAM.SP_ID);
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
                //sANPHAM.SP_TRANGTHAI = 0;
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

        public short KTKho(string id)
        {
            short TTSP = db.Database.SqlQuery<short>("select CTSP_TRANGTHAI from ChiTietSanPham where SP_ID ='" + id + "' and CTSP_TRANGTHAI =1").FirstOrDefault();
            if (TTSP == 1)
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
