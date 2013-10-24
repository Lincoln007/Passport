using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class AuthDao : DaoBase
    {
        private AccessToken ConvertEntity(AUTH_TOKEN entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new AccessToken
            {
                ID = entity.ID,
                AccountId = entity.ACCOUNT_ID,
                ClientId = entity.CLIENT_ID,
                CreateTime = entity.CREATE_TIME,
                Token = entity.TOKEN
            };
        }

        public AccessToken GetAccessToken(string clientId, int accountId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.AUTH_TOKEN.Where(e => e.CLIENT_ID == clientId && e.ACCOUNT_ID == accountId).FirstOrDefault();
                if (entity == null)
                {
                    entity = new AUTH_TOKEN
                    {
                        ACCOUNT_ID = accountId,
                        CLIENT_ID = clientId,
                        CREATE_TIME = DateTime.Now,
                    };

                    entity.TOKEN = AccessToken.GenerateToken(entity.CLIENT_ID, entity.ACCOUNT_ID, entity.CREATE_TIME);

                    db.AUTH_TOKEN.Add(entity);
                    db.SaveChanges();
                }
                return ConvertEntity(entity);
            }
        }

        public void Create(AccessToken token)
        {
            var entity = new AUTH_TOKEN
            {
                CLIENT_ID = token.ClientId,
                ACCOUNT_ID = token.AccountId,
                CREATE_TIME = token.CreateTime,
                TOKEN = token.Token,
            };

            using (var db = GetDataContext())
            {
                db.AUTH_TOKEN.Add(entity);
                db.SaveChanges();

                token.ID = entity.ID;

            }
        }
    }
}
