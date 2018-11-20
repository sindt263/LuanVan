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
                    Y = Convert.ToDouble(i.CHITIETSANPHAM.SANPHAM.GIASP.GIA_GIA),
                };
                dataPoints.Add(dataPoint);
            }

            ViewBag.DataPoints = "";
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
                    gia = gia + Convert.ToDouble(i.CHITIETSANPHAM.SANPHAM.GIASP.GIA_GIA);
                }
                DateTime a = Convert.ToDateTime(item.DN_NGALAPDON);
                DataPoint dataPoint = new DataPoint()
                {
                    X = " new Date(" + String.Format("{0:yyyy,MM-1,d}", a) + ")",
                    Y = gia,
                };
                dataPoints.Add(dataPoint);
                string da = JsonConvert.SerializeObject(dataPoints);
                ViewBag.DataPoints = da.Replace("\"", "");
            }
            return View();
        }



        public ViewResult ThongKeNhap()
        {
            laygiatritrongthang();

            ViewBag.DataPoints = "";
            return View();
        }

        [HttpPost]
        public ViewResult ThongkeNhap(DateTime dauthang, DateTime cuoithang)
        {
            @ViewBag.dauthang = dauthang;
            @ViewBag.cuoithang = cuoithang;
            var ngay = (from pn in db.PHIEUNHAPSPs where pn.PN_NGAY >= dauthang && pn.PN_NGAY <= cuoithang select pn).OrderBy(n => n.PN_NGAY);
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in ngay)
            {
                var data = (from ctn in db.CHITIETNHAPs
                            join pn in db.PHIEUNHAPSPs on ctn.PN_ID equals pn.PN_ID
                            where pn.PN_NGAY == item.PN_NGAY
                            select ctn);
                double gia = 0;
                foreach (var i in data)
                {
                    gia = gia + Convert.ToDouble(i.CHITIETSANPHAM.SANPHAM.GIASP.GIA_GIA);
                }
                DateTime a = Convert.ToDateTime(item.PN_NGAY);
                DataPoint dataPoint = new DataPoint()
                {
                    X = " new Date(" + String.Format("{0:yyyy,MM-1,d}", a) + ")",
                    Y = gia,
                };
                dataPoints.Add(dataPoint);
                string da = JsonConvert.SerializeObject(dataPoints);
                ViewBag.DataPoints = da.Replace("\"", "");


            }
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
                var model = (from pn in db.PHIEUNHAPSPs 
                             join ctn in db.CHITIETNHAPs on pn.PN_ID equals ctn.PN_ID
                             join ctsp in db.CHITIETSANPHAMs on ctn.CTSP_ID equals ctsp.CTSP_ID
                             join sp in db.SANPHAMs on ctsp.SP_ID equals sp.SP_ID
                             where pn.PN_NGAY >= dau && pn.PN_NGAY <= cuoi
                             select ctn);
                return View(model);
            }
            else
            {

                var model = (from pn in db.PHIEUNHAPSPs
                             join ctn in db.CHITIETNHAPs on pn.PN_ID equals ctn.PN_ID
                             join ctsp in db.CHITIETSANPHAMs on ctn.CTSP_ID equals ctsp.CTSP_ID
                             join sp in db.SANPHAMs on ctsp.SP_ID equals sp.SP_ID
                             where sp.NSX_ID == id && pn.PN_NGAY >= dau && pn.PN_NGAY <= cuoi
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
                             join ctsp in db.CHITIETSANPHAMs on ctdn.CTSP_ID equals ctsp.CTSP_ID
                             join sp in db.SANPHAMs on ctsp.SP_ID equals sp.SP_ID
                             where dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi && dn.TTDH_ID != 2
                             select ctdn);
                return View(model);
            }
            else
            {

                var model = (from dn in db.DONHANGs
                             join ctdn in db.CHITIETDONHANGs on dn.DN_ID equals ctdn.DN_ID
                             join ctsp in db.CHITIETSANPHAMs on ctdn.CTSP_ID equals ctsp.CTSP_ID
                             join sp in db.SANPHAMs on ctsp.SP_ID equals sp.SP_ID
                             where sp.NSX_ID == id && dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi && dn.TTDH_ID != 2
                             select ctdn);

                return View(model);
            }
        }

        //public JsonResult GetJsonData()
        //{
        //    Random r = new Random();
        //    var list = new List<CHITIETDONHANG>();
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        list.Add(new CHITIETDONHANG { DN_ID = "DN_ID " + i.ToString() });
        //    }
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}



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
            List<string> a = (from p in db.CHITIETDONHANGs select p.CTSP_ID).ToList();
            return a;
        }

        public ActionResult SPBanNhieuNhat()
        {
            laygiatritrongthang();
            ViewBag.SP = db.SANPHAMs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SPBanNhieuNhat(string SP_ID,DateTime dau, DateTime cuoi)
        {
            ViewBag.SP = db.SANPHAMs.ToList();
            ViewBag.dauthang = dau;
            ViewBag.cuoithang = cuoi;
           
            List<ThongKe> list = new List<ThongKe>();
            if (SP_ID == "0")
            {
                var TKSP = (from ctsp in db.CHITIETSANPHAMs join ctdn in db.CHITIETDONHANGs on ctsp.CTSP_ID equals ctdn.CTSP_ID
                            join dn in db.DONHANGs on ctdn.DN_ID equals dn.DN_ID where dn.TTDH_ID != 2
                            && dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi
                            select ctsp);
              
                foreach (var i in TKSP)
                {
                    if (list.FirstOrDefault(sp => sp.SP_ID == i.SP_ID) == null)
                    {
                        var count = (from ctsp in db.CHITIETSANPHAMs
                                    join ctdn in db.CHITIETDONHANGs on ctsp.CTSP_ID equals ctdn.CTSP_ID
                                    join dn in db.DONHANGs on ctdn.DN_ID equals dn.DN_ID
                                    where dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi
                                      && dn.TTDH_ID != 2 && ctsp.SP_ID == i.SP_ID
                                    select ctsp);
                        ThongKe tk = new ThongKe()
                        {
                            SP_ID = i.SP_ID,
                            TenSP = i.SANPHAM.SP_TEN,
                            SL = count.Count()
                        };
                        list.Add(tk);
                    }
                }
            }
            else
            {
                var TKSP = (from ctsp in db.CHITIETSANPHAMs join ctdn in db.CHITIETDONHANGs on ctsp.CTSP_ID equals ctdn.CTSP_ID
                            join dn in db.DONHANGs on ctdn.DN_ID equals dn.DN_ID
                           where dn.DN_NGALAPDON >= dau && dn.DN_NGALAPDON <= cuoi
                             && dn.TTDH_ID != 2 && ctsp.SP_ID == SP_ID  select ctsp);

                foreach (var i in TKSP)
                {
                    if (list.FirstOrDefault(sp => sp.SP_ID == i.SP_ID) == null)
                    {
                        ThongKe tk = new ThongKe()
                        {
                            SP_ID = i.SP_ID,
                            TenSP = i.SANPHAM.SP_TEN,
                            SL = TKSP.Count()
                        };
                        list.Add(tk);
                    }
                }
            }
            return View(list.OrderByDescending(sp=>sp.SL).ToList());
        }

      
    }
}