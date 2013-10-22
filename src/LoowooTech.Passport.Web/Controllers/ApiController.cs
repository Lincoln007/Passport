using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web.Controllers
{
    public class ApiController : ControllerBase
    {
        public JsonResult GetUserInfo(string access_token)
        {
            throw new NotImplementedException();
        }

        public JsonResult HasRight(string access_token, string rightName)
        {
            throw new NotImplementedException();
        }
    }
}
