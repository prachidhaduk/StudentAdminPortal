using System.Linq;
using System.Web.Mvc;
using StudentAdminPortal.Models;

namespace StudentAdminPortal.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db =
            new ApplicationDbContext();


        // GET: Account/Login
        public ActionResult Login(bool logout = false)
        {
            if (logout)
            {
                ViewBag.LogoutMessage =
                    "You have been logged out successfully.";
            }

            return View();
        }


        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error =
                    "Please enter username and password.";

                return View();
            }


            User user = db.Users.FirstOrDefault(u =>
                u.Username == username &&
                u.Password == password &&
                u.IsActive);


            if (user == null)
            {
                ViewBag.Error =
                    "Invalid username or password.";

                return View();
            }


            // Create login session
            Session["UserId"] = user.UserId;
            Session["Username"] = user.Username;
            Session["FullName"] = user.FullName;
            Session["Role"] = user.Role;


            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        // GET: Account/Logout
        public ActionResult Logout()
        {
            // Clear login session
            Session.Clear();
            Session.Abandon();

            // Send logout message to Login page
            return RedirectToAction("Login", "Account",
                new { logout = "true" });
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