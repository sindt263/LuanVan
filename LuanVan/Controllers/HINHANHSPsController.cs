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
    public class HINHANHSPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: HINHANHSPs
       
        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new HINHANHSPsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<HINHANHSP> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<HINHANHSP> model = db.HINHANHSPs.Include(h => h.SANPHAM);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.HA_ID.Contains(searchTerm) || x.SANPHAM.SP_TEN.Contains(searchTerm)|| x.SP_ID.ToString().Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.HA_ID).ToPagedList(page, pageSize);
        }
        // GET: HINHANHSPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            if (hINHANHSP == null)
            {
                return HttpNotFound();
            }
            return View(hINHANHSP);
        }

        // GET: HINHANHSPs/Create
        public ActionResult Create()
        {
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN");
            return View();
        }

        // POST: HINHANHSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HA_ID,CTSP_ID,HA_ND")] HINHANHSP hINHANHSP)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    Int32 length = file.ContentLength;
                    byte[] tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);
                    hINHANHSP.HA_ND = tempImage;
                    hINHANHSP.HA_ID = db.autottang("HinhAnhSP", "HA_ID", db.HINHANHSPs.Count()).ToString();
                }
                db.HINHANHSPs.Add(hINHANHSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", hINHANHSP.SP_ID);
            return View(hINHANHSP);
        }

        // GET: HINHANHSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            if (hINHANHSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", hINHANHSP.SP_ID);
            return View(hINHANHSP);
        }

        // POST: HINHANHSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HA_ID,CTSP_ID,HA_ND")] HINHANHSP hINHANHSP)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    Int32 length = file.ContentLength;
                    byte[] tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);
                    hINHANHSP.HA_ND = tempImage;
                }
                db.Entry(hINHANHSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", hINHANHSP.SP_ID);
            return View(hINHANHSP);
        }

        // GET: HINHANHSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            if (hINHANHSP == null)
            {
                return HttpNotFound();
            }
            return View(hINHANHSP);
        }

        // POST: HINHANHSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            db.HINHANHSPs.Remove(hINHANHSP);
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

        public void addHA(HINHANHSP hINHANHSP)
        {
            hINHANHSP.HA_ID = db.autottang("HinhAnhSP", "HA_ID", db.HINHANHSPs.Count()).ToString();
            db.HINHANHSPs.Add(hINHANHSP);
            db.SaveChanges();
        }

        public class HinhAnhResult : ActionResult
        {
            public byte[] NoiDungHinhAnh { get; set; }

            public HinhAnhResult(byte[] noidung)
            {
                NoiDungHinhAnh = noidung;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                var response = context.HttpContext.Response;
                response.Clear();
                response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (NoiDungHinhAnh != null)
                {
                    var stream = new MemoryStream(NoiDungHinhAnh);
                    stream.WriteTo(response.OutputStream);
                    stream.Dispose();
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult getImage(string id)
        {
            string strID = Request.QueryString["ID"];
            if (id != null)
            {
                //var ha = db.HinhAnhHoatDongs.Where(h => h.HD_IDHoatDong == ID).FirstOrDefault();
                var ha = (from p in db.HINHANHSPs where p.SP_ID == id  select p);
                foreach (var i in ha)
                {
                    if (i == null || i.HA_ND == null)
                    {
                        ModelState.AddModelError("", "Loi");
                    }
                    //Response.ContentType = "image/jpeg";
                    Response.OutputStream.Write(i.HA_ND.ToArray(), 0, i.HA_ND.Length);
                    Response.Flush();
                }
            }
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult getImageHA(string id)
        {
            string strID = Request.QueryString["ID"];
            if (id != null)
            {
                //var ha = db.HinhAnhHoatDongs.Where(h => h.HD_IDHoatDong == ID).FirstOrDefault();
                var ha = (from p in db.HINHANHSPs where p.HA_ID == id  select p);
                foreach (var i in ha)
                {
                    if (i == null || i.HA_ND == null)
                    {
                        ModelState.AddModelError("", "Loi");
                    }
                    //Response.ContentType = "image/jpeg";
                    Response.OutputStream.Write(i.HA_ND.ToArray(), 0, i.HA_ND.Length);
                    Response.Flush();
                }
            }
            return View();
        }


    }
}
