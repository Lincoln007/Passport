using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web
{
    public class IdentityUser : Account, System.Security.Principal.IIdentity
    {
        public IdentityUser(Account account)
        {
            this.ID = account.ID;
            this.Username = account.Username;
            this.Password = account.Password;
            this.LastLoginTime = account.LastLoginTime;
            this.LastLoginIP = account.LastLoginIP;
            this.Agent = account.Agent;
        }

        public string AuthenticationType
        {
            get { return "Web.Session"; }
        }

        public string Name
        {
            get { return ID.ToString(); }
        }
    }
}