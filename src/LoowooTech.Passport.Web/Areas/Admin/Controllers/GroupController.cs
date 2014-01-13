using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class GroupController : AdminControllerBase
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetList(int accountId = 0)
        {
            var data = Core.GroupManager.GetGroups(accountId);

            return JsonContent(data);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var model = Core.GroupManager.GetGroup(id) ?? new Group();
            ViewBag.Rights = string.Join("\n", model.Rights.Select(e => e.Name));
            ViewBag.Clients = Core.ClientManager.GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Group group, string rights)
        {
            if (!string.IsNullOrEmpty(rights))
            {
                group.Rights = rights.Replace("\r", "").Split('\n').Select(s => new GroupRight
                {
                    Name = s,
                    GroupID = group.GroupId
                });
            }
            Core.GroupManager.Save(group);
            return JsonSuccess();
        }

        public ActionResult Delete(int id)
        {
            Core.GroupManager.Delete(id);
            return JsonSuccess();
        }
    }
}
