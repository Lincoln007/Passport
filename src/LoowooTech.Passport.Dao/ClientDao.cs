using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class ClientDao : DaoBase
    {
        private Client ConvertEntity(APP_CLIENT entity)
        {
            if (entity == null) return null;
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

        public IEnumerable<Client> GetClients(Paging page)
        {
            var query = DB.APP_CLIENT.AsQueryable();
            //if (filter.Deleted.HasValue)
            //{
            //    query = query.Where(e => e.DELETED == (short)(filter.Deleted.Value ? 1 : 0));
            //}

            return query.SetPage(page).Select(e => ConvertEntity(e));
        }

        public Client GetClient(string clientId)
        {
            var entity = DB.APP_CLIENT.Where(e => e.CLIENT_ID == clientId).FirstOrDefault();

            return ConvertEntity(entity);
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
            var entity = DB.APP_CLIENT.Where(e => e.ID == id).FirstOrDefault();
            entity.DELETED = 1;
            DB.SaveChanges();
        }


        public Client GetClient(int id)
        {
            var entity = DB.APP_CLIENT.Where(e => e.ID == id).FirstOrDefault();
            return ConvertEntity(entity);
        }

        public void Update(Client client)
        {
            var entity = DB.APP_CLIENT.Where(e => e.ID == client.ID).FirstOrDefault();
            entity.NAME = client.Name;
            DB.SaveChanges();
        }
    }
}
