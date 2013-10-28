using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Manager;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web
{
    public class AccessTokenBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new AccessTokenBinder();
        }

        class AccessTokenBinder : IModelBinder
        {

            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var accessToken = controllerContext.HttpContext.Request["access_token"];
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new HttpException(403, "invalid access_token");
                }

                var token = AccessToken.Create(accessToken);

                var agentId = 0;
                int.TryParse(controllerContext.HttpContext.Request["agentId"], out agentId);
                if (agentId > 0)
                {
                    if (agentId > 0)
                    {
                        var hasAgent = Core.Instance.AccountManager.HasAgent(token.AccountId, agentId);
                        if (!hasAgent)
                        {
                            throw new HttpException(401, string.Format("当前用户没有代理{0}用户的权限！", agentId));
                        }

                        token.AgentId = agentId;
                    }
                }

                return token;
            }
        }
    }
}