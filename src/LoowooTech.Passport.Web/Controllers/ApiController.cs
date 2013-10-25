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

        public JsonResult GetRightInfo([AccessTokenBinder]AccessToken token, string rightNames, int agentId = 0)
        {
            var groups = Core.GroupManager.GetGroups(token.AccountId);
            var levels = Core.GroupManager.GetRightLevels(groups, rightNames.Split(','), RightLevel.SelfRight);

            if (agentId > 0 && Core.AccountManager.HasAgent(token.AccountId,agentId))
            {
                groups = Core.GroupManager.GetGroups(agentId);
                var agentLevels = Core.GroupManager.GetRightLevels(groups, rightNames.Split(','), RightLevel.AgentRight);

                //TODO：重复的权限移除，以代理权限为主。
                foreach (var kv in agentLevels)
                {
                    if (levels.ContainsKey(kv.Key))
                    {
                        levels.Remove(kv.Key);
                    }
                    else
                    {
                        levels.Add(kv.Key,kv.Value);
                    }
                }
            }

            return Json(levels, JsonRequestBehavior.AllowGet);
        }
    }
}
