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

            var SanPhams = new HomeController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;


            ViewBag.KM = (from p in db.KHUYENMAIs where p.KM_NGAYKETTHUC >= DateTime.Now && p.KM_ID != "0" select p).OrderByDescending(a => a.KM_NGAYBATDAU);


            var ChiTietSanPham = new HomeController();

            ViewBag.SPMoi = db.CHITIETSANPHAMs.OrderByDescending(n => n.CTSP_NGAYTAO).Take(6);


            return View(mode);
        }

        public ActionResult SearchSP(string searchTerm, int page = 1, int pageSize = 100)
        {
            var SanPhams = new HomeController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;
            return View(mode);
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
            string sp_id = db.Database.SqlQuery<string>("select SP_ID from SanPham where CTSP_ID='" + id + "'").Take(1).SingleOrDefault();
            int result = db.Database.SqlQuery<int>("select count(*) from ChiTietDonHang where SP_ID ='" + sp_id + "'").SingleOrDefault();
            return result;
        }

    }
}