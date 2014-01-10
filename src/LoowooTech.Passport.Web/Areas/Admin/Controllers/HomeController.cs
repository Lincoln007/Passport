using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
