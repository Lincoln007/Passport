using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Manager
{
    public class ClientManager : ManagerBase
    {
        public ClientManager(Core core) : base(core) { }

        private static readonly ClientDao Dao = new ClientDao();

        public Client GetClient(string clientId)
        {
            return Dao.GetClient(clientId);
        }
    }
}