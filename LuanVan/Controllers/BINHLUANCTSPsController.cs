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
    public class BINHLUANCTSPsController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult IndexKH(string id)
        {
            return View(db.BINHLUANCTSPs.Where(a => a.SP_ID == id).Where(a=>a.BL_TL == null).OrderByDescending(a=>a.BL_THOIGIAN).ToList());
        }

        public ActionResult CreateKH(string id, string id2)
        {
            BINHLUANCTSP bINHLUANCTSP = new BINHLUANCTSP();
            if (ModelState.IsValid)
            {
                bINHLUANCTSP.BL_ID = db.autottang("BinhLuanCTSP", "BL_ID", db.BINHLUANCTSPs.Count()).ToString();
                bINHLUANCTSP.BL_ND = id;
                bINHLUANCTSP.SP_ID = id2;
                bINHLUANCTSP.BL_THOIGIAN = DateTime.Now;
                if(Session["KH_ID"] != null)
                {
                    bINHLUANCTSP.KH_ID = Session["KH_ID"].ToString();
                    //bINHLUANCTSP.NV_ID = "0";
                }
                if (Session["NV_ID"] != null)
                {
                    bINHLUANCTSP.NV_ID = Session["NV_ID"].ToString();
                    //bINHLUANCTSP.KH_ID = "0";
                }
               
                db.BINHLUANCTSPs.Add(bINHLUANCTSP);
                db.SaveChanges();
                return RedirectToAction("IndexKH/" + id2);
            }

            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs
        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var BL = new BINHLUANCTSPsController();
            var mode = BL.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;
           
            return View(mode);
            
        }

        public IEnumerable<BINHLUANCTSP> ListAllPaging(string searchTerm, int page, int pageSize)
        {

            IQueryable<BINHLUANCTSP> model = db.BINHLUANCTSPs.Include(b => b.SANPHAM).Include(b => b.KHACHHANG).Include(b => b.NHANVIEN);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.KH_ID.Contains(searchTerm) || x.BL_ND.Contains(searchTerm) || x.SANPHAM.SP_TEN.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.BL_THOIGIAN).ToPagedList(page, pageSize);
        }
        // GET: BINHLUANCTSPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Create
        public ActionResult Create()
        {
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN");
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN");
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN");
            return View();
        }

        // POST: BINHLUANCTSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id2,string id3,[Bind(Include = "BL_ID,SP_ID,NV_ID,KH_ID,BL_ND,BL_THOIGIAN,BL_TL")] BINHLUANCTSP bINHLUANCTSP)
        {
            if (ModelState.IsValid)
            {
                bINHLUANCTSP.BL_ID = db.autottang("BinhLuanCTSP", "BL_ID", db.BINHLUANCTSPs.Count()).ToString();
                if (Session["NV_ID"] != null)
                {
                    bINHLUANCTSP.NV_ID = Session["NV_ID"].ToString();
                }
                else
                {
                    bINHLUANCTSP.NV_ID = "0";
                }
                if (Session["KH_ID"] != null)
                {
                    bINHLUANCTSP.KH_ID = Session["KH_ID"].ToString();

                }
                else
                {
                    bINHLUANCTSP.KH_ID = "0";
                }

                //bINHLUANCTSP.KH_ID = id;
                bINHLUANCTSP.SP_ID = id2;
                bINHLUANCTSP.BL_TL = id3;
                bINHLUANCTSP.BL_THOIGIAN = DateTime.Now;
                db.BINHLUANCTSPs.Add(bINHLUANCTSP);
                db.SaveChanges();
                return RedirectToAction("details/"+id2, "SanPhams");
            }

            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", bINHLUANCTSP.SP_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", bINHLUANCTSP.KH_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", bINHLUANCTSP.NV_ID);
            return View(bINHLUANCTSP);
        }

        public RedirectToRouteResult CreateTL(string id2,string id3,string id4)
        {
            BINHLUANCTSP bINHLUANCTSP = new BINHLUANCTSP();
            if (ModelState.IsValid)
            {
                bINHLUANCTSP.BL_ID = db.autottang("BinhLuanCTSP", "BL_ID", db.BINHLUANCTSPs.Count()).ToString();
                if(Session["NV_ID"] != null)
                {
                    bINHLUANCTSP.NV_ID = Session["NV_ID"].ToString();
                }
                else
                {
                    bINHLUANCTSP.NV_ID = "0";
                }
                if(Session["KH_ID"] != null)
                {
                    bINHLUANCTSP.KH_ID = Session["KH_ID"].ToString();

                }else
                {
                    bINHLUANCTSP.KH_ID = "0";
                }
                
                bINHLUANCTSP.SP_ID = id2;
                bINHLUANCTSP.BL_TL = id3;
                bINHLUANCTSP.BL_ND = id4;
                bINHLUANCTSP.BL_THOIGIAN = DateTime.Now;
                db.BINHLUANCTSPs.Add(bINHLUANCTSP);
                db.SaveChanges();
                return RedirectToAction("details/"+id2,"SanPhams");
            }

           
            return RedirectToAction("details/" + id2, "SanPhams");
        }

        // GET: BINHLUANCTSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", bINHLUANCTSP.SP_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", bINHLUANCTSP.KH_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", bINHLUANCTSP.NV_ID);
            return View(bINHLUANCTSP);
        }

        // POST: BINHLUANCTSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BL_ID,SP_ID,NV_ID,KH_ID,BL_ND,BL_THOIGIAN,BL_TL")] BINHLUANCTSP bINHLUANCTSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bINHLUANCTSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SP_ID = new SelectList(db.SANPHAMs, "SP_ID", "SP_TEN", bINHLUANCTSP.SP_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", bINHLUANCTSP.KH_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", bINHLUANCTSP.NV_ID);
            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            return View(bINHLUANCTSP);
        }

        // POST: BINHLUANCTSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            db.BINHLUANCTSPs.Remove(bINHLUANCTSP);
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
