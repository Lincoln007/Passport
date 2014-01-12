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
        public ActionResult List(int? accountId, bool? deleted, int page = 1, int pageSize = 20)
        {
            return View();
        }

        public ActionResult GetList(int? accountId, bool? deleted, int page = 1, int rows = 20)
        {
            var data = Core.GroupManager.GetGroups(new GroupFilter
            {
                AccountId = accountId,
                Deleted = deleted
            }, page, rows);

            return JsonContent(data);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var model = Core.GroupManager.GetGroup(id) ?? new Group();
            ViewBag.Rights = string.Join("\n", model.Rights.Select(e => e.Name));
            ViewBag.Clients = Core.ClientManager.GetClients();
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
