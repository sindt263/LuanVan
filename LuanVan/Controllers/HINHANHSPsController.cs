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

namespace LuanVan.Controllers
{
    public class HINHANHSPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: HINHANHSPs
        public ActionResult Index()
        {
            var hINHANHSPs = db.HINHANHSPs.Include(h => h.SANPHAM);
            return View(hINHANHSPs.ToList());
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
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_ID");
            return View();
        }

        // POST: HINHANHSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HA_ID,SP_ID,HA_ND")] HINHANHSP hINHANHSP)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                hINHANHSP.HA_ID = db.autottang("HINHANHSP", "HA_ID", db.HINHANHSPs.Count()).ToString();
                Int32 length = file.ContentLength;
                byte[] tempImage = new byte[length];
                file.InputStream.Read(tempImage, 0, length);
                db.HINHANHSPs.Add(hINHANHSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_ID", hINHANHSP.SP_ID);
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
            //ViewBag.HA_ID = new SelectList(db.HINHANHSPs, "HA_ID", "HA_ID", hINHANHSP.SP_ID);
            return View(hINHANHSP);
        }

        // POST: HINHANHSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HA_ID,SP_ID,HA_ND")] HINHANHSP hINHANHSP)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            if (ModelState.IsValid)
            {
                Int32 length = file.ContentLength;
                byte[] tempImage = new byte[length];
                file.InputStream.Read(tempImage, 0, length);
                hINHANHSP.HA_ND = tempImage;
                db.Entry(hINHANHSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_ID", hINHANHSP.SP_ID);
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
            if (id!=null)
            {
                //var ha = db.HinhAnhHoatDongs.Where(h => h.HD_IDHoatDong == ID).FirstOrDefault();
                var ha = (from p in db.HINHANHSPs where p.HA_ID == id select p).Take(1);
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
