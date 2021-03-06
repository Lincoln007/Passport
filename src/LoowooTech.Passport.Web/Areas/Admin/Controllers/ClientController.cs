﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class ClientController : AdminControllerBase
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetList()
        {
            return JsonContent(Core.ClientManager.GetList());
        }

        [HttpGet]
        public ActionResult Edit(int id =0)
        {
            var model = Core.ClientManager.GetModel(id) ?? new Client();
            ViewBag.Departments = Core.DepartmentManager.GetTree();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            Core.ClientManager.Save(client);
            return JsonSuccess();
        }

        public ActionResult Delete(int id)
        {
            Core.ClientManager.Delete(id);
            return JsonSuccess();
        }

    }
}
