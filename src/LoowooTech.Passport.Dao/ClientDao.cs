using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class ClientDao : DaoBase
    {
        public IEnumerable<Client> GetClients(Paging page)
        {
            using (var db = GetDataContext())
            {

                var query = db.Client.Where(e => e.Deleted == 0);

                return query.OrderByDescending(e => e.ID).SetPage(page).ToList();
            }
        }

        public Client GetClient(string clientId)
        {
            if (string.IsNullOrEmpty(clientId)) return null;
            using (var db = GetDataContext())
            {
                return db.Client.Where(e => e.ClientId == clientId).FirstOrDefault();
            }
        }

        public void Create(Client client)
        {
            using (var db = GetDataContext())
            {
                db.Client.Add(client);

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Client.Where(e => e.ID == id).FirstOrDefault();
                entity.Deleted = 1;
                db.SaveChanges();
            }
        }


        public Client GetClient(int id)
        {
            using (var db = GetDataContext())
            {
                return db.Client.Where(e => e.ID == id).FirstOrDefault();
            }
        }

        public void Update(Client client)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Client.Where(e => e.ID == client.ID).FirstOrDefault();
                db.Entry(entity).CurrentValues.SetValues(client);
                db.SaveChanges();
            }
        }
    }
}
