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

        public ActionResult GetAccountsByIds([AccessTokenBinder]AccessToken token, string accountIds)
        {
            var ids = accountIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id=>int.Parse(id)).ToArray();
            var result = Core.AccountManager.GetVAccounts(new AccountFilter { AccountIds = ids }, 1, int.MaxValue);

            return Json(result.List.Select(e => new
            {
                e.TrueName,
                e.Department,
                e.Rank,
                e.AccountId,
                e.Username
            }), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAccountsByDisplayNames([AccessTokenBinder]AccessToken token, string displayNames)
        {
            var result = new List<VAccount>();
            var names = displayNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var displayName in names)
            {
                var nameDescs = displayName.Split('_');
                var temp = new Account();
                temp.Department = nameDescs[0];
                if (nameDescs.Length > 1)
                {
                    temp.Rank = nameDescs[1];
                }
                if (nameDescs.Length > 2)
                {
                    temp.TrueName = nameDescs[2];
                }

                result.AddRange(Core.AccountManager.Search(temp));
            }

            return Json(result.Select(e => new
            {
                e.TrueName,
                e.Department,
                e.Rank,
                e.AccountId,
                e.Username
            }), JsonRequestBehavior.AllowGet);
        }
    }
}
