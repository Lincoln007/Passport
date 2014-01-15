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

            var code = Core.AuthManager.GenerateCode(client, CurrentUser.AccountId);
            if (redirect_url.Contains("?"))
            {
                redirect_url += "&code=" + code;
            }
            else
            {
                redirect_url += "?code=" + code;
            }
            return Redirect(redirect_url);

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

            var account = Core.AccountManager.GetAccount(accessToken.AccountId);
            var department = Core.DepartmentManager.GetModel(account.DepartmentId);
            var allRights = Core.GroupManager.GetAllRightLevels(account.AccountId, account.AgentId);
            return Json(new
            {
                user = account,
                department = department == null ? null : department.Name,
                rights = allRights,
                access_token = accessToken.Token,
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
