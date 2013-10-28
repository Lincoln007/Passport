using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{
    public class ApiController : ControllerBase
    {
        public JsonResult GetUserInfo([AccessTokenBinder]AccessToken token)
        {
            var account = Core.AccountManager.GetAccount(token.AccountId);

            return Json(account, JsonRequestBehavior.AllowGet);
        }
    }
}
