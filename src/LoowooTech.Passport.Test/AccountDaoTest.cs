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
            using (var db = DbHelper.GetDataContext())
            {
                var data = db.Account.Where(e => e.Username.Contains("maddemon")).FirstOrDefault();

                var account = target.GetAccount(data.Username);

                Assert.AreEqual(data.Username, account.Username);
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
                Username = "admin",
                Password = "admin",
                Role = (short)Role.Administrator,
                TrueName = "Admin",
            };
            target.Create(account);

            var expect = target.GetAccount(account.Username, "123");

            Assert.AreNotEqual(0, expect.AccountId);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            var account = target.GetAccounts(new AccountFilter { Deleted = false }).FirstOrDefault();
            account.Deleted = 1;
            target.Delete(account.AccountId);
            var expect = target.GetAccount(account.AccountId);

            Assert.AreEqual(1, expect.Deleted);
        }

        ///// <summary>
        /////A test for HasAgent
        /////</summary>
        //[TestMethod()]
        //public void HasAgentTest()
        //{
        //    using (var db = DbHelper.GetDataContext())
        //    {
        //        var data = db.USER_ACCOUNT_AGENT.FirstOrDefault();
        //        if (data != null)
        //        {
        //            var expect = target.HasAgent(data.ACCOUNT_ID, data.AGENT_ID);
        //            Assert.AreEqual(true, expect);
        //        }
        //    }
        //}

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Account account = null;

            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Account.FirstOrDefault();
                account = target.GetAccount(entity.AccountId);
                account.Username = "test" + DateTime.Now.Ticks;
                target.Update(account);
            }


            using (var db = DbHelper.GetDataContext())
            {
                var expect = db.Account.Where(e => e.AccountId == account.AccountId).FirstOrDefault();
                Assert.AreEqual(expect.Username, account.Username);
            }
        }

        /// <summary>
        ///A test for GetAccounts
        ///</summary>
        [TestMethod()]
        public void GetAccountsTest()
        {
            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Account.FirstOrDefault();
                entity.Deleted = 1;
                entity.Status = (short)Status.Disabled;
                db.SaveChanges();

                var list = target.GetAccounts(new AccountFilter { Deleted = true, Enabled = false });
                Assert.AreNotEqual(0, list.Count());
            }
        }

    }
}
