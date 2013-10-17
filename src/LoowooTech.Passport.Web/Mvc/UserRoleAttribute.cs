using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web
{
    public class UserRoleAttribute : ActionFilterAttribute
    {
        public UserRoleAttribute()
        {
            Role = Model.Role.User;
        }

        public Role Role { get; set; }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var currentUser = (CurrentUser)Thread.CurrentPrincipal.Identity;
            if (Role == Model.Role.Everyone)
            {
                return;
            }

            if (Role <= currentUser.Role)
            {
                return;
            }
            else
            {
                throw new HttpException(401, "你没有权限查看此页面");
            }
        }
    }
}