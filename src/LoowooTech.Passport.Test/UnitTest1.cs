using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using LoowooTech.Passport.Model;
using Oracle.ManagedDataAccess.Client;
namespace LoowooTech.Passport.Test
{
    [TestClass]
    public class UnitTest1
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
        public void DapperSelectTest()
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

    }
}
