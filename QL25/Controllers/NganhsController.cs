using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataIO;
using DataIO.Data;

namespace QL25.Controllers
{
    public class NganhsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Nganhs
        public ActionResult Index()
        {
            return View(db.Nganhs.ToList());
        }

        // GET: Nganhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }
            return View(nganh);
        }

        // GET: Nganhs/Create
        [CustomAuthorize("Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nganhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult Create([Bind(Include = "TenNganh")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                nganh.ID = Helper.GetNewNganhID(db);
                db.Nganhs.Add(nganh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nganh);
        }

        // GET: Nganhs/Edit/5
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }
            return View(nganh);
        }

        // POST: Nganhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult Edit([Bind(Include = "TenNganh")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nganh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nganh);
        }

        // GET: Nganhs/Delete/5
        [CustomAuthorize("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }
            return View(nganh);
        }

        // POST: Nganhs/Delete/5
        [CustomAuthorize("Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nganh nganh = db.Nganhs.Find(id);
            db.Nganhs.Remove(nganh);
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
