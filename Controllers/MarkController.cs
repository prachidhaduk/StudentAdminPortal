using System.Linq;
using System.Web.Mvc;
using StudentAdminPortal.Models;
using System.Data.Entity;
using StudentAdminPortal.Filters;

namespace StudentAdminPortal.Controllers
{
    [AdminAuthorize]
    public class MarkController : Controller
    {
        private ApplicationDbContext db =
            new ApplicationDbContext();


        // =========================================================
        // INDEX
        // =========================================================

        // GET: Mark
        public ActionResult Index()
        {
            var marks = db.Marks
                .Include("Student")
                .Include("Subject")
                .ToList();

            return View(marks);
        }


        // =========================================================
        // DETAILS
        // =========================================================

        // GET: Mark/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest);
            }

            Mark mark = db.Marks
                .Include("Student")
                .Include("Subject")
                .FirstOrDefault(m => m.MarkId == id);

            if (mark == null)
            {
                return HttpNotFound();
            }

            return View(mark);
        }


        // =========================================================
        // CREATE
        // =========================================================

        // GET: Mark/Create
        public ActionResult Create()
        {
            LoadDropdowns(null);

            return View();
        }


        // POST: Mark/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mark mark)
        {
            // Calculate marks
            mark.MarksObtained =
                mark.InternalMarks + mark.ExternalMarks;

            // Maximum marks
            mark.MaxMarks = 100;

            // Total marks
            mark.TotalMarks =
                mark.InternalMarks + mark.ExternalMarks;

            // Percentage
            mark.Percentage =
                (mark.MarksObtained / mark.MaxMarks) * 100;

            // Grade
            mark.Grade =
                CalculateGrade(mark.Percentage);


            if (ModelState.IsValid)
            {
                db.Marks.Add(mark);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            LoadDropdowns(mark);

            return View(mark);
        }


        // =========================================================
        // EDIT
        // =========================================================

        // GET: Mark/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest);
            }

            Mark mark = db.Marks.Find(id);

            if (mark == null)
            {
                return HttpNotFound();
            }

            LoadDropdowns(mark);

            return View(mark);
        }


        // POST: Mark/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mark mark)
        {
            // Recalculate marks
            mark.MarksObtained =
                mark.InternalMarks + mark.ExternalMarks;

            mark.MaxMarks = 100;

            mark.TotalMarks =
                mark.InternalMarks + mark.ExternalMarks;

            mark.Percentage =
                (mark.MarksObtained / mark.MaxMarks) * 100;

            mark.Grade =
                CalculateGrade(mark.Percentage);


            if (ModelState.IsValid)
            {
                db.Entry(mark).State =
                    EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            LoadDropdowns(mark);

            return View(mark);
        }

        // =========================================================
        // DELETE
        // =========================================================

        // GET: Mark/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest);
            }

            Mark mark = db.Marks
                .Include("Student")
                .Include("Subject")
                .FirstOrDefault(m => m.MarkId == id);

            if (mark == null)
            {
                return HttpNotFound();
            }

            return View(mark);
        }


        // POST: Mark/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mark mark = db.Marks.Find(id);

            if (mark != null)
            {
                db.Marks.Remove(mark);

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================================================
        // DROPDOWNS
        // =========================================================

        private void LoadDropdowns(Mark mark)
        {
            int? selectedStudent = null;
            int? selectedSubject = null;

            if (mark != null)
            {
                selectedStudent = mark.StudentId;
                selectedSubject = mark.SubjectId;
            }


            // Student dropdown
            ViewBag.StudentId = new SelectList(
                db.Students
                    .OrderBy(s => s.FirstName)
                    .Select(s => new
                    {
                        StudentId = s.StudentId,

                        StudentName =
                            s.FirstName + " " +
                            s.LastName +
                            " (" +
                            s.EnrollmentNo +
                            ")"
                    }),

                "StudentId",
                "StudentName",
                selectedStudent
            );


            // Subject dropdown
            ViewBag.SubjectId = new SelectList(
                db.Subjects
                    .OrderBy(s => s.SubjectName),

                "SubjectId",
                "SubjectName",
                selectedSubject
            );
        }


        // =========================================================
        // GRADE CALCULATION
        // =========================================================

        private string CalculateGrade(decimal percentage)
        {
            if (percentage >= 90)
                return "A+";

            if (percentage >= 80)
                return "A";

            if (percentage >= 70)
                return "B+";

            if (percentage >= 60)
                return "B";

            if (percentage >= 50)
                return "C";

            if (percentage >= 40)
                return "D";

            return "F";
        }


        // =========================================================
        // DISPOSE
        // =========================================================

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