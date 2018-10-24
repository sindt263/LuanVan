using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using Newtonsoft.Json;
using LuanVan.Controllers;
using PagedList;
using PagedList.Mvc;
using System.Web.Services;
using System.Web.Script.Services;

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
        public ViewResult ThongKeban()
        {
            laygiatritrongthang();
            var data = (from dn in db.DONHANGs join ctdn in db.CHITIETDONHANGs on dn.DN_ID equals ctdn.DN_ID where dn.TTDH_ID != 2 select ctdn);
            List<DataPoint> dataPoints = new List<DataPoint>();

            foreach (var i in data)
            {
                DateTime a = Convert.ToDateTime(i.DONHANG.DN_NGALAPDON);
                DataPoint dataPoint = new DataPoint()
                {
                    X = " new Date(" + String.Format("{0:yyyy,MM-1,d}", a) + ")",
                    Y = Convert.ToDouble(i.SANPHAM.GIASP.GIA_GIA),
                };
                dataPoints.Add(dataPoint);
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
        [HttpPost]
        public ViewResult ThongKeban(DateTime dauthang, DateTime cuoithang)
        {
            @ViewBag.dauthang = dauthang;
            @ViewBag.cuoithang = cuoithang;
            var ngay = (from dn in db.DONHANGs
                        where dn.DN_NGALAPDON >= dauthang && dn.DN_NGALAPDON <= cuoithang && dn.TTDH_ID != 2
                        select dn);

            List<DataPoint> dataPoints = new List<DataPoint>();

            foreach (var item in ngay)
            {
                var data = (from ctdn in db.CHITIETDONHANGs
                            join dn in db.DONHANGs on ctdn.DN_ID equals dn.DN_ID
                            where dn.DN_NGALAPDON == item.DN_NGALAPDON && dn.TTDH_ID != 2
                            select ctdn);

                double gia = 0;
                foreach (var i in data)
                {
                    gia = gia + Convert.ToDouble(i.SANPHAM.GIASP.GIA_GIA);
                }
                DateTime a = Convert.ToDateTime(item.DN_NGALAPDON);
                DataPoint dataPoint = new DataPoint()
                {
                    X = " new Date(" + String.Format("{0:yyyy,MM-1,d}", a) + ")",
                    Y = gia,
                };
                dataPoints.Add(dataPoint);

                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            }
            return View();
        }
        public ViewResult ThongKeNhap()
        {
            laygiatritrongthang();
            var data = db.CHITIETNHAPs.OrderBy(n => n.PHIEUNHAPSP.PN_NGAY);
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var i in data)
            {
                DateTime a = Convert.ToDateTime(i.PHIEUNHAPSP.PN_NGAY);
                DataPoint dataPoint = new DataPoint()
                {
                    X = " new Date(" + String.Format("{0:yyyy,MM-1,d}", a) + ")",
                    Y = Convert.ToDouble(i.SANPHAM.GIASP.GIA_GIA),
                };
                dataPoints.Add(dataPoint);
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }

        [HttpPost]
        public ViewResult ThongkeNhap(DateTime dauthang, DateTime cuoithang)
        {
            @ViewBag.dauthang = dauthang;
            @ViewBag.cuoithang = cuoithang;
            var data = (from ctn in db.CHITIETNHAPs
                        join pn in db.PHIEUNHAPSPs on ctn.PN_ID equals pn.PN_ID
                        where pn.PN_NGAY >= dauthang && pn.PN_NGAY <= cuoithang
                        select ctn).OrderBy(n => n.PHIEUNHAPSP.PN_NGAY);
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var i in data)
            {
                DateTime a = Convert.ToDateTime(i.PHIEUNHAPSP.PN_NGAY);
                DataPoint dataPoint = new DataPoint()
                {
                    X = " new Date(" + String.Format("{0:yyyy,MM-1,d}", a) + ")",
                    Y = Convert.ToDouble(i.CTN_GIA),
                };
                dataPoints.Add(dataPoint);
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

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
                             where dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi && dn.TTDH_ID != 2
                             select ctdn);
                return View(model);
            }
            else
            {

                var model = (from dn in db.DONHANGs
                             join ctdn in db.CHITIETDONHANGs on dn.DN_ID equals ctdn.DN_ID
                             join sp in db.SANPHAMs on ctdn.SP_ID equals sp.SP_ID
                             join gia in db.GIASPs on sp.GIA_ID equals gia.GIA_ID
                             where sp.NSX_ID == id && dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi && dn.TTDH_ID != 2
                             select ctdn);

                return View(model);
            }
        }

        public JsonResult GetJsonData()
        {
            Random r = new Random();
            var list = new List<CHITIETDONHANG>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(new CHITIETDONHANG { DN_ID = "DN_ID " + i });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
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

        public List<string> gettest()
        {
            List<string> a = (from p in db.CHITIETDONHANGs select p.SP_ID).ToList();
            return a;
        }

        public ActionResult SPBanNhieuNhat()
        {
            laygiatritrongthang();
            ViewBag.NSX = db.NHASANXUATs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SPBanNhieuNhat(DateTime dau, DateTime cuoi)
        {
            laygiatritrongthang();
            var nsx = db.NHASANXUATs.ToList();
            foreach (var i in nsx)
            {
                a(i.NSX_TEN, dau, cuoi);
            }
            List<ThongKe> li = Session["thongke"] as List<ThongKe>;
            return View(li.OrderByDescending(a => a.SL));
        }

        public int a(string id, DateTime dau, DateTime cuoi)
        {
            if (Session["thongke"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["thongke"] = new List<ThongKe>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }
            List<ThongKe> thongKes = Session["thongke"] as List<ThongKe>;

            int i = db.Database.SqlQuery<int>("select  COUNT(sp.SP_ID) from SANPHAM sp inner join NHASANXUAT nsx on sp.NSX_ID = nsx.NSX_ID inner join CHITIETDONHANG ctdn on ctdn.SP_ID = sp.SP_ID inner join DONHANG dn on ctdn.DN_ID = dn.DN_ID where nsx.NSX_TEN = N'" + id + "' and dn.DN_NGALAPDON >= N'" + dau + "' and dn.DN_NGALAPDON <= N'" + cuoi + "'").FirstOrDefault();

            ThongKe thongKe = new ThongKe()
            {
                SL = i,
                TenNSX = id
            };
            thongKes.Add(thongKe);
            return i;
        }
    }
}