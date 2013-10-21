using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Manager;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{
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
            base.OnException(filterContext);
        }
    }
}
