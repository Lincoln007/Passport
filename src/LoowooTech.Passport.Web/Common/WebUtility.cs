using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Web
{
    public static class WebUtility
    {
        public static bool IsAjaxRequest(this HttpContextBase context)
        {
            return context.Request.Headers["Ajax_Request"] == "1";
            throw new NotImplementedException();
        }

        public static int GetStatusCode(this Exception ex)
        {
            return 500;
            throw new NotImplementedException();
        }
    }
}