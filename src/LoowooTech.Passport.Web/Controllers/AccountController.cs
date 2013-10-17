using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{
    [UserRole]
    public class AccountController : ControllerBase
    {
        [UserRole(Role = Role.Everyone)]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [UserRole(Role = Role.Everyone)]
        [HttpPost]
        public ActionResult Login(string username, string password, string agentUsername, string returnUrl = "/admin")
        {
            var user = Core.AccountManager.GetAccount(username, password, agentUsername);
            if (user == null)
            {
                throw new ArgumentException("用户名或密码有误！");
            }

            HttpContext.UserLogin(user);

            return Redirect(returnUrl);
        }

        public ActionResult Logout(string returnUrl = "/")
        {
            HttpContext.UserLogout();
            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditPassword(string oldPassword,string newPassword,string rePassword)
        {
            return View();
        }
    }
}
