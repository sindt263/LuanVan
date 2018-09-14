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

        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            ViewBag.NSX = db.NHASANXUATs.ToList();
            ViewBag.sp = db.CHITIETSANPHAMs.ToList().Take(10);
            var ChiTietSanPham = new HomeController();
            var mode = ChiTietSanPham.ListAllPaging(searchTerm, page, pageSize).Take(6);
            ViewBag.SearchTerm = searchTerm;
            //var sANPHAMs = db.SANPHAMs.Include(s => s.DONGSANPHAM).Include(s => s.GIASP).Include(s => s.KHUYENMAI).Include(s => s.NHASANXUAT).Include(s => s.NHOMSANPHAM).Include(n => n.CHITIETNHAPs);
            return View(mode);
        }

        public IEnumerable<CHITIETSANPHAM> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.CTSP_MOTA.Contains(searchTerm) || x.CTSP_TEN.Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.CTSP_ID).ToPagedList(page, pageSize);
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

        
    }
}