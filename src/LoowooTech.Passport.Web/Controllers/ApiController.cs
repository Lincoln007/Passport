using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{
    public class ApiController : ControllerBase
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.HttpContext.Response.StatusCode = filterContext.Exception.GetStatusCode();
            filterContext.ExceptionHandled = true;
            filterContext.Result = Json(new { result = false, message = filterContext.Exception.Message });
        }
        
        
        public JsonResult GetUserInfo([AccessTokenBinder]AccessToken token)
        {
            var account = Core.AccountManager.GetAccount(token.AccountId);

            return Json(account, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRights([AccessTokenBinder]AccessToken token, string rightNames)
        {
            var result = Core.GroupManager.CheckRights(rightNames.Split(','), token.AccountId, token.AgentId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePassword([AccessTokenBinder]AccessToken token, string oldPassword, string newPassword)
        {
            var account = Core.AccountManager.GetAccount(token.AccountId);
            account = Core.AccountManager.GetAccount(account.Username, oldPassword);
            account.Password = newPassword;
            Core.AccountManager.Save(account);
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
