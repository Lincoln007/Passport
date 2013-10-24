using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminControllerBase
    {
        public ActionResult Login()
        {
            return Redirect("~/Account/Login");
        }

        public ActionResult List(string searchkey, bool? deleted, bool? enabled, DateTime? beginTime, DateTime? endTime, int page = 1, int pageSize = 20)
        {

            ViewBag.Data = Core.AccountManager.GetAccounts(new AccountFilter
            {
                Deleted = deleted,
                Enabled = enabled,
                SearchKey = searchkey,
                BeginTime = beginTime,
                EndTime = endTime
            }, page, pageSize);

            return View();
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
        public JsonResult Edit(Account account)
        {
            Core.AccountManager.Save(account);
            return Success();
        }

        public JsonResult Delete(int accountId)
        {
            Core.AccountManager.Delete(accountId);
            return Success(null, "账号已被删除！");
        }

        public JsonResult ResetPassword(int accountId)
        {
            var newPassword = Core.AccountManager.ResetPassword(accountId);
            return Success(new { password = newPassword });
        }
    }
}
