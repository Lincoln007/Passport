using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Manager;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{
    [UserLog]
    public class ControllerBase : AsyncController
    {
        protected Core Core = Core.Instance;

        protected CurrentUser CurrentUser
        {
            get
            {
                return (CurrentUser)HttpContext.User;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.HttpContext.Response.StatusCode = filterContext.Exception.GetStatusCode();
            filterContext.ExceptionHandled = true;
            if (filterContext.HttpContext.IsAjaxRequest())
            {
                filterContext.Result = Json(new { result = false, message = filterContext.Exception.Message });
            }
            else
            {
                filterContext.Result = View("Error", filterContext.Exception);
            }
            base.OnException(filterContext);
        }
    }
}
