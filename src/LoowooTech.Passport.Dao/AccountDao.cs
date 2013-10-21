using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;
using Dapper;
using System.Dynamic;

namespace LoowooTech.Passport.Dao
{
    public class AccountDao : DaoBase
    {
        private Account ConvertData(dynamic entity)
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

        public Account GetAccount(string username)
        {
            using (var conn = GetConnection())
            {
                var data = conn.Query("SELECT * FROM USER_ACCOUNT WHERE USERNAME = :Username", new
                {
                    Username = username
                }).FirstOrDefault();

                return ConvertData(data);
            }
        }

        public Account GetAccount(long accountId)
        {
            using (var conn = GetConnection())
            {
                var data = conn.Query("SELECT * FROM USER_ACCOUNT WHERE ID = :AccountId", new
                {
                    AccountId = accountId
                }).FirstOrDefault();

                return ConvertData(data);
            }
        }

        public bool HasAgent(long accountId, long agentId)
        {
            using (var conn = GetConnection())
            {
                var sql = "SELECT COUNT(1) as C1 FROM USER_ACCOUNT_AGENT WHERE ACCOUNT_ID = :AccountId and AGENT_ID = :AgentId";
                return conn.Query(sql, new
                {
                    AccountId = accountId,
                    AgentId = agentId
                }).Select(d => new { Count = d.C1 }).FirstOrDefault().Count > 0;
            }
        }

        public void Create(Account model)
        {

            using (var conn = GetConnection())
            {
                var countRow = conn.Query("SELECT COUNT(1) as C1 FROM USER_ACCOUNT WHERE USERNAME = :Username", new
                {
                    model.Username
                }).Select(d => new { Count= d.C1 }).FirstOrDefault();

                if (countRow.Count > 0)
                {
                    throw new ArgumentException("用户名已被占用！");
                }

                var insertSql = @"
INSERT INTO USER_ACCOUNT
(ID,USERNAME,PASSWORD,CREATE_TIME,LAST_LOGIN_TIME,LAST_LOGIN_IP,ROLE,TRUENAME)
VALUES
(ACCOUNT_ID.NEXTVAL,:UserName,:Password,:CreateTime,:LastLoginTime,:LastLoginIP,:Role,:TrueName)";
                conn.Execute(insertSql, new
                {
                    model.CreateTime,
                    model.LastLoginIP,
                    model.LastLoginTime,
                    ROLE = (short)model.Role,
                    model.TrueName,
                    model.Username,
                    Password = model.EncyptedPassword
                });
            }
        }

        public void Delete(long accountId)
        {
            using (var conn = GetConnection())
            {
                conn.Execute(@"DELETE FROM USER_ACCOUNT WHERE ID = :id", new { id = accountId });
            }
        }

        public void Update(Account account)
        {
            using (var conn = GetConnection())
            {
                var sql = @"
UPDATE USER_ACCOUNT
SET
USERNAME = :Username,
PASSWORD = :Password,
TRUENAME = :TrueName,
CREATE_TIME = :CreateTime,
LAST_LOGIN_TIME = :LastLoginTime,
LAST_LOGIN_IP = : LastLoginIp,
ROLE = :Role
WHERE ID = :id
";
                conn.Execute(sql, new
                {
                    id = account.AccountID,
                    account.Username,
                    account.EncyptedPassword,
                    account.TrueName,
                    account.CreateTime,
                    account.LastLoginTime,
                    account.LastLoginIP,
                    Role = (int)account.Role
                });
            }
        }


    }
}
