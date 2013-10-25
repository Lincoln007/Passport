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

        private static readonly AccountDao Dao = new AccountDao();

        public PagingResult<Account> GetAccounts(AccountFilter filter, int page = 1, int pageSize = 20)
        {
            var paging = new Paging
            {
                PageSize = pageSize,
                CurrentPage = page,
            };
            var data = Dao.GetAccounts(filter, paging);
            return new PagingResult<Account>(paging, data);
        }

        public Account GetAccount(string username, string password, string agentUsername = null)
        {
            var account = Dao.GetAccount(username, password);
            if (account == null || account.Deleted)
            {
                throw new ArgumentException("用户名或密码不正确！");
            }

            if (account.Status == Status.Disabled)
            {
                throw new ArgumentException("该用户登录功能已被关闭！");
            }

            if (!string.IsNullOrEmpty(agentUsername))
            {
                var agent = Dao.GetAccount(agentUsername);
                if (agent == null || agent.Deleted)
                {
                    throw new ArgumentException("代理的用户名不存在！");
                }

                if (!Dao.HasAgent(account.AccountId, agent.AccountId))
                {
                    throw new ArgumentException("你没有被授权代理这个用户！");
                }

                account.AgentId = agent.AccountId;
            }

            return account;
        }

        public Account GetAccount(int accountId)
        {
            return Dao.GetAccount(accountId);
        }

        public void Save(Account account)
        {
            if (string.IsNullOrEmpty(account.Username))
            {
                throw new ArgumentException("用户名不能为空！");
            }

            if (string.IsNullOrEmpty(account.Password))
            {
                throw new ArgumentException("密码不能为空！");
            }


            if (account.AccountId > 0)
            {
                var tmp = Dao.GetAccount(account.Username);
                if (tmp.AccountId != account.AccountId)
                {
                    throw new ArgumentException("用户名已被使用！");
                }

                Dao.Update(account);
            }
            else
            {
                Dao.Create(account);
            }
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
                    || (val > 'Z' && val < 'a')
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
            var account = Dao.GetAccount(accountId);
            account.Password = GetRandomString();
            Dao.Update(account);
            return account.Password;
        }

        public bool HasAgent(int accountId, int agentId)
        {
            return Dao.HasAgent(accountId, agentId);
        }
    }
}