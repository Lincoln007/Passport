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

        public AccountManager AccountManager
        {
            get
            {
                return new AccountManager();
            }
        }

        public GroupManager GroupManager
        {
            get
            {
                return new GroupManager();
            }
        }

        public ClientManager ClientManager
        {
            get
            {
                return new ClientManager();
            }
        }

        public AuthManager AuthManager
        {
            get
            {
                return new AuthManager();
            }
        }


        public LogManager LogManager
        {
            get
            {
                return new LogManager();
            }
        }

        public DepartmentManager DepartmentManager
        {
            get
            {
                return new DepartmentManager();
            }
        }

    }
}