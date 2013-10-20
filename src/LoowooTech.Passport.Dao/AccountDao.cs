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
        public Account GetAccount(string username, string password, string agentUsername)
        {
            var user = GetAccount(username);
            if (user == null || user.DELETED == 1)
            {
                throw new ArgumentException("用户名不存在！");
            }
            if (user.PASSWORD.ToLower() != password.MD5())
            {
                throw new ArgumentException("密码不正确！");
            }

            var account = new Account
            {
                ID = (int)user.ID,
                CreateTime = user.CREATE_TIME,
                LastLoginIP = user.LAST_LOGIN_IP,
                LastLoginTime = user.LAST_LOGIN_TIME,
                Role = (Role)user.ROLE,
                TrueName = user.TRUENAME,
                Username = user.USERNAME,
                Password = user.PASSWORD,
            };

            if (!string.IsNullOrEmpty(agentUsername))
            {
                var agent = GetAccount(agentUsername);
                if (agent == null || agent.DELETED == 1)
                {
                    throw new ArgumentException("代理的用户名不存在！");
                }

                if (!DB.USER_ACCOUNT_AGENT.Any(a => a.ACCOUNT_ID == user.ID && a.AGENT_ID == agent.ID))
                {
                    throw new ArgumentException("你没有被授权代理这个用户！");
                }

                account.AgentID = agent.ID;
            }

            return account;
        }

        private USER_ACCOUNT GetAccount(string username)
        {
            return DB.USER_ACCOUNT.FirstOrDefault(a => a.USERNAME.ToLower() == username.ToLower());
        }

        public Account GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public Account Create(Account account)
        {
            throw new NotImplementedException();
        }

        public void Delete(int accountId)
        {
            throw new NotImplementedException();
        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }


    }
}
