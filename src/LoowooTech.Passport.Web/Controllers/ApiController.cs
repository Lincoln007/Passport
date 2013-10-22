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

        public JsonResult HasRight([AccessTokenBinder]AccessToken token, string rightNames, int agentId = 0)
        {
            var groups = Core.GroupManager.GetGroups(token.AccountId);
            if (Core.GroupManager.HasRights(groups, rightNames.Split(',')))
            {
                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }

            if (agentId > 0 && Core.AccountManager.HasAgent(token.AccountId,agentId))
            {
                groups = Core.GroupManager.GetGroups(agentId);
                if (Core.GroupManager.HasRights(groups, rightNames.Split(',')))
                {
                    return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}
