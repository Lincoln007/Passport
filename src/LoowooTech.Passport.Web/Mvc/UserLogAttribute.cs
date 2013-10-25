using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Manager;

namespace LoowooTech.Passport.Web
{
    internal class UserLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var userId = filterContext.HttpContext.User.Identity.Name;

            Core.Instance.LogManager.AddLog(new Model.OperateLog
            {
                AccountId = String.IsNullOrEmpty(userId) ? 0 : int.Parse(userId),
                Action = controllerName + "." + actionName,
                CreateTime = DateTime.Now
            });
        }
    }
}