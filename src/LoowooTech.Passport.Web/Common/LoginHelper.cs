using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using LoowooTech.Passport.Web.Models;

namespace LoowooTech.Passport.Web
{
    public static class LoginHelper
    {
        private static string SESSION_KEY = "lwt_user";

        public static void SaveLogin(this Account account, HttpContextBase context)
        {
            context.Session[SESSION_KEY] = account;
        }

        public static Account GetCurrentUser(this HttpContextBase context)
        {
            var obj = context.Session[SESSION_KEY];
            return obj == null ? null : (Account)obj;
        }
    }
}