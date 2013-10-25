using LoowooTech.Passport.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LoowooTech.Passport.Model;
using System.Collections.Generic;

namespace LoowooTech.Passport.Test
{
    [TestClass()]
    public class ClientDaoTest
    {
        private ClientDao target = new ClientDao();
        /// <summary>
        ///A test for GetClients
        ///</summary>
        [TestMethod()]
        public void GetClientsTest()
        {
            var list = target.GetClients(null);
            Assert.AreNotEqual(0, list.Count());
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Client client = null;
            using (var db = new DBEntities())
            {
                var entity = db.APP_CLIENT.FirstOrDefault();
                client = target.GetClient(entity.ID);
                client.Name = "UPDATE.TEST" + DateTime.Now.Ticks;
                target.Update(client);
            }

            using (var db = new DBEntities())
            {
                var entity = db.APP_CLIENT.Where(e => e.ID == client.ID).FirstOrDefault();

                Assert.AreEqual(entity.NAME, client.Name);
            }
        }

        /// <summary>
        ///A test for GetClient
        ///</summary>
        [TestMethod()]
        public void GetClientTest()
        {
            using (var db = new DBEntities())
            {
                var entity = db.APP_CLIENT.FirstOrDefault();
                var client = target.GetClient(entity.ID);
                Assert.AreEqual(entity.NAME, client.Name);

                client = target.GetClient(entity.CLIENT_ID);
                Assert.AreEqual(entity.ID, client.ID);
            }
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            using (var db = new DBEntities())
            {
                var entity = db.APP_CLIENT.FirstOrDefault();
                target.Delete(entity.ID);
                var client = target.GetClient(entity.ID);
                Assert.AreEqual(true, client.Deleted);
            }
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void CreateTest()
        {
            var client = new Client
            {
                Hosts = "localhost",
                Name = "Test" + DateTime.Now.Ticks
            };
            target.Create(client);

            Assert.AreNotEqual(0, client.ID);
        }
    }
}
