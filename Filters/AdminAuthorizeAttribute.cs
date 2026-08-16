using System.Web.Mvc;
using System.Web.Routing;

namespace StudentAdminPortal.Filters
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(
            ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserId"] == null)
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Account" },
                            { "action", "Login" }
                        });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}