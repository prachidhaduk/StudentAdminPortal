using System.Linq;
using System.Web.Mvc;
using StudentAdminPortal.Models;
using StudentAdminPortal.Filters;

namespace StudentAdminPortal.Controllers
{
    [AdminAuthorize]
    public class DashboardController : Controller
    {
        private ApplicationDbContext db =
            new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.TotalStudents = db.Students.Count();

            ViewBag.TotalCourses = db.Courses.Count();

            ViewBag.TotalSubjects = db.Subjects.Count();

            ViewBag.TotalAttendance =
                db.Attendances.Count();

            ViewBag.PresentCount =
                db.Attendances.Count(a => a.IsPresent);

            ViewBag.AbsentCount =
                db.Attendances.Count(a => !a.IsPresent);

            var recentStudents = db.Students
                .OrderByDescending(s => s.StudentId)
                .Take(5)
                .ToList();

            return View(recentStudents);
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