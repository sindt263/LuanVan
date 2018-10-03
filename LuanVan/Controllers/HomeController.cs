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

        public ActionResult Index()
        {
            //ViewSP
            ViewBag.nsx = db.NHASANXUATs.ToList();
            


            ViewBag.KM = (from p in db.KHUYENMAIs where p.KM_NGAYKETTHUC >= DateTime.Now && p.KM_ID != "0" select p).OrderByDescending(a => a.KM_NGAYBATDAU);

            
            ViewBag.SPMoi = db.CHITIETSANPHAMs.OrderByDescending(n => n.CTSP_NGAYTAO).Take(6);


            return View(db.CHITIETSANPHAMs.ToList());
        }

        public ActionResult SearchSP(string searchTerm,int id=0, int id2= 0, int page = 1, int pageSize = 100)
        {
            var SanPhams = new HomeController();
            var mode = SanPhams.ListAllPaging(searchTerm,id,id2, page, pageSize);
            ViewBag.SearchTerm = searchTerm;
           
            return View(mode);
        }
        public IEnumerable<CHITIETSANPHAM> ListAllPaging(string searchTerm,int id, int id2, int page, int pageSize)
        {
            IQueryable<CHITIETSANPHAM> model = db.CHITIETSANPHAMs;

            if (id!=0 && id2!= 0 && string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.CTSP_ID equals ctsp.CTSP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA >=id &&  gia.GIA_GIA <= id2 select ctsp).Distinct();
                return result.OrderByDescending(x => x.CTSP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if(id == 0 && id2 == 0 && !string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.CTSP_ID equals ctsp.CTSP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where (ctsp.CTSP_TEN.Contains(searchTerm) || ctsp.CTSP_MOTA.Contains(searchTerm)) select ctsp).Distinct();
                return result.OrderByDescending(x => x.CTSP_NGAYTAO).ToPagedList(page, pageSize);
            }            
            else if(id != 0 && id2 == 0 && string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.CTSP_ID equals ctsp.CTSP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA >= id  select ctsp).Distinct();
                return result.OrderByDescending(x => x.CTSP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if(id != 0 && id2 == 0 && !string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.CTSP_ID equals ctsp.CTSP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA >= id  && (ctsp.CTSP_TEN.Contains(searchTerm) || ctsp.CTSP_MOTA.Contains(searchTerm)) select ctsp).Distinct();
                return result.OrderByDescending(x => x.CTSP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if(id == 0 && id2 != 0 && string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.CTSP_ID equals ctsp.CTSP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA <= id2  select ctsp).Distinct();
                return result.OrderByDescending(x => x.CTSP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else if(id == 0 && id2 != 0 && !string.IsNullOrEmpty(searchTerm))
            {
                var result = (from sp in db.SANPHAMs join ctsp in db.CHITIETSANPHAMs on sp.CTSP_ID equals ctsp.CTSP_ID join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID where gia.GIA_GIA <= id2 && (ctsp.CTSP_TEN.Contains(searchTerm) || ctsp.CTSP_MOTA.Contains(searchTerm)) select ctsp).Distinct();
                return result.OrderByDescending(x => x.CTSP_NGAYTAO).ToPagedList(page, pageSize);
            }
            else
            {
                return model;
            }
            
           
            
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