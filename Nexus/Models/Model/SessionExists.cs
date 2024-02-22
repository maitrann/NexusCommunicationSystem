using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nexus.Models.Model
{
    public class SessionExists : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var en = filterContext.HttpContext.Session;
            if (en.GetString("Customer") != null)
            {
                filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary {
                                { "Controller", "MainPage" },
                                { "Action", "Index" }
                              });
            }

        }

    }
}
