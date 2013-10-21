using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Manager
{
    public class AccountManager : ManagerBase
    {
        public AccountManager(Core core) : base(core) { }

        private Account ConvertEntity(USER_ACCOUNT entity)
        {
            return new Account
            {
                //AccountID = entity.ID,
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
            entity.PASSWORD = model.EncyptedPassword;
            entity.DELETED = (short)(model.Deleted ? 1 : 0);
            return entity;
        }
        
        private AccountDao Dao = new AccountDao();

        public Account GetAccount(string username, string password, string agentUsername = null)
        {
            var entity = Dao.GetEntity(username);
            if (entity == null || entity.DELETED == 1)
            {
                throw new ArgumentException("用户名不存在！");
            }
            if (entity.PASSWORD.ToLower() != Account.EncyptPassword(password))
            {
                throw new ArgumentException("密码不正确！");
            }

            var account = ConvertEntity(entity);

            if (!string.IsNullOrEmpty(agentUsername))
            {
                var agent = Dao.GetEntity(agentUsername);
                if (agent == null || agent.DELETED == 1)
                {
                    throw new ArgumentException("代理的用户名不存在！");
                }

                if (!Dao.HasAgent(account.AccountID, agent.ID))
                {
                    throw new ArgumentException("你没有被授权代理这个用户！");
                }

                account.AgentID = agent.ID;
            }

            return account;
        }

        public Account GetAccount(int accountId)
        {
            return ConvertEntity(Dao.GetEntity(accountId));
        }

        public Account Create(Account account)
        {
            var entity = new USER_ACCOUNT();
            Dao.Create(ConvertModel(account, entity));
            account.AccountID = entity.ID;
            return account;
        }


        public void Delete(int accountId)
        {
            Dao.Delete(accountId);
        }

        private string GetRandomString(int length = 8)
        {
            var random = new Random();
            var sb = new StringBuilder();
            while (sb.Length < length)
            {
                var val = random.Next('0', 'z');
                if ((val > '9' && val < 'A')
                    ||(val > 'Z' && val <'a')
                    )
                {
                    continue;
                }

                sb.Append((char)val);
            }
            return sb.ToString();
        }

        public string ResetPassword(int accountId)
        {
            var entity = Dao.GetEntity(accountId);
            var newPassword = GetRandomString();
            entity.PASSWORD = Account.EncyptPassword(newPassword);
            Dao.Update(entity);
            return newPassword;
        }
    }
}