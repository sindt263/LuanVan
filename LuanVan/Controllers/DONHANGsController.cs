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
        public ActionResult IndexKH()
        {
            string kh_id = Session["KH_ID"].ToString();
            var dONHANGs = from p in db.DONHANGs where p.KH_ID == kh_id select p;
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
                    dONHANG.NV_ID = Session["NV_ID"].ToString();
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
         public ActionResult CreateDH(string id1, string id2,string id3)
        {
            string KH_ID = Session["KH_ID"].ToString();
            string result = db.Database.SqlQuery<string>("select KH_ID from KhachHang where KH_ID='" + KH_ID + "'").SingleOrDefault();
            SANPHAMsController sANPHAM = new SANPHAMsController();
            if (result != null)
                {
                    DONHANG dONHANG = new DONHANG();
                    if (ModelState.IsValid)
                    {
                    if (Session["DN_ID"] == null) { Session["DN_ID"] = db.autottang("DonHang", "DN_ID", db.DONHANGs.Count()); 
                    //dONHANG.NV_ID = Session["NV_ID"].ToString();
                        dONHANG.DN_ID = Session["DN_ID"].ToString();
                        dONHANG.TTDH_ID = 4;
                        dONHANG.KH_ID = KH_ID;
                        dONHANG.DN_NGALAPDON = DateTime.Now;
                        dONHANG.DN_GHICHU = "Khách đặc Online";
                        dONHANG.HTTT_ID = Convert.ToInt16(id1);
                        dONHANG.DN_SL = Convert.ToInt32(id2);
                        db.DONHANGs.Add(dONHANG);
                        db.SaveChanges();
                    }
                    else
                    {
                        string DNID = Session["DN_ID"].ToString();
                        DONHANG dON = db.DONHANGs.FirstOrDefault(m => m.DN_ID == DNID);                        
                        if (dON != null)
                        {
                            dON.HTTT_ID = Convert.ToInt16(id1);
                            dON.DN_SL = Convert.ToInt32(id2);
                            db.SaveChanges();
                        }
                    }
                        string DN_ID = Session["DN_ID"].ToString();
                    CHITIETDONHANG cHITIETDONHANG = new CHITIETDONHANG();
                        var giohang = Session["giohang"] as List<CartItem>;
                        foreach (var i in giohang)
                        {
                            string CTDH_ID = db.autottang("CHITIETDONHANG", "CTDH_ID", db.CHITIETDONHANGs.Count()).ToString();

                            string SP_ID = i.SanPhamID;
                            string TT=    db.Database.SqlQuery<string>("select SP_TRANGTHAI from SanPham where SP_ID ='" + SP_ID + "'").SingleOrDefault();
                        if (Convert.ToInt32(TT) == 1)
                        {
                            db.Database.ExecuteSqlCommand("Insert into ChiTietDonHang (CTDH_ID,DN_ID,SP_ID,CTDH_DIACHIGIAO) values('" + CTDH_ID + "','" + DN_ID + "','" + SP_ID + "','" + id3 + "')");
                            db.Database.ExecuteSqlCommand("update sanpham set SP_TRANGTHAI =0 where SP_ID ='" + SP_ID + "'");
                            ModelState.AddModelError("", "Xạc nhận mua " + i.SanPhamID + " thành công");
                        }

                        }
                        ModelState.AddModelError("", "Đã thêm chờ hàng vui lòng chờ duyệt đơn !");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Khách hàng không tồn tại");
                }
            
            return View();
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
                dONHANG.NV_ID = Session["NV_ID"].ToString();
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

        public JsonResult EditHuy(string id)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(m => m.DN_ID == id);
            var SP_ID = (from p in db.CHITIETDONHANGs where p.DN_ID == id select p);
            CHITIETDONHANGsController cHITIETDONHANGs = new CHITIETDONHANGsController();
            if (dONHANG != null)
            {
                foreach(var i in SP_ID)
                {
                    SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(sp => sp.SP_ID == i.SP_ID);
                    cHITIETDONHANGs.EditHuy(sANPHAM.SP_ID);
                }
                dONHANG.TTDH_ID = 2;
                db.SaveChanges();
            }
            string a = "Đã hũy đơn hàng " + id;
            return Json(a,JsonRequestBehavior.AllowGet);
        }
    }
}
