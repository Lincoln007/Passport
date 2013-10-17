using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Manager;

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

        private ClientManager _clientManager;
        public ClientManager ClientManager
        {
            get
            {
                if (_clientManager == null) _clientManager = new ClientManager();

                return _clientManager;
            }
        }

        private AuthManager _authManager;
        public AuthManager AuthManager
        {
            get
            {
                if (_authManager == null) _authManager = new AuthManager();

                return _authManager;
            }
        }


    }
}