using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web.Areas.Admin.Controllers
{
    public class RankController : AdminControllerBase
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetList()
        {
            return JsonContent(Core.RankManager.GetList());
        }

        public ActionResult Update(int id = 0, string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("名称没有填写");
            }
            if (id > 0)
            {
                Core.RankManager.Update(new Model.Rank { ID = id, Name = name });
            }
            else
            {
                Core.RankManager.Add(name);
            }
            return JsonSuccess();
        }

        public ActionResult Delete(int id = 0)
        {
            Core.RankManager.Delete(id);
            return JsonSuccess();
        }
    }
}
