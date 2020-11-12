using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportShop.Controllers
{
    public class SessionCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = filterContext.HttpContext.Session;

            if (session != null && session.GetString("Admin") == null)
            {
                ((Controller) filterContext.Controller).TempData["AdminErrorMessage"] =
                    "Sorry, this page is for admins only. Log in now!";
                filterContext.Result = new RedirectResult("/Login");
            }
        }
    }
}