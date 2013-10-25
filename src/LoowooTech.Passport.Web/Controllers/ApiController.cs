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
            var selfRights = Core.GroupManager.GetRightLevels(groups, rightNames.Split(','), RightLevel.SelfRight);

            if (agentId > 0 && Core.AccountManager.HasAgent(token.AccountId,agentId))
            {
                groups = Core.GroupManager.GetGroups(agentId);
                var agentRights = Core.GroupManager.GetRightLevels(groups, rightNames.Split(','), RightLevel.AgentRight);

                //SelfRights和AgentRights键值完全重复，如果Agent键值不为Agent级别，则用Self键值代替
                foreach (var kv in agentRights)
                {
                    if (agentRights[kv.Key] == RightLevel.AgentRight)
                    {
                        selfRights[kv.Key] = agentRights[kv.Key];
                    }
                }
            }

            return Json(selfRights, JsonRequestBehavior.AllowGet);
        }
    }
}
