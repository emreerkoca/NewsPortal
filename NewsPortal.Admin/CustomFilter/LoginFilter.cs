using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewsPortal.Admin.CustomFilter
{
    public class LoginFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpContextWrapper wrapper = new HttpContextWrapper(HttpContext.Current);
            var sessionControl = filterContext.HttpContext.Session["UserID"];
            if(sessionControl == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}