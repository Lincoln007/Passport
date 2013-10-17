using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web
{
    public static class LoginHelper
    {
        private static string KEY = "lwt_user";

        public static void SaveLogin(this Account account, HttpContextBase context)
        {
            var ticketName = account.ID + "|" + (int)account.Role + "|" + account.Agent.ID + "|" + account.Username;

            var ticket = new FormsAuthenticationTicket(account.ID.ToString(), false, 30);

            context.Response.Cookies.Set(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));

        }

        public static CurrentUser GetCurrentUser(this HttpContextBase context)
        {
            var user = new CurrentUser();
            var cookie = context.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (cookie == null) return user;

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null) return user;

            var values = ticket.Name.Split('|');
            if (values.Length == 4)
            {
                user.AccountId = int.Parse(values[0]);
                user.Role = (Role)int.Parse(values[1]);
                user.AgentId = int.Parse(values[2]);
                user.Username = values[3];
            }
            return user;
        }
    }
}