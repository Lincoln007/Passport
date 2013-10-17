using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Web
{
    public class CurrentUser : System.Security.Principal.IIdentity
    {
        public int AccountId { get; set; }

        public int AgentId { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }
        
        public string AuthenticationType
        {
            get { return "Web.Session"; }
        }

        public string Name
        {
            get { return AccountId.ToString(); }
        }

        public bool IsAuthenticated
        {
            get { return AccountId > 0; }
        }
    }
}