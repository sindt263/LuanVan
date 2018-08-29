using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;

namespace LuanVan.Controllers
{
    public class LoginsController : Controller
    {
        DataContext db = new DataContext();
        // GET: Logins
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoginKH()
        {
            HttpCookie userInfo = Request.Cookies["LoginKH"];
            if(userInfo != null)
            {
                ViewBag.TK = userInfo["KH_TaiKhoan"];
                ViewBag.MK = userInfo["KH_MatKhau"];
            }
            
            return View();
        }

        [HttpPost, ActionName("LoginKH")]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLoginKH()
        {            
            
            string TK = Request["KH_TAIKHOAN"];
            string MK = Request["KH_MATKHAU"];
            
            var result = (from p in db.KHACHHANGs where p.KH_TAIKHOAN == TK && p.KH_MATKHAU == MK select p);
            if (result.Count() >= 1)
            {
                HttpCookie userInfo = new HttpCookie("LoginKH");
                userInfo["KH_TaiKhoan"] = TK;
                userInfo["KH_MatKhau"] = MK;
                userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                Response.Cookies.Add(userInfo);
                ViewBag.TK = userInfo["KH_TaiKhoan"];
                ViewBag.MK = userInfo["KH_MatKhau"];
                foreach (var item in result)
                {
                    Session["KH_ID"] = item.KH_ID;
                    Session["KH_Ten"] = item.KH_TEN;
                }

                return Redirect("~/Home/");
            }
            else {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu sai " + TK + MK + result.Count());
            }
               
            return View();
        }

        public ActionResult LoginNV()
        {
            return View();
        }

        [HttpPost, ActionName("LoginNV")]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLoginnv()
        {
            string TK = Request["NV_TaiKhoan"];
            string MK = Request["NV_MatKhau"];
            var result = (from p in db.NHANVIENs where p.NV_TAIKHOAN == TK && p.NV_MATKHAU == MK select p);
                if(result.Count() >= 1)
            {
                foreach(var item in result)
                {
                    Session["NV_ID"] = item.NV_ID;
                    Session["LNV_ID"] = item.LNV_ID;
                }
                return Redirect("~/Home/");
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu sai "+TK+MK);
            }
            return View();
        }
    }
}