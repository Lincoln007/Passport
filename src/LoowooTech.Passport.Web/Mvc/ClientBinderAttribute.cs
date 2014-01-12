using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Manager;

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

                var client = Core.Instance.ClientManager.GetModel(client_id);
                if (client == null)
                {
                    throw new HttpException(403, "invalid_client");
                }

                return client;
            }
        }
    }
}