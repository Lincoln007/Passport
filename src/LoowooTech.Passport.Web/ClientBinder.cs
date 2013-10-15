using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web
{
    public class ClientBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new ClientBinder();
        }

        class ClientBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var client_id = controllerContext.HttpContext.Request.QueryString["client_id"];
                if (string.IsNullOrEmpty(client_id))
                {
                    throw new HttpException(403, "invalid_client");
                }
                var core = new Core();
                var client = core.ClientManager.GetClient(client_id);
                if (client == null)
                {
                    throw new HttpException(403, "invalid_client");
                }

                var actionName = controllerContext.RouteData.Values["Action"].ToString();
                if (actionName.ToLower() == "access_token")
                {
                    var clientSecret = controllerContext.HttpContext.Request["client_secret"];
                    if (string.IsNullOrEmpty(clientSecret) || client.ClientSecret.ToLower() != clientSecret.ToLower())
                    {
                        throw new HttpException(403, "invalid_client");
                    }
                }

                return client;
            }
        }
    }
}