using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class ClientController : AdminControllerBase
    {
        public ActionResult List(int page = 1, int pageSize = 20)
        {
            ViewBag.Data = Core.ClientManager.GetClients(page, pageSize);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var model = new Client();
            if (id.HasValue)
            {
                model = Core.ClientManager.GetClient(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(Client client)
        {
            Core.ClientManager.Save(client);
            return Success();
        }

        public JsonResult Delete(int id)
        {
            Core.ClientManager.Delete(id);
            return Success();
        }

    }
}
