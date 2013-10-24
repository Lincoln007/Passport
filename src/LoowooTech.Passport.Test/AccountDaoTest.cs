using LoowooTech.Passport.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LoowooTech.Passport.Model;
using System.Collections.Generic;

namespace LoowooTech.Passport.Test
{


    /// <summary>
    ///This is a test class for AccountDaoTest and is intended
    ///to contain all AccountDaoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AccountDaoTest
    {
        private AccountDao target = new AccountDao();
        /// <summary>
        ///A test for GetAccount
        ///</summary>
        [TestMethod()]
        public void GetAccountTest()
        {
            using (var db = new DBEntities())
            {
                var data = db.USER_ACCOUNT.Where(e => e.USERNAME.Contains("maddemon")).FirstOrDefault();

                var account = target.GetAccount(data.USERNAME);

                Assert.AreEqual(data.USERNAME, account.Username);
            }
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void CreateTest()
        {
            Account account = new Account
            {
                Username = "maddemon" + DateTime.Now.Ticks,
                Password = "123",
                Role = Role.Administrator,
                TrueName = "jim",
            };
            target.Create(account);

            var expect = target.GetAccount(account.Username);

            Assert.AreNotEqual(0, expect.AccountId);

        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            var account = target.GetAccounts(new AccountFilter { Deleted = false }).FirstOrDefault();
            account.Deleted = true;
            target.Delete(account.AccountId);
            var expect = target.GetAccount(account.AccountId);

            Assert.AreEqual(true, expect.Deleted);
        }

        /// <summary>
        ///A test for HasAgent
        ///</summary>
        [TestMethod()]
        public void HasAgentTest()
        {
            using (var db = new DBEntities())
            {
                var data = db.USER_ACCOUNT_AGENT.FirstOrDefault();
                if (data != null)
                {
                    var expect = target.HasAgent(data.ACCOUNT_ID, data.AGENT_ID);
                    Assert.AreEqual(true, expect);
                }
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Account account = null;

            using (var db = new DBEntities())
            {
                var entity = db.USER_ACCOUNT.FirstOrDefault();
                account = target.GetAccount(entity.ID);
                account.Username = "test" + DateTime.Now.Ticks;
                target.Update(account);
            }


            using (var db = new DBEntities())
            {
                var expect = db.USER_ACCOUNT.Where(e => e.ID == account.AccountId).FirstOrDefault();
                Assert.AreEqual(expect.USERNAME, account.Username);
            }
        }

        /// <summary>
        ///A test for GetAccounts
        ///</summary>
        [TestMethod()]
        public void GetAccountsTest()
        {
            using (var db = new DBEntities())
            {
                var entity = db.USER_ACCOUNT.FirstOrDefault();
                entity.DELETED = 1;
                entity.STATUS = (short)Status.Disabled;
                db.SaveChanges();

                var list = target.GetAccounts(new AccountFilter { Deleted = true, Enabled = false });
                Assert.AreNotEqual(0, list.Count());
            }
        }

    }
}
