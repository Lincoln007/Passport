using System;
using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using System.Linq;

namespace LoowooTech.Passport.Test
{
    [TestClass]
    public class TestAccountDao
    {
        private AccountDao dao = new AccountDao();

        [TestMethod]
        public void TestGetAccount()
        {
            var account = dao.GetAccount(1);
            Assert.AreNotEqual(null, account);
        }

        [TestMethod]
        public void TestCreate()
        {
            var account = new Account
            {
                Username = "maddemon" + DateTime.Now.Ticks,
                Password = "123",
                Role = Role.Administrator,
                TrueName = "JimZheng",
            };

            dao.Create(account);

            Assert.AreNotEqual(0, account.AccountId);
        }
    }
}
