using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataIO.Data;

namespace QL25.Controllers
{
    public class HCsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: HCs
        public ActionResult Index()
        {
            var hCs = db.HCs.Include(h => h.BacDaoTao).Include(h => h.BC).Include(h => h.CapChuyenMon).Include(h => h.QH);
            return View(hCs.ToList());
        }

        // GET: HCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HC hC = db.HCs.Find(id);
            if (hC == null)
            {
                return HttpNotFound();
            }
            return View(hC);
        }

        // GET: HCs/Create
        public ActionResult Create()
        {
            ViewBag.BacDaoTao_ID = new SelectList(db.BacDaoTaos, "ID", "TenBac");
            ViewBag.BC_ID = new SelectList(db.BCs, "ID", "ID");
            ViewBag.CapChuyenMon_ID = new SelectList(db.CapChuyenMons, "ID", "TenCap");
            ViewBag.QH_ID = new SelectList(db.QHs, "ID", "CapHam");
            return View();
        }

        // POST: HCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BC_ID,HoTen,NgaySinh,NgayBatDau,NgayKetThuc,BacDaoTao_ID,CapChuyenMon_ID,QH_ID")] HC hC)
        {
            if (ModelState.IsValid)
            {
                db.HCs.Add(hC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BacDaoTao_ID = new SelectList(db.BacDaoTaos, "ID", "TenBac", hC.BacDaoTao_ID);
            ViewBag.BC_ID = new SelectList(db.BCs, "ID", "ID", hC.BC_ID);
            ViewBag.CapChuyenMon_ID = new SelectList(db.CapChuyenMons, "ID", "TenCap", hC.CapChuyenMon_ID);
            ViewBag.QH_ID = new SelectList(db.QHs, "ID", "CapHam", hC.QH_ID);
            return View(hC);
        }

        // GET: HCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HC hC = db.HCs.Find(id);
            if (hC == null)
            {
                return HttpNotFound();
            }
            ViewBag.BacDaoTao_ID = new SelectList(db.BacDaoTaos, "ID", "TenBac", hC.BacDaoTao_ID);
            ViewBag.BC_ID = new SelectList(db.BCs, "ID", "ID", hC.BC_ID);
            ViewBag.CapChuyenMon_ID = new SelectList(db.CapChuyenMons, "ID", "TenCap", hC.CapChuyenMon_ID);
            ViewBag.QH_ID = new SelectList(db.QHs, "ID", "CapHam", hC.QH_ID);
            return View(hC);
        }

        // POST: HCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BC_ID,HoTen,NgaySinh,NgayBatDau,NgayKetThuc,BacDaoTao_ID,CapChuyenMon_ID,QH_ID")] HC hC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BacDaoTao_ID = new SelectList(db.BacDaoTaos, "ID", "TenBac", hC.BacDaoTao_ID);
            ViewBag.BC_ID = new SelectList(db.BCs, "ID", "ID", hC.BC_ID);
            ViewBag.CapChuyenMon_ID = new SelectList(db.CapChuyenMons, "ID", "TenCap", hC.CapChuyenMon_ID);
            ViewBag.QH_ID = new SelectList(db.QHs, "ID", "CapHam", hC.QH_ID);
            return View(hC);
        }

        // GET: HCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HC hC = db.HCs.Find(id);
            if (hC == null)
            {
                return HttpNotFound();
            }
            return View(hC);
        }

        // POST: HCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HC hC = db.HCs.Find(id);
            db.HCs.Remove(hC);
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
