using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Manager
{
    public class ClientManager : ManagerBase
    {
        public ClientManager(Core core) : base(core) { }

        public Client GetClient(string clientId)
        {
            throw new NotImplementedException();
        }
    }
}