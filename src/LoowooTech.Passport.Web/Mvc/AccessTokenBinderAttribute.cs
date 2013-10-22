using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

                return AccessToken.Create(accessToken);
            }
        }
    }
}