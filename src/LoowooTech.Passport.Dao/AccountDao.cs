using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class AccountDao : DaoBase
    {
        public USER_ACCOUNT GetEntity(string username)
        {
            return DB.USER_ACCOUNT.FirstOrDefault(a => a.USERNAME.ToLower() == username.ToLower());
        }

        public USER_ACCOUNT GetEntity(int accountId)
        {
            return DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == accountId);
        }

        public bool HasAgent(int accountId, int agentId)
        {
            return DB.USER_ACCOUNT_AGENT.Any(a => a.ACCOUNT_ID == accountId && a.AGENT_ID == agentId);
        }

        public void Create(USER_ACCOUNT entity)
        {
            DB.USER_ACCOUNT.Add(entity);
            DB.SaveChanges();
        }

        public void Delete(int accountId)
        {
            var entity = DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == accountId);
            if (entity != null && entity.DELETED == 0)
            {
                entity.DELETED = 1;
                DB.SaveChanges();
            }
        }

        public void Update(USER_ACCOUNT entity)
        {
            var updateEntity = DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == entity.ID);
            updateEntity.USERNAME = entity.USERNAME;
            updateEntity.PASSWORD = entity.PASSWORD;
            updateEntity.TRUENAME = entity.TRUENAME;

            updateEntity.LAST_LOGIN_IP = entity.LAST_LOGIN_IP;
            updateEntity.LAST_LOGIN_TIME = entity.LAST_LOGIN_TIME;
            updateEntity.ROLE = entity.ROLE;
            updateEntity.DELETED = entity.DELETED;

            DB.SaveChanges();
        }


    }
}
