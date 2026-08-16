using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using StudentAdminPortal.Models;
using StudentAdminPortal.Filters;

namespace StudentAdminPortal.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ViewBag.TotalStudents = db.Students.Count();
                ViewBag.TotalCourses = db.Courses.Count();
                ViewBag.TotalSubjects = db.Subjects.Count();
                ViewBag.TotalMarks = db.Marks.Count();
                ViewBag.TotalAttendance = db.Attendances.Count();

                ViewBag.ActiveStudents =
                    db.Students.Count(s => s.Status == true);

                ViewBag.InactiveStudents =
                    db.Students.Count(s => s.Status == false);


                // Students by Course
                var studentsByCourse = db.Students
                    .GroupBy(s => s.Course.CourseName)
                    .Select(g => new
                    {
                        Course = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                ViewBag.CourseNames =
                    studentsByCourse.Select(x => x.Course).ToList();

                ViewBag.CourseStudentCounts =
                    studentsByCourse.Select(x => x.Count).ToList();


                // Grade Distribution
                var gradeDistribution = db.Marks
                    .GroupBy(m => m.Grade)
                    .Select(g => new
                    {
                        Grade = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.Grade)
                    .ToList();

                ViewBag.Grades =
                    gradeDistribution.Select(x => x.Grade).ToList();

                ViewBag.GradeCounts =
                    gradeDistribution.Select(x => x.Count).ToList();


                // Attendance Distribution
                var attendanceDistribution = db.Attendances
                    .GroupBy(a => a.IsPresent)
                    .Select(g => new
                    {
                        IsPresent = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                ViewBag.PresentCount = attendanceDistribution
                    .Where(x => x.IsPresent == true)
                    .Select(x => x.Count)
                    .FirstOrDefault();

                ViewBag.AbsentCount = attendanceDistribution
                    .Where(x => x.IsPresent == false)
                    .Select(x => x.Count)
                    .FirstOrDefault();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TestDatabase()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                int courseCount = db.Courses.Count();

                return Content("Database Connected! Courses: " + courseCount);
            }
        }
    }
}