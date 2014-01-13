using LoowooTech.Passport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class DepartmentController : AdminControllerBase
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetList()
        {
            var data = Core.DepartmentManager.GetTree();
            return JsonContent(data);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0, int clientId = 0)
        {
            var model = Core.DepartmentManager.GetModel(id) ?? new Department();
            ViewBag.Tree = Core.DepartmentManager.GetTree(clientId);
            ViewBag.Clients = Core.ClientManager.GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Department model)
        {
            Core.DepartmentManager.Save(model);
            return JsonSuccess(model);
        }

        public ActionResult Delete(int id = 0)
        {
            Core.DepartmentManager.Delete(id);
            return JsonSuccess();
        }
    }
}
