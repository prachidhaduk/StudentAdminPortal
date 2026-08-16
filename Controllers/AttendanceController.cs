using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using StudentAdminPortal.Models;
using StudentAdminPortal.Filters;

namespace StudentAdminPortal.Controllers
{
    [AdminAuthorize]
    public class AttendanceController : Controller
    {
        private ApplicationDbContext db =
            new ApplicationDbContext();


        // GET: Attendance
        public ActionResult Index()
        {
            var attendance = db.Attendances
                .Include("Student")
                .OrderByDescending(a => a.AttendanceDate)
                .ToList();

            return View(attendance);
        }


        // GET: Attendance/Create
        public ActionResult Create()
        {
            LoadStudents();

            return View();
        }


        // POST: Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            LoadStudents(attendance.StudentId);

            return View(attendance);
        }


        // GET: Attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest);
            }

            Attendance attendance =
                db.Attendances.Find(id);

            if (attendance == null)
            {
                return HttpNotFound();
            }

            LoadStudents(attendance.StudentId);

            return View(attendance);
        }


        // POST: Attendance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State =
                    EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            LoadStudents(attendance.StudentId);

            return View(attendance);
        }


        // GET: Attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    System.Net.HttpStatusCode.BadRequest);
            }

            Attendance attendance =
                db.Attendances
                .Include("Student")
                .FirstOrDefault(a =>
                    a.AttendanceId == id);

            if (attendance == null)
            {
                return HttpNotFound();
            }

            return View(attendance);
        }


        // POST: Attendance/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance =
                db.Attendances.Find(id);

            if (attendance != null)
            {
                db.Attendances.Remove(attendance);

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // Student dropdown
        private void LoadStudents(int? selectedStudent = null)
        {
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