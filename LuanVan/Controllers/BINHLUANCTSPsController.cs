﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;

namespace LuanVan.Controllers
{
    public class BINHLUANCTSPsController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult IndexKH(string id)
        {
            return View(db.BINHLUANCTSPs.Where(a => a.CTSP_ID == id).Where(a=>a.BL_TL == null).ToList());
        }

        public ActionResult CreateKH(string id, string id2)
        {
            BINHLUANCTSP bINHLUANCTSP = new BINHLUANCTSP();
            if (ModelState.IsValid)
            {
                bINHLUANCTSP.BL_ID = db.autottang("BinhLuanCTSP", "BL_ID", db.BINHLUANCTSPs.Count()).ToString();
                bINHLUANCTSP.BL_ND = id;
                bINHLUANCTSP.CTSP_ID = id2;
                bINHLUANCTSP.BL_THOIGIAN = DateTime.Now;
                if(Session["KH_ID"] != null)
                {
                    bINHLUANCTSP.KH_ID = Session["KH_ID"].ToString();
                    bINHLUANCTSP.NV_ID = "0";
                }
                if (Session["NV_ID"] != null)
                {
                    bINHLUANCTSP.NV_ID = Session["NV_ID"].ToString();
                    bINHLUANCTSP.KH_ID = "0";
                }
               
                db.BINHLUANCTSPs.Add(bINHLUANCTSP);
                db.SaveChanges();
                return RedirectToAction("IndexKH/" + id2);
            }

            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs
        public ActionResult Index()
        {
            var bINHLUANCTSPs = db.BINHLUANCTSPs.Include(b => b.CHITIETSANPHAM).Include(b => b.KHACHHANG).Include(b => b.NHANVIEN);
            return View(bINHLUANCTSPs.ToList());
        }

        // GET: BINHLUANCTSPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Create
        public ActionResult Create()
        {
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN");
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN");
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN");
            return View();
        }

        // POST: BINHLUANCTSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, string id2,string id3,[Bind(Include = "BL_ID,CTSP_ID,NV_ID,KH_ID,BL_ND,BL_THOIGIAN")] BINHLUANCTSP bINHLUANCTSP)
        {
            if (ModelState.IsValid)
            {
                bINHLUANCTSP.BL_ID = db.autottang("BinhLuanCTSP", "BL_ID", db.BINHLUANCTSPs.Count()).ToString();
                if(Session["NV_ID"] != null)
                {
                    bINHLUANCTSP.NV_ID = Session["NV_ID"].ToString();
                    bINHLUANCTSP.KH_ID = "0";
                }
                if(Session["KH_ID"] != null)
                {
                    bINHLUANCTSP.KH_ID = Session["KH_ID"].ToString();
                    bINHLUANCTSP.NV_ID = "0";
                }
                
                bINHLUANCTSP.KH_ID = id;
                bINHLUANCTSP.CTSP_ID = id2;
                bINHLUANCTSP.BL_TL = id3;
                bINHLUANCTSP.BL_THOIGIAN = DateTime.Now;
                db.BINHLUANCTSPs.Add(bINHLUANCTSP);
                db.SaveChanges();
                return RedirectToAction("details/"+id2,"chitietsanphams");
            }

            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", bINHLUANCTSP.CTSP_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", bINHLUANCTSP.KH_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", bINHLUANCTSP.NV_ID);
            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", bINHLUANCTSP.CTSP_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", bINHLUANCTSP.KH_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", bINHLUANCTSP.NV_ID);
            return View(bINHLUANCTSP);
        }

        // POST: BINHLUANCTSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BL_ID,CTSP_ID,NV_ID,KH_ID,BL_ND,BL_THOIGIAN")] BINHLUANCTSP bINHLUANCTSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bINHLUANCTSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CTSP_ID = new SelectList(db.CHITIETSANPHAMs, "CTSP_ID", "CTSP_TEN", bINHLUANCTSP.CTSP_ID);
            ViewBag.KH_ID = new SelectList(db.KHACHHANGs, "KH_ID", "KH_TEN", bINHLUANCTSP.KH_ID);
            ViewBag.NV_ID = new SelectList(db.NHANVIENs, "NV_ID", "NV_TEN", bINHLUANCTSP.NV_ID);
            return View(bINHLUANCTSP);
        }

        // GET: BINHLUANCTSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            if (bINHLUANCTSP == null)
            {
                return HttpNotFound();
            }
            return View(bINHLUANCTSP);
        }

        // POST: BINHLUANCTSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BINHLUANCTSP bINHLUANCTSP = db.BINHLUANCTSPs.Find(id);
            db.BINHLUANCTSPs.Remove(bINHLUANCTSP);
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
    }
}