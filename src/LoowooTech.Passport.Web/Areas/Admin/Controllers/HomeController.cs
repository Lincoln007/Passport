using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            RefreshCache();
            return View();
        }

        public ActionResult Cache()
        {
            Core.AccountManager.CreateCache();
            return Content("缓存刷新完毕");
        }

        private void RefreshCache()
        {
            new Thread(() =>
            {
                try
                {
                    Core.AccountManager.CreateCache();
                }
                catch
                {
                }
            }).Start();
        }
    }
}
