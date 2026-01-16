using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CollegeManager.Models;

namespace CollegeManager.Controllers
{
    public class StudentsController : Controller
    {
        private CollegeContext db = new CollegeContext();

        public ActionResult ByClass(int classId)
        {
            var students = db.Students.Where(s => s.ClassID == classId)
                                      .Include(s => s.Class).ToList();
            ViewBag.Class = db.Classes.Find(classId);
            return View(students);
        }

        public ActionResult Create(int? classId)
        {
            ViewBag.ClassID = new SelectList(db.Classes, ""ClassID"", ""ClassName"", classId);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction(""ByClass"", new { classId = student.ClassID });
            }
            ViewBag.ClassID = new SelectList(db.Classes, ""ClassID"", ""ClassName"", student.ClassID);
            return View(student);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var student = db.Students.Find(id);
            if (student == null) return HttpNotFound();
            ViewBag.ClassID = new SelectList(db.Classes, ""ClassID"", ""ClassName"", student.ClassID);
            return View(student);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(""ByClass"", new { classId = student.ClassID });
            }
            ViewBag.ClassID = new SelectList(db.Classes, ""ClassID"", ""ClassName"", student.ClassID);
            return View(student);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var student = db.Students.Find(id);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        [HttpPost, ActionName(""Delete""), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = db.Students.Find(id);
            int classId = student.ClassID;
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction(""ByClass"", new { classId = classId });
        }
    }
}
