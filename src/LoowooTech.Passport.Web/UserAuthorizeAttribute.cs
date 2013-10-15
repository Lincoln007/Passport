using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Web
{
    public class UserAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var account = LoginHelper.GetLoginStatus(httpContext);

            return account != null && account.ID > 0;
        }


        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            var returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
            var loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;
            filterContext.HttpContext.Response.Redirect(loginUrl + "?returnUrl=" + HttpUtility.UrlEncode(returnUrl));
        }
    }
}