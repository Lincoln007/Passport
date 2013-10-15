using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.Passport.Web.Controllers
{
    [UserAuthorize]
    public class OAuth2Controller : Controller
    {
        public ActionResult Authroize(string client_id, string redirect_url)
        {
            throw new NotImplementedException();
        }


        public ActionResult AccessToken(string client_id, string client_secret, string code)
        {
            throw new NotImplementedException();
        }


    }
}
