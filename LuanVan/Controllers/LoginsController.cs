using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            
            Session["KH_ID"] = null;
            //Session["NV_ID"] = null;
            Session["KH_Ten"] = null;
            Session["KH_SDT"] = null;
            Session["KH_EMAIL"] = null;
            //Session["LNV_ID"] = null;
            Session["KH_DIACHI"] = null;
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
            
            string TK = Request["TK"];
            string MK = Request["MK"];
            string chuoimahoa = Encrypt(MK);
            string remember = Request["remember"];
            var result = (from p in db.KHACHHANGs where p.KH_TAIKHOAN == TK && p.KH_MATKHAU == chuoimahoa select p);
            if (result.Count() >= 1)
            {
                if (remember =="1")
                {
                    HttpCookie userInfo = new HttpCookie("LoginKH");
                    userInfo["KH_TaiKhoan"] = TK;
                    userInfo["KH_MatKhau"] = MK;
                    userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(userInfo);
                    ViewBag.TK = userInfo["KH_TaiKhoan"];
                    ViewBag.MK = userInfo["KH_MatKhau"];
                }
                else
                {
                    HttpCookie userInfo = new HttpCookie("LoginKH");
                    userInfo["KH_TaiKhoan"] = null;
                    userInfo["KH_MatKhau"] = null;
                    userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(userInfo);
                }
                foreach (var item in result)
                {
                    Session["KH_ID"] = item.KH_ID;
                    Session["KH_Ten"] = item.KH_TEN;
                    Session["KH_SDT"] = item.KH_SDT;
                    Session["KH_EMAIL"] = item.KH_EMAIL;
                    Session["KH_DIACHI"] = item.KH_DIACHI;
                }
                
                return Redirect("~/"+ Session["link"]);
            }
            else {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu sai ");
            }
               
            return View();
        }

        public ActionResult LoginNV()
        {
            Session["NV_ID"] = null;
            Session["LNV_ID"] = null;
            Session["NV_TEN"] = null;

            HttpCookie userInfo = Request.Cookies["LoginNV"];
            if (userInfo != null)
            {
                ViewBag.TKNV = userInfo["NV_TaiKhoan"];
                ViewBag.MKNV = userInfo["NV_MatKhau"];
            }
            return View();
        }

        [HttpPost, ActionName("LoginNV")]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLoginnv()
        {
            string TK = Request["NVTK"];
            string MK = Request["NVMK"];
            string remember = Request["remember"];
            string mahoa = Encrypt(MK);
            var result = (from p in db.NHANVIENs where p.NV_TAIKHOAN == TK && p.NV_MATKHAU == mahoa select p);
                if(result.Count() >= 1)
            {
                if (remember == "1")
                {
                    HttpCookie userInfo = new HttpCookie("LoginNV");
                    userInfo["NV_TaiKhoan"] = TK;
                    userInfo["NV_MatKhau"] = MK;
                    userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(userInfo);
                    ViewBag.TKNV = userInfo["NV_TaiKhoan"];
                    ViewBag.MKNV = userInfo["NV_MatKhau"];
                }
                else
                {
                    HttpCookie userInfo = new HttpCookie("LoginNV");
                    userInfo["NV_TaiKhoan"] = null;
                    userInfo["NV_MatKhau"] = null;
                    userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(userInfo);
                }
                foreach (var item in result)
                {
                    Session["NV_ID"] = item.NV_ID;
                    Session["NV_TEN"] = item.NV_TEN;
                    Session["LNV_ID"] = item.LNV_ID;
                }
                return Redirect("~/ChiTietSanPhams/");
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu sai ");
            }
            return View();
        }


        private string key = "SSSShop";

        /// <summary>
        /// Mã hóa chuỗi có mật khẩu
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa</returns>
        public string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Giản mã
        /// </summary>
        /// <param name="toDecrypt">Chuỗi đã mã hóa</param>
        /// <returns>Chuỗi giản mã</returns>
        public string Decrypt(string toDecrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}