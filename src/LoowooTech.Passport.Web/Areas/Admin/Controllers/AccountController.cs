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

        public ActionResult List(int page = 1)
        {
            return View();
        }

        public ActionResult Edit(Account account)
        {
            return View();
        }

        public ActionResult Delete(int accountId)
        {
            return View();
        }

        public ActionResult ResetPassword(int accountId)
        {
            return View();
        }

        public ActionResult AccountAgent(int accountId)
        {
            return View();
        }
    }
}
