using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class AccountDao : DaoBase
    {
        public List<VAccount> GetVAccounts(AccountFilter filter, Paging page = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.VAccount.AsQueryable();
                if (filter.Deleted.HasValue)
                {
                    var deletedValue = (short)(filter.Deleted.Value ? 1 : 0);
                    query = query.Where(e => e.Deleted == deletedValue);
                }

                if (filter.Enabled.HasValue)
                {
                    var enabledValue = (short)(filter.Enabled.Value ? Status.Enabled : Status.Disabled);
                    query = query.Where(e => e.Status == enabledValue);
                }

                if (!string.IsNullOrEmpty(filter.SearchKey))
                {
                    query = query.Where(e => e.TrueName.Contains(filter.SearchKey) || e.Username.Contains(filter.SearchKey));
                }

                if (filter.BeginTime.HasValue)
                {
                    query = query.Where(e => e.CreateTime > filter.BeginTime.Value);
                }

                if (filter.EndTime.HasValue)
                {
                    query = query.Where(e => e.CreateTime <= filter.EndTime.Value);
                }
                if (filter.Usernames != null)
                {
                    query = query.Where(e => filter.Usernames.Contains(e.Username));
                }
                if (filter.AccountIds != null)
                {
                    query = query.Where(e => filter.AccountIds.Contains(e.AccountId));
                }

                return query.OrderByDescending(e => e.AccountId).SetPage(page).ToList();
            }
        }

        public List<Account> GetAccounts(AccountFilter filter, Paging page = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.Account.AsQueryable();
                if (filter.Deleted.HasValue)
                {
                    var deletedValue = (short)(filter.Deleted.Value ? 1 : 0);
                    query = query.Where(e => e.Deleted == deletedValue);
                }

                if (filter.Enabled.HasValue)
                {
                    var enabledValue = (short)(filter.Enabled.Value ? Status.Enabled : Status.Disabled);
                    query = query.Where(e => e.Status == enabledValue);
                }

                if (!string.IsNullOrEmpty(filter.SearchKey))
                {
                    query = query.Where(e => e.TrueName.Contains(filter.SearchKey) || e.Username.Contains(filter.SearchKey));
                }

                if (filter.BeginTime.HasValue)
                {
                    query = query.Where(e => e.CreateTime > filter.BeginTime.Value);
                }

                if (filter.EndTime.HasValue)
                {
                    query = query.Where(e => e.CreateTime <= filter.EndTime.Value);
                }
                if (filter.Usernames != null)
                {
                    query = query.Where(e => filter.Usernames.Contains(e.Username));
                }
                if (filter.AccountIds != null)
                {
                    query = query.Where(e => filter.AccountIds.Contains(e.AccountId));
                }

                return query.OrderByDescending(e => e.AccountId).SetPage(page).ToList();
            }
        }

        public Account GetAccount(string username, string password = null)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Account.Where(a => a.Username.ToLower() == username.ToLower()).FirstOrDefault();
                if (entity == null)
                {
                    return null;
                }
                if (!string.IsNullOrEmpty(password))
                {
                    if (entity.Password != Account.GetEncyptPassword(password, entity.CreateTime))
                    {
                        return null;
                    }
                }

                return entity;
            }
        }
        public Account GetAccount(int accountId)
        {
            using (var db = GetDataContext())
            {
                return db.Account.FirstOrDefault(e => e.AccountId == accountId);
            }
        }

        public bool HasAgent(int accountId, int agentId)
        {
            using (var db = GetDataContext())
            {
                return db.AccountAgent.Any(e => e.AccountId == agentId && e.AgentId == accountId);
            }
        }
        public void Create(Account account)
        {
            using (var db = GetDataContext())
            {
                var existUser = db.Account.Count(e => e.Username.ToLower() == account.Username.ToLower()) > 0;
                if (existUser)
                {
                    throw new ArgumentException("用户名已被占用！");
                }

                if (string.IsNullOrEmpty(account.Password))
                {
                    throw new ArgumentNullException("密码没有填写！");
                }

                var password = account.Password;
                account.Password = Account.GetEncyptPassword(password, account.CreateTime);

                db.Account.Add(account);

                db.SaveChanges();
                account.Password = password;
            }
        }

        public void Delete(int accountId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Account.Where(e => e.AccountId == accountId).FirstOrDefault();
                if (entity != null && entity.Deleted == 0)
                {
                    entity.Deleted = 1;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Account account)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Account.Where(e => e.AccountId == account.AccountId).FirstOrDefault();
                if (entity == null)
                {
                    throw new ArgumentException("参数错误，没找到这个帐号");
                }
                account.CreateTime = entity.CreateTime;
                if (string.IsNullOrEmpty(account.Password))
                {
                    account.Password = entity.Password;
                }
                else
                {
                    account.Password = Account.GetEncyptPassword(account.Password, entity.CreateTime);
                }

                db.Entry(entity).CurrentValues.SetValues(account);

                db.SaveChanges();
            }
        }

        public void UpdateAccountAgents(int accountId, int[] agentIds)
        {
            using (var db = GetDataContext())
            {
                var olds = db.AccountAgent.Where(e => e.AccountId == accountId);
                foreach (var item in olds)
                {
                    db.AccountAgent.Remove(item);
                }
                foreach (var newId in agentIds)
                {
                    db.AccountAgent.Add(new AccountAgent
                    {
                        AccountId = accountId,
                        AgentId = newId
                    });
                }
                db.SaveChanges();
            }
        }

        public List<Account> GetAccountAgents(int accountId)
        {
            using (var db = GetDataContext())
            {
                var accountIds = db.AccountAgent.Where(e => e.AccountId == accountId).Select(e => e.AgentId).ToArray();
                return GetAccounts(new AccountFilter { AccountIds = accountIds });
            }
        }
    }
}
