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
        [TestMethod]
        public void TestGetAccount()
        {

        }

        [TestMethod]
        public void TestCreate()
        {
            var dao = new AccountDao();
            var account = new Account
            {
                Username = "maddemon" + DateTime.Now.Ticks,
                Password = "123",
                Role = Role.Administrator,
                TrueName = "JimZheng",
            };

            dao.Create(account);

            var id = dao.GetConnection().Query("SELECT ACCOUNT_ID.CURRVAL AS ID FROM USER_ACCOUNT").FirstOrDefault();

            Assert.AreNotEqual(0, id.ID);
        }
    }
}
