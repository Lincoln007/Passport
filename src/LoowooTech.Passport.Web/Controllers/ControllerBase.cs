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

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 500;
            var ex = GetException(filterContext.Exception);
            filterContext.Result = Json(new
            {
                result = false,
                message = ex.Message,
                stackTrace = ex.StackTrace
            }, JsonRequestBehavior.AllowGet);
        }

        private Exception GetException(Exception exception)
        {
            var inner = exception.InnerException;
            if (inner != null)
            {
                return GetException(inner);
            }
            return exception;
        }
    }
}
