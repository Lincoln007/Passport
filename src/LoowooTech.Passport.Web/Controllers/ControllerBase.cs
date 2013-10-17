using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Controllers
{
    public class ControllerBase : AsyncController
    {
        protected static Core Core = new Core();

        protected Account CurrentUser
        {
            get
            {
                return (Account)HttpContext.User;
            }
        }
    }
}
