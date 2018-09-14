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

            return View(mode);
        }

        public IEnumerable<CHITIETSANPHAM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs.Include(s => s.SANPHAMs);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.CTSP_ID.Contains(searchTerm) || x.CTSP_TEN.Contains(searchTerm) || x.CTSP_MOTA.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.CTSP_ID).ToPagedList(page, pageSize);
        }
        // GET: CHITIETSANPHAMs/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.HAID = (from p in db.HINHANHSPs where p.CTSP_ID == id select p).Take(2);
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
        public ActionResult Create([Bind(Include = "CTSP_ID,CTSP_TEN,CTSP_CNMANGHINH,CTSP_DOPHANGIAI,CTSP_MANHINH,CTSP_CAMERATRUOC,CTSP_CAMERASAU,CTSP_HEDIEUHANH,CTSP_RAM,CTSP_ROM,CTSP_DUNGLUONGPIN,CTSP_SOSIM,CTSP_MOTA")] CHITIETSANPHAM cHITIETSANPHAM)
        {
            HINHANHSPsController hINHANHSPsController = new HINHANHSPsController();
            HINHANHSP hINHANHSP = new HINHANHSP();
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                cHITIETSANPHAM.CTSP_ID = db.autottang("ChiTietSanPham", "CTSP_ID", db.CHITIETSANPHAMs.Count()).ToString();
                db.CHITIETSANPHAMs.Add(cHITIETSANPHAM);
                db.SaveChanges();
                if (file != null)
                {

                    Int32 length = file.ContentLength;
                    byte[] tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);
                    hINHANHSP.HA_ND = tempImage;
                    hINHANHSP.HA_ID = db.autottang("HinhAnhSP", "HA_ID", db.HINHANHSPs.Count()).ToString();
                    hINHANHSP.CTSP_ID = cHITIETSANPHAM.CTSP_ID;
                    hINHANHSPsController.addHA(hINHANHSP);
                }

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
        public ActionResult Edit([Bind(Include = "CTSP_ID,CTSP_TEN,CTSP_CNMANGHINH,CTSP_DOPHANGIAI,CTSP_MANHINH,CTSP_CAMERATRUOC,CTSP_CAMERASAU,CTSP_HEDIEUHANH,CTSP_RAM,CTSP_ROM,CTSP_DUNGLUONGPIN,CTSP_SOSIM,CTSP_MOTA")] CHITIETSANPHAM cHITIETSANPHAM)
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

        public int GetGia(string id)
        {
            string sp_id = db.Database.SqlQuery<string>("select SP_ID from SanPham where CTSP_ID ='" + id + "'").Take(1).SingleOrDefault();
            string gia_id = db.Database.SqlQuery<string>("select Gia_ID from SanPham where SP_ID ='" + sp_id + "'").Take(1).SingleOrDefault();
            int gia = db.Database.SqlQuery<int>("select Gia_GIA from GIASP where GIA_ID ='" + gia_id + "'").Take(1).SingleOrDefault();
            return gia;
            
        } public string GetSP_ID(string id)
        {
            string sp_id = db.Database.SqlQuery<string>("select SP_ID from SanPham where CTSP_ID ='" + id + "' and SP_TRANGTHAI = 1").Take(1).SingleOrDefault();
            return sp_id;
            
        }
    }
}
