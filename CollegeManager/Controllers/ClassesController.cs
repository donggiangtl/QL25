using System.Linq;
using System.Net;
using System.Web.Mvc;
using CollegeManager.Models;

namespace CollegeManager.Controllers
{
    public class ClassesController : Controller
    {
        private CollegeContext db = new CollegeContext();

        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cls = db.Classes.Find(id);
            if (cls == null) return HttpNotFound();
            return View(cls);
        }

        public ActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Class cls)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(cls);
                db.SaveChanges();
                return RedirectToAction(""Index"");
            }
            return View(cls);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cls = db.Classes.Find(id);
            if (cls == null) return HttpNotFound();
            return View(cls);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Class cls)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cls).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(""Index"");
            }
            return View(cls);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cls = db.Classes.Find(id);
            if (cls == null) return HttpNotFound();
            return View(cls);
        }

        [HttpPost, ActionName(""Delete""), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cls = db.Classes.Find(id);
            db.Classes.Remove(cls);
            db.SaveChanges();
            return RedirectToAction(""Index"");
        }

        public PartialViewResult ClassMenu(int? selectedClassId)
        {
            var classes = db.Classes.ToList();
            ViewBag.SelectedClassId = selectedClassId;
            return PartialView(""_ClassMenu"", classes);
        }
    }
}
