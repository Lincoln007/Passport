using LoowooTech.Passport.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LoowooTech.Passport.Model;

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
            var account = target.GetAccounts(new SelectFilter { SearchKey = "maddemon" }).Take(1).FirstOrDefault();
            
            var username = account.Username;

            var actual = target.GetAccount(username);
            Assert.AreEqual(username, actual.Username);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void CreateTest()
        {
            Account account = new Account
            {
                Username = "maddemon",
                Password = "123",
                Role = Role.Administrator,
                TrueName = "jim",
            };
            target.Create(account);
            Assert.AreNotEqual(0, account.AccountId);
            
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {

            int accountId = 0; // TODO: Initialize to an appropriate value
            target.Delete(accountId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetAccount
        ///</summary>
        [TestMethod()]
        public void GetAccountTest1()
        {

            int accountId = 0; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = target.GetAccount(accountId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAccounts
        ///</summary>
        [TestMethod()]
        public void GetAccountsTest()
        {

            var filter = new SelectFilter { Deleted = true};
            var account = target.GetAccounts(filter).Take(1).FirstOrDefault();
            if (account != null)
            {
                Assert.AreEqual(true, account.Deleted);
            }
            filter = new SelectFilter { Enabled = false };
            account = target.GetAccounts(filter).Take(1).FirstOrDefault();
            if (account != null)
            {
                Assert.AreEqual(Status.Disabled, account.Status);
            }
        }

        /// <summary>
        ///A test for HasAgent
        ///</summary>
        [TestMethod()]
        public void HasAgentTest()
        {

            int accountId = 0; // TODO: Initialize to an appropriate value
            int agentId = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasAgent(accountId, agentId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {

            Account account = null; // TODO: Initialize to an appropriate value
            target.Update(account);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
