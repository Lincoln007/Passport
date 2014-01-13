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
        private static readonly ClientDao Dao = new ClientDao();

        public Client GetModel(string clientId)
        {
            return Dao.GetModel(clientId);
        }

        public List<Client> GetList()
        {
            return Dao.GetList();
        }

        public void Delete(int id)
        {
            Dao.Delete(id);
        }

        public void Save(Client client)
        {
            if (string.IsNullOrEmpty(client.Name))
            {
                throw new ArgumentException("应用名称没有填写");
            }

            if (client.ID > 0)
            {
                Dao.Update(client);
            }
            else
            {
                Dao.Create(client);
            }
        }

        public Client GetModel(int id)
        {
            if (id == 0) return null;
            return Dao.GetClient(id);
        }
    }
}