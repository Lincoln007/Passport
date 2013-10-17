﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace LoowooTech.Passport.Web
{
    public class UserPrincipal : IPrincipal
    {
        public UserPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}