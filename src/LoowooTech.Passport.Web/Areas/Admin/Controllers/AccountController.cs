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
        public ActionResult Edit(int id = 0)
        {
            var model = Core.AccountManager.GetAccount(id) ?? new Account();
            ViewBag.Departments = Core.DepartmentManager.GetTree();
            ViewBag.Groups = Core.GroupManager.GetGroups(0);
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
