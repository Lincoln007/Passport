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

        public ActionResult List(string searchkey, bool? deleted, bool? enabled, int page = 1, int pageSize = 20)
        {

            ViewBag.Data = Core.AccountManager.GetAccounts(new SelectFilter
            {
                Deleted = deleted,
                Enabled = enabled,
                SearchKey = searchkey
            }, page, pageSize);

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? accountId)
        {
            if (accountId.HasValue)
            {
                ViewBag.Account = Core.AccountManager.GetAccount(accountId.Value);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            return View();
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
