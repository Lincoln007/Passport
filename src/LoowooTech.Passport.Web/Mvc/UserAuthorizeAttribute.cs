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
            var currentUser = httpContext.GetCurrentUser();
            httpContext.User = new UserPrincipal(new IdentityUser(currentUser));
            return currentUser.IsAuthenticated;
        }


        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            var returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
            var loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;
            filterContext.HttpContext.Response.Redirect(loginUrl + "?returnUrl=" + HttpUtility.UrlEncode(returnUrl));
        }
    }
}