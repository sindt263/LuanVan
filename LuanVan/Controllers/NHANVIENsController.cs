using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using PagedList;
using PagedList.Mvc;

namespace LuanVan.Controllers
{
    public class NHANVIENsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: NHANVIENs
       

        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new NHANVIENsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<NHANVIEN> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<NHANVIEN> model = db.NHANVIENs.Include(n => n.LOAINV);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.NV_ID.Contains(searchTerm) || x.NV_TEN.Contains(searchTerm) || x.NV_SDT.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.NV_ID).ToPagedList(page, pageSize);
        }
        // GET: NHANVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // GET: NHANVIENs/Create
        public ActionResult Create()
        {
            ViewBag.LNV_ID = new SelectList(db.LOAINVs, "LNV_ID", "LNV_TEN");
            return View();
        }

        // POST: NHANVIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NV_ID,LNV_ID,NV_TEN,NV_NGAYSINH,NV_QUEQUAN,NV_DIACHI,NV_GIOITINH,NV_NGAYKYHOPDONG,NV_NGAYKETTHUCHOPDONG,NV_EMAIL,NV_MATKHAU,NV_TAIKHOAN,NV_SDT")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.NHANVIENs.Add(nHANVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LNV_ID = new SelectList(db.LOAINVs, "LNV_ID", "LNV_TEN", nHANVIEN.LNV_ID);
            return View(nHANVIEN);
        }
         public ActionResult ThongBao()
        {
            return View();
        }

        // POST: NHANVIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThongBao(string ThongBao)
        {
            // Luu: 
            string path = Server.MapPath("~/ThongBao.txt");
            StreamReader r = new StreamReader(path);
            string a = r.ReadToEnd();
            r.Close();

            StreamWriter w = new StreamWriter(path, false);
            //true = ghi tiep vao file, false = ghi de le du lieu cu, neu file chua ton tai se dc tao ra, file test.txt cung cấp thư mục, nếu ko phai chi rõ duong dẫn
            
            w.WriteLine(ThongBao.ToString() +" lúc: "+ DateTime.Now +" Bởi: "+Session["NV_TEN"]);
            w.WriteLine(a);
            //Doc: 
            //string s = "";
            //StreamReader r = new StreamReader(@"S:\ThongBao.txt");
            //while ((s = r.ReadLine()) != null)
            //{
                
            //    Session["ThongBao"] = s;
            //}
            w.Close();
            return View();
        }

        
        // GET: NHANVIENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.LNV_ID = new SelectList(db.LOAINVs, "LNV_ID", "LNV_TEN", nHANVIEN.LNV_ID);
            return View(nHANVIEN);
        }

        // POST: NHANVIENs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NV_ID,LNV_ID,NV_TEN,NV_NGAYSINH,NV_QUEQUAN,NV_DIACHI,NV_GIOITINH,NV_NGAYKYHOPDONG,NV_NGAYKETTHUCHOPDONG,NV_EMAIL,NV_MATKHAU,NV_TAIKHOAN,NV_SDT")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LNV_ID = new SelectList(db.LOAINVs, "LNV_ID", "LNV_TEN", nHANVIEN.LNV_ID);
            return View(nHANVIEN);
        }

        // GET: NHANVIENs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: NHANVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            db.NHANVIENs.Remove(nHANVIEN);
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
