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
        public static void UserLogin(this HttpContextBase context, Account account)
        {
            var ticketName = account.AccountId + "|" + (int)account.Role + "|" + account.AgentId + "|" + account.Username;

            var ticket = new FormsAuthenticationTicket(account.AccountId.ToString(), false, 30);

            context.Response.Cookies.Set(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));

        }

        public static void UserLogout(this HttpContextBase context)
        {
            var cookie = context.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddYears(-1);
            context.Response.SetCookie(cookie);
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