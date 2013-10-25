using LoowooTech.Passport.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LoowooTech.Passport.Model;
using LoowooTech.Passport.Dao;

namespace LoowooTech.Passport.Test
{


    /// <summary>
    ///This is a test class for AccountManagerTest and is intended
    ///to contain all AccountManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AccountManagerTest
    {
        /// <summary>
        ///A test for ResetPassword
        ///</summary>
        [TestMethod()]
        public void ResetPasswordTest()
        {
            using (var db = new DBEntities())
            {
                var entity = db.USER_ACCOUNT.Where(e => e.DELETED == 0).FirstOrDefault();
                var newpw = Core.Instance.AccountManager.ResetPassword(entity.ID);
                var account = Core.Instance.AccountManager.GetAccount(entity.USERNAME, newpw);
                Assert.AreNotEqual(null, account);
                Assert.AreEqual(entity.ID, account.AccountId);
            }
        }
    }
}
