using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;
using LoowooTech.Passport.Common;

namespace LoowooTech.Passport.Dao
{
    public class AccountDao : DaoBase
    {
        private USER_ACCOUNT GetEntity(string username)
        {
            return DB.USER_ACCOUNT.FirstOrDefault(a => a.USERNAME.ToLower() == username.ToLower());
        }

        private Account ConvertEntity(USER_ACCOUNT entity)
        {
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

        private USER_ACCOUNT ConvertModel(Account model, USER_ACCOUNT entity)
        {
            entity.ID = model.AccountID;
            entity.CREATE_TIME = model.CreateTime;
            entity.LAST_LOGIN_IP = model.LastLoginIP;
            entity.LAST_LOGIN_TIME = model.LastLoginTime;
            entity.ROLE = (short)model.Role;
            entity.TRUENAME = model.TrueName;
            entity.USERNAME = model.Username;
            entity.PASSWORD = model.Password.MD5();
            entity.DELETED = (short)(model.Deleted ? 1 : 0);
            return entity;
        }

        public Account GetAccount(string username, string password, string agentUsername)
        {
            var entity = GetEntity(username);
            if (entity == null || entity.DELETED == 1)
            {
                throw new ArgumentException("用户名不存在！");
            }
            if (entity.PASSWORD.ToLower() != password.MD5())
            {
                throw new ArgumentException("密码不正确！");
            }

            var account = ConvertEntity(entity);

            if (!string.IsNullOrEmpty(agentUsername))
            {
                var agent = GetEntity(agentUsername);
                if (agent == null || agent.DELETED == 1)
                {
                    throw new ArgumentException("代理的用户名不存在！");
                }

                if (!DB.USER_ACCOUNT_AGENT.Any(a => a.ACCOUNT_ID == entity.ID && a.AGENT_ID == agent.ID))
                {
                    throw new ArgumentException("你没有被授权代理这个用户！");
                }

                account.AgentID = agent.ID;
            }

            return account;
        }

        public Account GetAccount(int accountId)
        {
            var entity = DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == accountId);
            return ConvertEntity(entity);
        }

        public void Create(Account account)
        {
            var entity = new USER_ACCOUNT();
            DB.USER_ACCOUNT.Add(ConvertModel(account, entity));
            DB.SaveChanges();
        }

        public void Delete(int accountId)
        {
            var entity = DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == accountId);
            if (entity != null && entity.DELETED == 0)
            {
                entity.DELETED = (short)1;
                DB.SaveChanges();
            }
        }

        public void Update(Account account)
        {
            var entity = DB.USER_ACCOUNT.FirstOrDefault(e => e.ID == account.AccountID);
            ConvertModel(account, entity);
            DB.SaveChanges();
        }


    }
}
