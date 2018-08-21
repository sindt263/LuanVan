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
    public class SANPHAMsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: SANPHAMs
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM);
            return View(sANPHAMs.ToList());
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
            ViewBag.NSP_ID = new SelectList(db.NHOMSANPHAMs, "NSP_ID", "NSP_TEN");
            return View();
        }

        // POST: SANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SP_ID,NSP_ID,KM_ID,GIA_ID,DSP_ID,NSX_ID,SP_TEN,SP_TRANGTHAI,SP_MOTANGAN,SP_MOTACHITIET")] SANPHAM sANPHAM)
        {
            HINHANHSPsController hINHANHSPsController = new HINHANHSPsController();
            HINHANHSP hINHANHSP = new HINHANHSP();
            HttpPostedFileBase file = Request.Files["Image"];
            if (file != null)
            {
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
                ModelState.AddModelError("", "Đã thêm sản phẩm " + sANPHAM.SP_ID);
            }

            ViewBag.DSP_ID = new SelectList(db.DONGSANPHAMs, "DSP_ID", "DSP_TEN", sANPHAM.DSP_ID);
            ViewBag.GIA_ID = new SelectList(db.GIASPs, "GIA_ID", "GIA_ID", sANPHAM.GIA_ID);
            ViewBag.KM_ID = new SelectList(db.KHUYENMAIs, "KM_ID", "KM_TEN", sANPHAM.KM_ID);
            ViewBag.NSX_ID = new SelectList(db.NHASANXUATs, "NSX_ID", "NSX_TEN", sANPHAM.NSX_ID);
            ViewBag.NSP_ID = new SelectList(db.NHOMSANPHAMs, "NSP_ID", "NSP_TEN", sANPHAM.NSP_ID);
            return View(sANPHAM);
        }
        public ActionResult CreateSP(string id1,string id2, string id3, string id4, string id5, string id6, string id7, string id8, string id9)
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
                sANPHAM.SP_MOTANGAN = id8;
                sANPHAM.SP_MOTACHITIET = id9;

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
            ViewBag.NSP_ID = new SelectList(db.NHOMSANPHAMs, "NSP_ID", "NSP_TEN", sANPHAM.NSP_ID);
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SP_ID,NSP_ID,KM_ID,GIA_ID,DSP_ID,NSX_ID,SP_TEN,SP_TRANGTHAI,SP_MOTANGAN,SP_MOTACHITIET")] SANPHAM sANPHAM)
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

        public string getIDHA(string IDSP)
        {
            var result = (from p in db.HINHANHSPs where p.SP_ID == IDSP select p).Take(1);
            foreach(var i in result)
            {
                string id = i.HA_ID;
                if (id != null)
                {
                    return id;
                }
            }
            return "";
        }

        public ActionResult getImage(string id)
        {
            string strID = Request.QueryString["ID"];
            if (id != null)
            {
                //var ha = db.HinhAnhHoatDongs.Where(h => h.HD_IDHoatDong == ID).FirstOrDefault();
                var ha = (from p in db.HINHANHSPs where p.SP_ID == id select p);
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
