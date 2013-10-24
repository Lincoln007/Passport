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
            var data = Core.GroupManager.GetGroups(new GroupFilter
            {
                AccountId = accountId,
                Deleted = deleted
            }, page, pageSize);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? groupId)
        {
            var model = new Group();
            if (groupId.HasValue)
            {
                model = Core.GroupManager.GetGroup(groupId.Value);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(Group group)
        {
            Core.GroupManager.Save(group);
            return Success();
        }

        public JsonResult Delete(int groupId)
        {
            Core.GroupManager.Delete(groupId);
            return Success();
        }
    }
}
