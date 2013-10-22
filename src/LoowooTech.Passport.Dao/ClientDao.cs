using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class ClientDao : DaoBase
    {
        public Client GetClient(string clientId)
        {
            var entity = DB.APP_CLIENT.Where(e => e.CLIENT_ID == clientId).FirstOrDefault();

            return new Client
            {
                ID = entity.ID,
                ClientId = entity.CLIENT_ID,
                Deleted = entity.DELETED == 1,
                ClientSecret = entity.CLIENT_SECRET,
                CreateTime = entity.CREATE_TIME,
                Hosts = entity.HOSTS
            };
        }

        public void Create(Client client)
        {
            var entity = new APP_CLIENT
            {
                CLIENT_ID = client.ClientId,
                CLIENT_SECRET = client.ClientSecret,
                HOSTS = client.Hosts,
                CREATE_TIME = client.CreateTime,
            };

            DB.APP_CLIENT.Add(entity);

            DB.SaveChanges();
            client.ID = entity.ID;
        }

        public void Delete(int id)
        {
            var entity = DB.APP_CLIENT.FirstOrDefault(e => e.ID == id);
            entity.DELETED = 1;
            DB.SaveChanges();
        }

    }
}
