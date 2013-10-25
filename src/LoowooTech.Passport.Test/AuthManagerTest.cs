using LoowooTech.Passport.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LoowooTech.Passport.Model;
using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Common;

namespace LoowooTech.Passport.Test
{
    /// <summary>
    ///This is a test class for AuthManagerTest and is intended
    ///to contain all AuthManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AuthManagerTest
    {
        /// <summary>
        ///A test for GenerateCode
        ///</summary>
        [TestMethod()]
        public void GenerateCodeTest()
        {
            using (var db = new DBEntities())
            {
                var appClient = db.APP_CLIENT.Where(a => a.DELETED == 0).FirstOrDefault();
                var userAccount = db.USER_ACCOUNT.Where(e => e.DELETED == 0).FirstOrDefault();
                if (appClient != null && userAccount != null)
                {
                    var client = new ClientDao().GetClient(appClient.ID);

                    var code = Core.Instance.AuthManager.GenerateCode(client, userAccount.ID);

                    Assert.AreNotEqual(null, code);

                    var token = Core.Instance.AuthManager.GetAccessToken(new AuthorizeCode
                    {
                        AccountId = userAccount.ID,
                        ClientId = client.ClientId,
                        CreateTime = DateTime.Now
                    });

                    Assert.AreNotEqual(null, token);

                    var values = token.Token.FromHexString().AESDecrypt().Split('|');

                    Assert.AreEqual(values[0], client.ClientId);
                }
            }
        }
    }
}
