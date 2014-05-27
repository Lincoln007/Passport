using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LoowooTech.Common;
using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Manager
{
    public class AccountManager : ManagerBase
    {
        private static readonly AccountDao Dao = new AccountDao();

        public PagingResult<VAccount> GetVAccounts(AccountFilter filter, int page = 1, int pageSize = 20)
        {
            var paging = new Paging
            {
                PageSize = pageSize,
                CurrentPage = page,
            };
            var data = Dao.GetVAccounts(filter, paging);
            return new PagingResult<VAccount>(paging, data);
        }

        //public PagingResult<Account> GetAccounts(AccountFilter filter, int page = 1, int pageSize = 20)
        //{
        //    var paging = new Paging
        //    {
        //        PageSize = pageSize,
        //        CurrentPage = page,
        //    };
        //    var data = Dao.GetAccounts(filter, paging);
        //    return new PagingResult<Account>(paging, data);
        //}

        public Account GetAccount(string username, string password, string agentUsername = null)
        {
            var account = Dao.GetAccount(username, password);
            if (account == null || account.Deleted == 1)
            {
                throw new ArgumentException("用户名或密码不正确！");
            }

            if ((Status)account.Status == Status.Disabled)
            {
                throw new ArgumentException("该用户登录功能已被关闭！");
            }

            if (!string.IsNullOrEmpty(agentUsername))
            {
                var agent = Dao.GetAccount(agentUsername);
                if (agent == null || agent.Deleted == 1)
                {
                    throw new ArgumentException("代理的用户名不存在！");
                }

                if (!Dao.HasAgent(account.AccountId, agent.AccountId))
                {
                    throw new ArgumentException("你没有被授权代理这个用户！");
                }

                account.Agent = agent;
            }

            return account;
        }

        public Account GetAccount(int accountId, int agentId = 0)
        {
            if (accountId == 0)
            {
                return null;
            }
            var account = Dao.GetAccount(accountId);
            if (agentId > 0)
            {
                if (Dao.HasAgent(accountId, agentId))
                {
                    account.Agent = Dao.GetAccount(agentId);
                }
            }
            return account;
        }

        public Account GetAccountAllInfo(int accountId, int agentId = 0)
        {
            var account = GetAccount(accountId);
            var department = Core.DepartmentManager.GetModel(account.DepartmentId);
            var rank = Core.RankManager.GetModel(account.RankId);
            account.Department = department == null ? null : department.Name;
            account.Rank = rank == null ? null : rank.Name;
            account.Rights = Core.GroupManager.GetAccountRightNames(accountId).ToArray();
            if (agentId > 0)
            {
                account.Agent = GetAccountAllInfo(agentId);
            }
            return account;
        }

        public void Save(Account account)
        {
            if (string.IsNullOrEmpty(account.Username))
            {
                throw new ArgumentException("用户名不能为空！");
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
                if (string.IsNullOrEmpty(account.Password))
                {
                    throw new ArgumentException("密码不能为空！");
                }
                Dao.Create(account);
            }

            UpdateCache(account);
        }

        private void UpdateCache(Account account)
        {
            var department = Core.DepartmentManager.GetModel(account.DepartmentId);
            var rank = Core.RankManager.GetModel(account.RankId);
            var vAccount = new VAccount
            {
                AccountId = account.AccountId,
                CreateTime = account.CreateTime,
                Username = account.Username,
                Deleted = account.Deleted,
                Department = department == null ? null : department.Name,
                Rank = rank == null ? null : rank.Name,
                TrueName = account.TrueName,
                Status = account.Status
            };
            UpdateCache(vAccount);
        }

        public void UpdateCache(VAccount account)
        {
            Cache.HSet("account_id", account.AccountId.ToString(), account);
            Cache.HSet("account_name", account.TrueName + "_" + account.Department + "_" + account.Rank, account);
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

        public void UpdateAccountAgents(int accountId, string[] usernames)
        {
            var users = Dao.GetAccounts(new AccountFilter { Usernames = usernames });
            Dao.UpdateAccountAgents(accountId, users.Select(e => e.AccountId).ToArray());
        }

        public List<Account> GetAccountAgents(int accountId)
        {
            return Dao.GetAccountAgents(accountId);
        }


    }
}