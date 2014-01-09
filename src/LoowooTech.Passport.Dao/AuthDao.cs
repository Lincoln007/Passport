using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class AuthDao : DaoBase
    {
        public AccessToken GetAccessToken(string clientId, int accountId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.AccessToken.Where(e => e.ClientId == clientId && e.AccountId == accountId).FirstOrDefault();
                if (entity == null)
                {
                    entity = new AccessToken
                    {
                        AccountId = accountId,
                        ClientId = clientId,
                        CreateTime = DateTime.Now,
                    };

                    entity.Token = AccessToken.GenerateToken(entity.ClientId, entity.AccountId, entity.CreateTime);

                    db.AccessToken.Add(entity);
                    db.SaveChanges();
                }
                return entity;
            }
        }

        public void Create(AccessToken token)
        {
            using (var db = GetDataContext())
            {
                db.AccessToken.Add(token);
                db.SaveChanges();
            }
        }
    }
}
