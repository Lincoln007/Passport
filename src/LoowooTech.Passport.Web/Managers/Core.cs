using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Web.Managers;

namespace LoowooTech.Passport.Web
{
    public class Core
    {
        private AccountManager _accountManager;
        public AccountManager AccountManager
        {
            get
            {
                if (_accountManager == null) _accountManager = new AccountManager();

                return _accountManager;
            }
        }
    }
}