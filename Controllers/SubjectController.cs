using System.Linq;
using System.Web.Mvc;
using StudentAdminPortal.Models;
using StudentAdminPortal.Filters;

namespace StudentAdminPortal.Controllers
{
    [AdminAuthorize]
    public class SubjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subject
        public ActionResult Index()
        {
            var subjects = db.Subjects
                             .Include("Course")
                             .ToList();

            return View(subjects);
        }

        // GET: Subject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            Subject subject = db.Subjects
                                .Include("Course")
                                .FirstOrDefault(s => s.SubjectId == id);

            if (subject == null)
            {
                return HttpNotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName"
            );

            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName",
                subject.CourseId
            );

            return View(subject);
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            Subject subject = db.Subjects.Find(id);

            if (subject == null)
            {
                return HttpNotFound();
            }

            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName",
                subject.CourseId
            );

            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State =
                    System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName",
                subject.CourseId
            );

            return View(subject);
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            Subject subject = db.Subjects
                                .Include("Course")
                                .FirstOrDefault(s => s.SubjectId == id);

            if (subject == null)
            {
                return HttpNotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);

            if (subject != null)
            {
                db.Subjects.Remove(subject);
                db.SaveChanges();
            }

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