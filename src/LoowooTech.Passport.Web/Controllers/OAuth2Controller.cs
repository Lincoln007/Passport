using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{

    public class OAuth2Controller : ControllerBase
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

            return Redirect(redirect_url);

        }

        [ActionName("access_token")]
        public ActionResult AccessToken(string code)
        {
            var authCode = Core.AuthManager.GetAuthCode(code);
            if (authCode == null)
            {
                throw new HttpException(401, "access_denied");
            }

            var accessToken = Core.AuthManager.GetAccessToken(authCode);

            return Json(new
            {
                user = Core.AccountManager.GetAccount(authCode.AccountId),
                access_token = accessToken,
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
