using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class AccountController : AdminControllerBase
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetList(string searchkey, bool? deleted, bool? enabled, DateTime? beginTime, DateTime? endTime, int page = 1, int rows = 20)
        {
            var list = Core.AccountManager.GetAccounts(new AccountFilter
            {
                Deleted = deleted,
                Enabled = enabled,
                SearchKey = searchkey,
                BeginTime = beginTime,
                EndTime = endTime
            }, page, rows);

            return JsonContent(list);
        }


        [HttpGet]
        public ActionResult Edit(int? accountId)
        {
            var model = new Account();
            if (accountId.HasValue)
            {
                model = Core.AccountManager.GetAccount(accountId.Value);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            Core.AccountManager.Save(account);
            return JsonSuccess();
        }

        public ActionResult Delete(int accountId)
        {
            Core.AccountManager.Delete(accountId);
            return JsonSuccess(null, "账号已被删除！");
        }

        public ActionResult ResetPassword(int accountId)
        {
            var newPassword = Core.AccountManager.ResetPassword(accountId);
            return JsonSuccess(new { password = newPassword });
        }
    }
}
