using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web.Controllers
{
    public class ControllerBase : AsyncController
    {
        protected static Core Core = new Core();
    }
}
