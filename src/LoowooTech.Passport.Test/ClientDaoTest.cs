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
            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Client.FirstOrDefault();
                client = target.GetClient(entity.ID);
                client.Name = "UPDATE.TEST" + DateTime.Now.Ticks;
                target.Update(client);
            }

            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Client.Where(e => e.ID == client.ID).FirstOrDefault();

                Assert.AreEqual(entity.Name, client.Name);
            }
        }

        /// <summary>
        ///A test for GetClient
        ///</summary>
        [TestMethod()]
        public void GetClientTest()
        {
            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Client.FirstOrDefault();
                var client = target.GetClient(entity.ID);
                Assert.AreEqual(entity.Name, client.Name);

                client = target.GetClient(entity.ClientId);
                Assert.AreEqual(entity.ID, client.ID);
            }
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Client.FirstOrDefault();
                target.Delete(entity.ID);
                var client = target.GetClient(entity.ID);
                Assert.AreEqual(1, client.Deleted);
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
