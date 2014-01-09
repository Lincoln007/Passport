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

        public Client GetClient(string clientId)
        {
            return Dao.GetClient(clientId);
        }

        public PagingResult<Client> GetClients(int page, int pageSize)
        {
            var paging = new Paging(page, pageSize);
            var list = Dao.GetClients(paging);
            return new PagingResult<Client>(paging, list);
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
            {
                Dao.Create(client);
            }
        }

        public Client GetClient(int id)
        {
            return Dao.GetClient(id);
        }
    }
}