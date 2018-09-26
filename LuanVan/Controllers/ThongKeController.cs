using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using LuanVan.Controllers;
using PagedList;
using PagedList.Mvc;

namespace LuanVan.Controllers
{
    public class ThongKeController : Controller
    {
        private DataContext db = new DataContext();
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult IndexChi()
        {
            laygiatritrongthang();
            return View();
        }


        [HttpPost]
        public ActionResult IndexChi(DateTime dauthang, DateTime cuoithang)
        {
            ViewBag.cuoithang = cuoithang;
            ViewBag.dauthang = dauthang;
            var model = (from pn in db.PHIEUNHAPSPs join ctn in db.CHITIETNHAPs on pn.PN_ID equals ctn.PN_ID where pn.PN_NGAY >= dauthang && pn.PN_NGAY <= cuoithang select ctn);

            return View(model);
        }


        public ActionResult ThongKetheoNhapNSX()
        {
            laygiatritrongthang();

            return View();
        }
        [HttpPost]
        public ActionResult ThongKetheoNhapNSX(string id, DateTime dau, DateTime cuoi)
        {
            ViewBag.NSX = db.NHASANXUATs.ToList();

            ViewBag.cuoithang = cuoi;

            ViewBag.dauthang = dau;
            if (id == "0")
            {
                var model = (from nsx in db.NHASANXUATs
                             join sp in db.SANPHAMs on nsx.NSX_ID equals sp.NSX_ID
                             join ctn in db.CHITIETNHAPs on sp.SP_ID equals ctn.SP_ID
                             join pn in db.PHIEUNHAPSPs on ctn.PN_ID equals pn.PN_ID
                             where pn.PN_NGAY >= dau && pn.PN_NGAY <= cuoi
                             select ctn);
                return View(model);
            }
            else
            {

                var model = (from nsx in db.NHASANXUATs
                             join sp in db.SANPHAMs on nsx.NSX_ID equals sp.NSX_ID
                             join ctn in db.CHITIETNHAPs on sp.SP_ID equals ctn.SP_ID
                             join pn in db.PHIEUNHAPSPs on ctn.PN_ID equals pn.PN_ID
                             where nsx.NSX_ID == id && pn.PN_NGAY >= dau && pn.PN_NGAY <= cuoi
                             select ctn);
                return View(model);
            }


        }

        public ViewResult ThongKeBanTheoNSX()
        {
            laygiatritrongthang();
            return View();
        }
        [HttpPost]
        public ViewResult ThongKeBanTheoNSX(string id, DateTime dau, DateTime cuoi)
        {
            ViewBag.NSX = db.NHASANXUATs.ToList();

            ViewBag.cuoithang = cuoi;

            ViewBag.dauthang = dau;
            if (id == "0")
            {
                var model = (from dn in db.DONHANGs
                             join ctdn in db.CHITIETDONHANGs on dn.DN_ID equals ctdn.DN_ID
                             join sp in db.SANPHAMs on ctdn.SP_ID equals sp.SP_ID
                             join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID
                             where dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi
                             select ctdn);
                return View(model);
            }
            else
            {

                var model = (from dn in db.DONHANGs
                             join ctdn in db.CHITIETDONHANGs on dn.DN_ID equals ctdn.DN_ID
                             join sp in db.SANPHAMs on ctdn.SP_ID equals sp.SP_ID
                             join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID
                             where sp.NSX_ID == id && dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi
                             select ctdn);

                return View(model);
            }
        }

        private void laygiatritrongthang()
        {
            ViewBag.NSX = db.NHASANXUATs.ToList();
            DateTime dtResult = new DateTime();
            dtResult = dtResult.AddMonths(DateTime.Now.Month);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            DateTime cuoithang = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dtResult.Day);
            ViewBag.cuoithang = cuoithang;
            DateTime dauthang = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ViewBag.dauthang = dauthang;
        }
    }
}