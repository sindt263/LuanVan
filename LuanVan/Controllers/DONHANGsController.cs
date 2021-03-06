﻿using System;
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
    public class DONHANGsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: DONHANGs

        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new DONHANGsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<DONHANG> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<DONHANG> model = db.DONHANGs.Include(d => d.HINHTHUCTHANHTOAN).Include(d => d.KHACHHANG).Include(d => d.TRANGTHAIDONHANG);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.DN_ID.ToString().Contains(searchTerm) || x.KH_ID.Contains(searchTerm)
                || x.NV_ID.Contains(searchTerm) || x.KHACHHANG.KH_TEN.Contains(searchTerm) || x.TRANGTHAIDONHANG.TTDH_TEN.Contains(searchTerm)
                 || x.DN_NGALAPDON.ToString().Contains(searchTerm));
            }
            return model.OrderByDescending(x => x.DN_ID).ToPagedList(page, pageSize);
        }


        public ActionResult IndexKH(string searchTerm, int page = 1, int pageSize = 11)
        {
            if (Session["KH_ID"] != null)
            {
                string id = Session["KH_ID"].ToString();

                var SanPhams = new DONHANGsController();
                var mode = SanPhams.ListAllPaging2(id, searchTerm, page, pageSize);
                ViewBag.SearchTerm = searchTerm;
                return View(mode);
            }
            else
            {
                return RedirectToAction("LoginKH", "Logins");
            }
        }
        public IEnumerable<DONHANG> ListAllPaging2(string id, string searchTerm, int page, int pageSize)
        {

            IQueryable<DONHANG> model = (from p in db.DONHANGs where p.KH_ID == id select p);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.DN_ID.ToString().Contains(searchTerm)
               || x.KHACHHANG.KH_TEN.Contains(searchTerm)
               || x.TRANGTHAIDONHANG.TTDH_TEN.Contains(searchTerm));

            }
            return model.OrderByDescending(x => x.DN_ID).ToPagedList(page, pageSize);
        }
        public IEnumerable<DONHANG> ListAllPaging1(string searchTerm, int page, int pageSize)
        {

            IQueryable<DONHANG> model = (from p in db.DONHANGs where p.DN_SDT == searchTerm select p);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.DN_SDT.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.DN_ID).ToPagedList(page, pageSize);

        }
      
        public ActionResult IndexDH(string searchTerm, int page = 1, int pageSize = 11)
        {

            var SanPhams = new DONHANGsController();
            var mode = SanPhams.ListAllPaging1(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
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
        public ActionResult Create([Bind(Include = "DN_ID,TTDH_ID,KH_ID,HTTT_ID,DN_NGALAPDON,DN_GHICHU,DN_SL,DN_SDT,DN_DIACHI,DN_MATHE,DN_CHUTHE,DN_NGAYCAP,DN_EMAIL")] DONHANG dONHANG)
        {
            string KH_ID = Request["KH_ID"];
            string result = db.Database.SqlQuery<string>("select KH_ID from KhachHang where KH_ID='" + KH_ID + "'").FirstOrDefault();

            if (result != null)
            {
                if (ModelState.IsValid)
                {
                    dONHANG.NV_ID = Session["NV_ID"].ToString();
                    dONHANG.DN_ID = db.autottang("DonHang", "DN_ID", db.DONHANGs.Count());
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

        public ActionResult CreateDH(string id1, string id2, string id3, string id4)
        {

            if (id4.Length <= 9 || string.IsNullOrEmpty(id3))
            {
                ModelState.AddModelError("", "Vui lòng điền đầy đủ thông tin !");

            }
            else
            {

                SANPHAMsController sANPHAM = new SANPHAMsController();

                DONHANG dONHANG = new DONHANG();
                if (ModelState.IsValid)
                {


                    //dONHANG.NV_ID = Session["NV_ID"].ToString();
                    dONHANG.DN_ID = db.autottang("DonHang", "DN_ID", db.DONHANGs.Count());
                    dONHANG.DN_SDT = id4;
                    dONHANG.TTDH_ID = 4;
                    if (Session["KH_ID"] != null)
                    {
                        string KH_ID = Session["KH_ID"].ToString();
                        dONHANG.KH_ID = KH_ID;
                    }

                    dONHANG.DN_NGALAPDON = DateTime.Now;
                    dONHANG.DN_GHICHU = "Khách đặc Online";
                    dONHANG.HTTT_ID = Convert.ToInt16(id1);
                    //dONHANG.DN_SL = Convert.ToInt32(id2);
                    db.DONHANGs.Add(dONHANG);
                    db.SaveChanges();


                    int DN_ID = Convert.ToInt32(dONHANG.DN_ID);
                    CHITIETDONHANG cHITIETDONHANG = new CHITIETDONHANG();
                    var giohang = Session["giohang"] as List<CartItem>;
                    foreach (var i in giohang)
                    {
                        string CTDH_ID = db.autottang("CHITIETDONHANG", "CTDH_ID", db.CHITIETDONHANGs.Count()).ToString();

                        string SP_ID = i.SanPhamID;
                        short TT = db.Database.SqlQuery<short>("select SP_TRANGTHAI from SanPham where SP_ID ='" + SP_ID + "'").SingleOrDefault();
                        if (TT == 1)
                        {
                            db.Database.ExecuteSqlCommand("Insert into ChiTietDonHang (CTDH_ID,DN_ID,SP_ID,CTDH_DIACHIGIAO) values('" + CTDH_ID + "','" + DN_ID + "','" + SP_ID + "',N'" + id3 + "')");
                            db.Database.ExecuteSqlCommand("update sanpham set SP_TRANGTHAI =0 where SP_ID ='" + SP_ID + "'");

                            ModelState.AddModelError("", "Xạc nhận mua " + i.SanPhamID + " thành công");
                        }
                        CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == i.SanPhamID);
                        giohang.Remove(itemXoa);
                    }
                    ModelState.AddModelError("", "Đã thêm chờ hàng vui lòng chờ duyệt đơn !");
                }

            }
            return View();
        }

        // GET: DONHANGs/Edit/5
        public ActionResult Edit(int id)
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
        public ActionResult Edit([Bind(Include = "DN_ID,TTDH_ID,KH_ID,HTTT_ID,DN_NGALAPDON,DN_GHICHU,DN_SDT,DN_EMAIL,DN_DIACHI,DN_MATHE,DN_CHUTHE,DN_NGAYCAP")] DONHANG dONHANG)
        {
            Mail mail = new Mail();
            var CTSP_ID = (from p in db.CHITIETDONHANGs where p.DN_ID == dONHANG.DN_ID select p);
            CHITIETDONHANGsController cHITIETDONHANGs = new CHITIETDONHANGsController();
            if (dONHANG != null)
            {

                if (Convert.ToInt16(dONHANG.TTDH_ID) == 2)
                {
                    foreach (var i in CTSP_ID)
                    {
                        CHITIETSANPHAM cHITIETSANPHAM = db.CHITIETSANPHAMs.FirstOrDefault(sp => sp.CTSP_ID == i.CTSP_ID);
                        cHITIETDONHANGs.EditHuy(cHITIETSANPHAM.CTSP_ID);
                    }
                    string guimail = mail.SendMailFull("sindt264@gmail.com", "vinhvavinh", "SMTP.gmail.com", "587", dONHANG.DN_EMAIL, "Đã hủy đơn hàng", "Đơn hàng " + dONHANG.DN_ID + " đã bị hủy .", true);
                }
                else if (Convert.ToInt16(dONHANG.TTDH_ID) == 1)
                {
                    string guimail = mail.SendMailFull("sindt264@gmail.com", "vinhvavinh", "SMTP.gmail.com", "587", dONHANG.DN_EMAIL, "Đã duyệt đơn hàng", "Đơn hàng " + dONHANG.DN_ID + " đã được duyệt thành công", true);
                }
                else if (Convert.ToInt16(dONHANG.TTDH_ID) == 3)
                {
                    string guimail = mail.SendMailFull("sindt264@gmail.com", "vinhvavinh", "SMTP.gmail.com", "587", dONHANG.DN_EMAIL, "Đã giao thành công", "Đơn hàng " + dONHANG.DN_ID + " đã được giao thành công", true);
                }

                dONHANG.NV_ID = Session["NV_ID"].ToString();
                dONHANG.TTDH_ID = dONHANG.TTDH_ID;
                if (ModelState.IsValid)
                {
                    db.Entry(dONHANG).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dONHANG);
            }

            //if (ModelState.IsValid)
            //{
            //if (Convert.ToInt16(id_ttdh) == 2)
            //{
            //    EditHuy(id_dh);
            //}
            //    dONHANG.NV_ID = Session["NV_ID"].ToString();
            //    db.Entry(dONHANG).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            ViewBag.HTTT_ID = new SelectList(db.HINHTHUCTHANHTOANs, "HTTT_ID", "HTTT_TEN", dONHANG.HTTT_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", dONHANG.KH_ID);
            ViewBag.TTDH_ID = new SelectList(db.TRANGTHAIDONHANGs, "TTDH_ID", "TTDH_TEN", dONHANG.TTDH_ID);
            return View(dONHANG);
        }

        // GET: DONHANGs/Delete/5
        public ActionResult Delete(int id)
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
        public ActionResult DeleteConfirmed(int id)
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
            if (result != null)
            {
                string output = "Đã xác định khách hàng " + id;
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string output = "Khánh hàng " + id + " không tồn tại";
                return Json(output, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditHuy(int id)
        {
            Mail mail = new Mail();
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(m => m.DN_ID == id);
            var SP_ID = (from p in db.CHITIETDONHANGs where p.DN_ID == id select p);
            CHITIETDONHANGsController cHITIETDONHANGs = new CHITIETDONHANGsController();
            if (dONHANG != null)
            {
                foreach (var i in SP_ID)
                {
                    CHITIETSANPHAM cHITIETSANPHAM = db.CHITIETSANPHAMs.FirstOrDefault(sp => sp.CTSP_ID == i.CTSP_ID);
                    cHITIETDONHANGs.EditHuy(cHITIETSANPHAM.SP_ID);
                }
                dONHANG.TTDH_ID = 2;
                string guimail = mail.SendMailFull("sindt264@gmail.com", "vinhvavinh", "SMTP.gmail.com", "587", Session["KH_EMAIL"].ToString(), "Đã hủy đơn hàng", "Đơn hàng " + dONHANG.DN_ID + " đã bị hủy .", true);
                db.SaveChanges();
            }
            string a = "Đã hũy đơn hàng " + id;
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditDuyet(int id)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(m => m.DN_ID == id);
            var SP_ID = (from p in db.CHITIETDONHANGs where p.DN_ID == id select p);
            CHITIETDONHANGsController cHITIETDONHANGs = new CHITIETDONHANGsController();
            if (dONHANG != null)
            {
                foreach (var i in SP_ID)
                {
                    SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(sp => sp.SP_ID == i.CTSP_ID);
                    cHITIETDONHANGs.EditHuy(sANPHAM.SP_ID);
                }
                dONHANG.TTDH_ID = 2;
                db.SaveChanges();
            }
            string a = "Đã hũy đơn hàng " + id;
            return Json(a, JsonRequestBehavior.AllowGet);
        }
    }
}
