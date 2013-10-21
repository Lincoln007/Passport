using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using LoowooTech.Passport.Model;
using Oracle.ManagedDataAccess.Client;
namespace LoowooTech.Passport.Test
{
    [TestClass]
    public class DapperTest
    {
        private static string connectionString;

        private static OracleConnection conn;

        [TestInitialize]
        public void Init()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            conn = new OracleConnection(connectionString);
        }

        [TestMethod]
        public void TestDapperSelect()
        {
            var sql = @"SELECT * FROM USER_ACCOUNT WHERE ID = :ID";
            var account = conn.Query(sql, new { ID = 2 }).Select(d => new Account
            {
                AccountID = (int)d.ID,
                Username = d.USERNAME
            }).FirstOrDefault();

            Assert.AreEqual(2, account.AccountID);
            Assert.AreEqual("test", account.Username);
        }

        [TestMethod]
        public void TestDapperCreate()
        {
            var sql = @"INSERT INTO USER_ACCOUNT
(ID,USERNAME,PASSWORD,CREATE_TIME,LAST_LOGIN_TIME,LAST_LOGIN_IP,ROLE,TRUENAME)
VALUES
(ROWIDENTITY.NEXTVAL,:UserName,:Password,:CreateTime,:LastLoginTime,:LastLoginIP,:Role,:TrueName)";

            conn.Execute(sql, new
            {
                Username = "maddemon",
                Password = "123",
                CreateTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                LastLoginIp = "192.168.1.1",
                Role = (short)Role.User,
                TrueName = "Jim Zheng"
            });


        }

    }
}
