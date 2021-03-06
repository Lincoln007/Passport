﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{

    public class AuthController : ControllerBase
    {
        [UserAuthorize]
        public ActionResult Authroize([ClientBinder]Client client, string redirect_url)
        {
            var uri = new Uri(redirect_url);
            if (!client.Hosts.Contains(uri.Host))
            {
                throw new HttpException(403, "redirect_uri_mismatch");
            }

            var account = Core.AccountManager.GetAccount(CurrentUser.AccountId, CurrentUser.AgentId);

            var returnUrl = Core.AuthManager.GetAppendedCodeReturnUrl(client, account, redirect_url);

            return Redirect(returnUrl);

        }

        [ActionName("access_token")]
        public ActionResult AccessToken([ClientBinder]Client client, string code)
        {
            var authCode = Core.AuthManager.GetAuthorizeCode(code);
            if (authCode == null)
            {
                throw new HttpException(401, "access_denied");
            }

            var accessToken = Core.AuthManager.GetAccessToken(authCode);
            if (accessToken == null)
            {
                throw new HttpException(401, "invalid arguments");
            }

            var account = Core.AccountManager.GetAccountAllInfo(accessToken.AccountId, accessToken.AgentId);
            return Json(new
            {
                user = account,
                agent = account.Agent,
                access_token = accessToken.Token,
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
