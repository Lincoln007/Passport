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
                AccountId = entity.ID,
                CreateTime = entity.CREATE_TIME,
                LastLoginIP = entity.LAST_LOGIN_IP,
                LastLoginTime = entity.LAST_LOGIN_TIME,
                Role = (Role)entity.ROLE,
                TrueName = entity.TRUENAME,
                Username = entity.USERNAME,
                //Password = entity.PASSWORD,
                Deleted = entity.DELETED == 1,
                Status = (Status)entity.STATUS
            };
        }

        private USER_ACCOUNT ConvertModel(Account model, USER_ACCOUNT entity = null)
        {
            if (entity == null) entity = new USER_ACCOUNT();
            entity.ID = model.AccountId;
            entity.CREATE_TIME = model.CreateTime;
            entity.LAST_LOGIN_IP = model.LastLoginIP;
            entity.LAST_LOGIN_TIME = model.LastLoginTime;
            entity.ROLE = (short)model.Role;
            entity.TRUENAME = model.TrueName;
            entity.USERNAME = model.Username;
            if (!string.IsNullOrEmpty(model.Password))
            {
                entity.PASSWORD = model.EncyptedPassword;
            }
            entity.DELETED = (short)(model.Deleted ? 1 : 0);
            entity.STATUS = (short)model.Status;
            return entity;
        }

        public IEnumerable<Account> GetAccounts(AccountFilter filter, Paging page = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.USER_ACCOUNT.AsQueryable();
                if (filter.Deleted.HasValue)
                {
                    query = query.Where(e => e.DELETED == (short)(filter.Deleted.Value ? 1 : 0));
                }

                if (filter.Enabled.HasValue)
                {
                    query = query.Where(e => e.STATUS == (short)(filter.Enabled.Value ? Status.Enabled : Status.Disabled));
                }

                if (!string.IsNullOrEmpty(filter.SearchKey))
                {
                    query = query.Where(e => e.TRUENAME.Contains(filter.SearchKey) || e.USERNAME.Contains(filter.SearchKey));
                }

                if (filter.BeginTime.HasValue)
                {
                    query = query.Where(e => e.CREATE_TIME > filter.BeginTime.Value);
                }

                if (filter.EndTime.HasValue)
                {
                    query = query.Where(e => e.CREATE_TIME <= filter.EndTime.Value);
                }

                return query.OrderByDescending(e => e.ID).SetPage(page).ToList().Select(e => ConvertEntity(e));
            }
        }

        public Account GetAccount(string username, string password = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.USER_ACCOUNT.Where(a => a.USERNAME.ToLower() == username.ToLower());
                if (!string.IsNullOrEmpty(password))
                {
                    query = query.Where(e => e.PASSWORD == Account.EncyptPassword(e.PASSWORD, e.CREATE_TIME));
                }

                var entity = query.FirstOrDefault();

                return ConvertEntity(entity);
            }
        }
        public Account GetAccount(int accountId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.USER_ACCOUNT.FirstOrDefault(e => e.ID == accountId);
                return ConvertEntity(entity);
            }
        }

        public bool HasAgent(int accountId, int agentId)
        {
            using (var db = GetDataContext())
            {
                return db.USER_ACCOUNT_AGENT.Any(a => a.ACCOUNT_ID == accountId && a.AGENT_ID == agentId);
            }
        }
        public void Create(Account account)
        {
            using (var db = GetDataContext())
            {
                var existUser = db.USER_ACCOUNT.Count(e => e.USERNAME.ToLower() == account.Username.ToLower()) > 0;
                if (existUser)
                {
                    throw new ArgumentException("用户名已被占用！");
                }

                if (string.IsNullOrEmpty(account.Password))
                {
                    throw new ArgumentNullException("密码没有填写！");
                }

                var entity = ConvertModel(account);

                db.USER_ACCOUNT.Add(entity);
                db.SaveChanges();

                account.AccountId = entity.ID;
            }
        }

        public void Delete(int accountId)
        {
            using (var db = new DBEntities())
            {
                var entity = db.USER_ACCOUNT.Where(e => e.ID == accountId).FirstOrDefault();
                if (entity != null && entity.DELETED == 0)
                {
                    entity.DELETED = 1;
                    db.SaveChanges();
                }
            }
        }

        public void Update(Account account)
        {
            using (var db = GetDataContext())
            {
                var entity = db.USER_ACCOUNT.Where(e => e.ID == account.AccountId).FirstOrDefault();
                if (entity == null)
                {
                    throw new ArgumentException("参数错误，没找到这个帐号");
                }

                entity = ConvertModel(account, entity);

                db.SaveChanges();
            }
        }


    }
}
