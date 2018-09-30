using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using LuanVan.Controllers;
using PagedList;
using PagedList.Mvc;

namespace LuanVan.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        //public ActionResult Index()
        //{
        //    ViewBag.NSX = db.NHASANXUATs.ToList();
        //    ViewBag.sp = db.CHITIETSANPHAMs.ToList();
        //    var sp = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM).Include(n => n.CHITIETNHAPs).OrderByDescending(s => s.CTSP_ID);
        //    return View(sp);
        //}

        public ViewResult test()
        {
           
            return View();
        }

        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 100)
        {
            //ViewSP
            ViewBag.nsx = db.NHASANXUATs.ToList();

            var SanPhams = new SANPHAMsController();
            var model = SanPhams.ListAllPaging1(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            foreach (var ii in ViewBag.nsx)
            {
                string id = ii.NSX_ID;

                ViewData[ii.NSX_TEN] = (from p in db.SANPHAMs where p.NSX_ID == id select p.CTSP_ID).Distinct();
                foreach (var iii in ViewData[ii.NSX_TEN])
                {
                    string ctsp_id = iii;
                    ViewData[iii] = (from p in db.CHITIETSANPHAMs where p.CTSP_ID == ctsp_id select p);
                }

            }

            ViewBag.KM = (from p in db.KHUYENMAIs where p.KM_NGAYKETTHUC >= DateTime.Now && p.KM_ID != "0" select p).OrderByDescending(a => a.KM_NGAYBATDAU);

            var PN_ID = (from p in db.PHIEUNHAPSPs select p).OrderByDescending(a => a.PN_NGAY);
            string SP_ID = "";
            foreach (var ix in PN_ID)
            {
                SP_ID += db.Database.SqlQuery<string>("select SP_ID from ChiTietNhap where PN_ID ='" + ix.PN_ID + "'").DefaultIfEmpty();
            }
            ViewBag.MaSP = SP_ID;

            //Home
            string i = db.Database.SqlQuery<string>("select top 1 ctsp.CTSP_ID from ((ChitietSanPham ctsp inner join SanPham sp on sp.CTSP_ID = ctsp.CTSP_ID) inner join CHITIETDONHANG ctdn on sp.SP_ID = ctdn.SP_ID)  group by ctsp.CTSP_ID, ctsp.CTSP_TEN order by Count(ctsp.CTSP_ID) desc ").FirstOrDefault();
            ViewBag.Top = i;
            ViewBag.NSX = db.NHASANXUATs.ToList();
            ViewBag.KM = (from p in db.KHUYENMAIs where p.KM_NGAYKETTHUC >= DateTime.Now select p).OrderByDescending(k => k.KM_NGAYBATDAU).Take(10);
            ViewBag.sp = db.SANPHAMs.ToList().Take(10);
            var ChiTietSanPham = new HomeController();

            //ViewBag.SPMoi = ChiTietSanPham.ListAllPaging(searchTerm, page, pageSize).Take(6);
            ViewBag.SPMoi = db.CHITIETSANPHAMs.OrderByDescending(n => n.CTSP_NGAYTAO).Take(6);
            ViewBag.SearchTerm = searchTerm;
            //var sANPHAMs = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM).Include(n => n.CHITIETNHAPs);
            return View(model);
        }

        public IEnumerable<CHITIETSANPHAM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.CTSP_MOTA.Contains(searchTerm) || x.CTSP_TEN.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.CTSP_NGAYTAO).ToPagedList(page, pageSize);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PagesEmpty()
        {
            return View();
        }

        public int count(string id)
        {
            string sp_id = db.Database.SqlQuery<string>("select SP_ID from SanPham where CTSP_ID='"+id+"'").Take(1).SingleOrDefault();
            int result = db.Database.SqlQuery<int>("select count(*) from ChiTietDonHang where SP_ID ='"+sp_id+"'").SingleOrDefault();
            return result;
        }

    }
}