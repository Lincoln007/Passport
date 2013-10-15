using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Web.Models;

namespace LoowooTech.Passport.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl = "/admin")
        {
            var user = Core.AccountManager.Get(username, password);
            if (user == null)
            {
                throw new ArgumentException("用户名或密码有误！");    
            }

            user.SaveLoginStatus(HttpContext);

            return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}
