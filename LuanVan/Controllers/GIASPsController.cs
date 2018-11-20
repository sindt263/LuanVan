using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuanVan.Models;
using PagedList;
using PagedList.Mvc;

namespace LuanVan.Controllers
{
    public class GIASPsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: GIASPs
       
        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 11)
        {
            var SanPhams = new GIASPsController();
            var mode = SanPhams.ListAllPaging(searchTerm, page, pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(mode);
        }

        public IEnumerable<GIASP> ListAllPaging(string searchTerm, int page, int pageSize)
        {
            IQueryable<GIASP> model = db.GIASPs;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(x => x.GIA_ID.Contains(searchTerm) || x.GIA_GIA.ToString().Contains(searchTerm) || x.GIA_NGAYCAPNHAT.ToString().Contains(searchTerm));

            }

            return model.OrderByDescending(x => x.GIA_NGAYCAPNHAT).ToPagedList(page, pageSize);
        }
        // GET: GIASPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIASP gIASP = db.GIASPs.Find(id);
            if (gIASP == null)
            {
                return HttpNotFound();
            }
            return View(gIASP);
        }

        // GET: GIASPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GIASPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GIA_ID,GIA_GIA,GIA_NGAYCAPNHAT")] GIASP gIASP)
        {
            string GIA_ID = Request["GIA_ID"].ToString();
            GIASP GIA = db.GIASPs.Find(GIA_ID);
            if (GIA != null)
            {
                ModelState.AddModelError("", "Mã giá bị trùng !");
            }
            else
            if (ModelState.IsValid)
            {
                db.GIASPs.Add(gIASP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gIASP);
        }

        // GET: GIASPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIASP gIASP = db.GIASPs.Find(id);
            if (gIASP == null)
            {
                return HttpNotFound();
            }
            return View(gIASP);
        }

        // POST: GIASPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GIA_ID,GIA_GIA,GIA_NGAYCAPNHAT")] GIASP gIASP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIASP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gIASP);
        }

        // GET: GIASPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIASP gIASP = db.GIASPs.Find(id);
            if (gIASP == null)
            {
                return HttpNotFound();
            }
            return View(gIASP);
        }

        // POST: GIASPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GIASP gIASP = db.GIASPs.Find(id);
            db.GIASPs.Remove(gIASP);
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
