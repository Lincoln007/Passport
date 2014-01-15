﻿using System;
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
        public ActionResult Login(string return_url = "/", string client_id = null, string css = null)
        {
            var client = Core.ClientManager.GetModel(client_id);
            var user = HttpContext.GetCurrentUser();
            if (user.IsAuthenticated)
            {
                ViewBag.ReturnUrl = Core.AuthManager.GetAppendedCodeReturnUrl(client, user.AccountId, return_url);
                return View("LoginResult");
            }
            ViewBag.ClientId = client_id;
            ViewBag.CssUrl = css;
            ViewBag.ReturnUrl = HttpUtility.UrlEncode(return_url);
            return View();
        }

        [UserRole(Role = Role.Everyone)]
        [HttpPost]
        public ActionResult LoginResult(string clientId, string username, string password, string agentUsername, string returnUrl = "/")
        {
            var user = Core.AccountManager.GetAccount(username, password, agentUsername);
            if (user == null)
            {
                throw new ArgumentException("用户名或密码有误！");
            }

            HttpContext.UserLogin(user);


            if ((Role)user.Role == Role.Administrator && returnUrl == "/")
            {
                returnUrl = "/admin";
            }
            else
            {
                var client = Core.ClientManager.GetModel(clientId);
                if (client == null)
                {
                    throw new ArgumentException("Client_Id参数错误");
                }
                returnUrl = Core.AuthManager.GetAppendedCodeReturnUrl(client, user.AccountId, returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        public ActionResult Logout(string returnUrl = "/")
        {
            HttpContext.UserLogout();
            return Redirect(returnUrl);
        }
    }
}
