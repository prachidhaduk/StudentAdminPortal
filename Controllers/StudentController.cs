using System.Linq;
using System.Web.Mvc;
using StudentAdminPortal.Models;
using System.Data.Entity;
using StudentAdminPortal.Filters;

namespace StudentAdminPortal.Controllers
{
    [AdminAuthorize]
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student
        public ActionResult Index(string search)
        {
            var students = db.Students
                             .Include("Course")
                             .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                students = students.Where(s =>
                    s.FirstName.Contains(search) ||
                    s.LastName.Contains(search) ||
                    s.EnrollmentNo.Contains(search) ||
                    s.Email.Contains(search)
                );
            }

            ViewBag.TotalStudents = db.Students.Count();
            ViewBag.Search = search;

            return View(students.ToList());
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName"
            );

            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName",
                student.CourseId
            );

            return View(student);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName",
                student.CourseId
            );

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(
                db.Courses,
                "CourseId",
                "CourseName",
                student.CourseId
            );

            return View(student);
        }


        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);

            if (student != null)
            {
                // Delete student's marks
                var marks = db.Marks
                    .Where(m => m.StudentId == id)
                    .ToList();

                foreach (var mark in marks)
                {
                    db.Marks.Remove(mark);
                }

                // Delete student's attendance
                var attendance = db.Attendances
                    .Where(a => a.StudentId == id)
                    .ToList();

                foreach (var record in attendance)
                {
                    db.Attendances.Remove(record);
                }

                // Delete student
                db.Students.Remove(student);

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