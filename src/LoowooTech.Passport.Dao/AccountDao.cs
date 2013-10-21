using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class AccountDao : DaoBase
    {
        private Account ConvertEntity(USER_ACCOUNT entity)
        {
            if (entity == null) return null;
            return new Account
            {
                AccountID = entity.ID,
                CreateTime = entity.CREATE_TIME,
                LastLoginIP = entity.LAST_LOGIN_IP,
                LastLoginTime = entity.LAST_LOGIN_TIME,
                Role = (Role)entity.ROLE,
                TrueName = entity.TRUENAME,
                Username = entity.USERNAME,
                Password = entity.PASSWORD,
                Deleted = entity.DELETED == 1
            };
        }

        private USER_ACCOUNT ConvertModel(Account model, USER_ACCOUNT entity = null)
        {
            if (entity == null) entity = new USER_ACCOUNT();
            entity.ID = model.AccountID;
            entity.CREATE_TIME = model.CreateTime;
            entity.LAST_LOGIN_IP = model.LastLoginIP;
            entity.LAST_LOGIN_TIME = model.LastLoginTime;
            entity.ROLE = (short)model.Role;
            entity.TRUENAME = model.TrueName;
            entity.USERNAME = model.Username;
            entity.PASSWORD = model.EncyptedPassword;
            entity.DELETED = (short)(model.Deleted ? 1 : 0);
            return entity;
        }

        public Account GetAccount(string username)
        {
            var entity = DB.USER_ACCOUNT.FirstOrDefault(a => a.USERNAME.ToLower() == username.ToLower());
            return ConvertEntity(entity);
        }

        public Account GetAccount(int accountId)
        {
            var entity = DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == accountId);
            return ConvertEntity(entity);
        }

        public bool HasAgent(int accountId, int agentId)
        {
            return DB.USER_ACCOUNT_AGENT.Any(a => a.ACCOUNT_ID == accountId && a.AGENT_ID == agentId);
        }

        public void Create(Account account)
        {
            var db = new DBEntities();
            var entity = ConvertModel(account);
            db.USER_ACCOUNT.Add(entity);
            db.SaveChanges();

            account.AccountID = entity.ID;
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

        public void Update(Account account)
        {
            var entity = DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == account.AccountID);
            if (entity == null)
            {
                throw new ArgumentException("参数错误，没找到这个帐号");
            }

            entity = ConvertModel(account, entity);

            DB.SaveChanges();
        }


    }
}
