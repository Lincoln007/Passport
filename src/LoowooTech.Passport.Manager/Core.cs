using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Manager
{
    public class Core
    {
        private Core()
        { }

        public static Core Instance = new Core();

        private AccountManager _accountManager;
        public AccountManager AccountManager
        {
            get
            {
                if (_accountManager == null) _accountManager = new AccountManager(this);

                return _accountManager;
            }
        }

        private ClientManager _clientManager;
        public ClientManager ClientManager
        {
            get
            {
                if (_clientManager == null) _clientManager = new ClientManager(this);

                return _clientManager;
            }
        }

        private AuthManager _authManager;
        public AuthManager AuthManager
        {
            get
            {
                if (_authManager == null) _authManager = new AuthManager(this);

                return _authManager;
            }
        }


    }
}