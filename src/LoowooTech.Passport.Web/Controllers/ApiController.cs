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

        private ActionResult JsonSuccess()
        {
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccounts(string realName)
        {
            var list = Core.AccountManager.GetVAccounts(new AccountFilter { SearchKey = realName }, 1, int.MaxValue);
            return Json(list);
        }

        public ActionResult UpdatePassword([AccessTokenBinder]AccessToken token, string oldPassword, string newPassword)
        {
            var account = Core.AccountManager.GetAccount(token.AccountId);
            account = Core.AccountManager.GetAccount(account.Username, oldPassword);
            account.Password = newPassword;
            Core.AccountManager.Save(account);
            return JsonSuccess();
        }

        public ActionResult GetAgents([AccessTokenBinder]AccessToken token)
        {
            var agents = Core.AccountManager.GetAccountAgents(token.AccountId);
            return Json(agents);
        }

        public ActionResult SetAgents([AccessTokenBinder]AccessToken token, string[] usernames)
        {
            Core.AccountManager.UpdateAccountAgents(token.AccountId, usernames);
            return JsonSuccess();
        }
    }
}
