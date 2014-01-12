using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Common;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    [UserAuthorize]
    [UserRole(Role = Role.Administrator)]
    public class AdminControllerBase : LoowooTech.Passport.Web.Controllers.ControllerBase
    {
        protected ActionResult JsonSuccess(dynamic data = null, string message = null)
        {
            return Content(new { result = true, data, message }.ToJson());
        }

        public ActionResult JsonContent(object data)
        {
            return Content(data.ToJson());
        }
    }
}
