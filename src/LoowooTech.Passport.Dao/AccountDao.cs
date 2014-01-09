using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class AccountDao : DaoBase
    {
        public IEnumerable<Account> GetAccounts(AccountFilter filter, Paging page = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.Account.AsQueryable();
                if (filter.Deleted.HasValue)
                {
                    query = query.Where(e => e.Deleted == (short)(filter.Deleted.Value ? 1 : 0));
                }

                if (filter.Enabled.HasValue)
                {
                    query = query.Where(e => e.Status == (short)(filter.Enabled.Value ? Status.Enabled : Status.Disabled));
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

                return query.OrderByDescending(e => e.AccountId).SetPage(page).ToList();
            }
        }

        public Account GetAccount(string username, string password = null)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Account.Where(a => a.Username.ToLower() == username.ToLower()).FirstOrDefault();
                if (!string.IsNullOrEmpty(password))
                {
                    if (entity.Password != Account.EncyptPassword(password, entity.CreateTime))
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
                return db.Account.Any(a => a.AccountId == accountId && a.AgentId == agentId);
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


                db.Account.Add(account);

                db.SaveChanges();

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

                db.Entry(entity).CurrentValues.SetValues(account);
                if (!string.IsNullOrEmpty(account.Password))
                {
                    entity.Password = account.EncyptedPassword;
                }

                db.SaveChanges();
            }
        }


    }
}
