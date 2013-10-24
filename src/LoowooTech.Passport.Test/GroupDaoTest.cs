using LoowooTech.Passport.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LoowooTech.Passport.Model;
using System.Collections.Generic;

namespace LoowooTech.Passport.Test
{


    /// <summary>
    ///This is a test class for GroupDaoTest and is intended
    ///to contain all GroupDaoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GroupDaoTest
    {
        private GroupDao target = new GroupDao();

        /// <summary>
        ///A test for GetGroups
        ///</summary>
        [TestMethod()]
        public void GetGroupsTest()
        {
            using (var db = new DBEntities())
            {
                var data = db.USER_ACCOUNT_GROUP.FirstOrDefault();

                if (data != null)
                {
                    var list = target.GetGroups(data.ACCOUNT_ID);
                    Assert.AreNotEqual(0, list.Count());
                }
            }
        }

        /// <summary>
        ///A test for GetGroup
        ///</summary>
        [TestMethod()]
        public void GetGroupTest()
        {
            using (var db = new DBEntities())
            {
                var data = db.USER_GROUP.FirstOrDefault();
                if (data != null)
                {
                    var group = target.GetGroup(data.ID);
                    Assert.AreEqual(data.NAME, group.Name);
                }
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
                var data = db.USER_GROUP.FirstOrDefault();
                if (data != null)
                {
                    target.Delete(data.ID);
                    var group = target.GetGroup(data.ID);
                    Assert.AreEqual(true, group.Deleted);
                }
            }
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void CreateTest()
        {
            var group = new Group
            {
                Name = "TestGroup" + DateTime.Now.Ticks,
                Description = "test",
                Rights = new List<string> { "TEST.UPDATE", "TEST.ADD" }
            };

            target.Create(group);

            using (var db = new DBEntities())
            {
                var entity = db.USER_GROUP.Where(e => e.NAME == group.Name).FirstOrDefault();

                Assert.AreNotEqual(null, entity);

                var rights = db.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == entity.ID).Select(e => e.NAME).ToList();

                Assert.AreEqual("TEST.UPDATE,TEST.ADD", string.Join(",", rights));
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Group group = null;

            using (var db = new DBEntities())
            {
                var data = db.USER_GROUP.FirstOrDefault();
                if (data == null) return;

                group = target.GetGroup(data.ID);
                group.Name = "UpdateTest" + DateTime.Now.Ticks;
                group.Rights = new List<string> { "UPDATE.TEST.UPDATE,AAA" };

                target.Update(group);

            }

            using (var db = new DBEntities())
            {
                var entity = db.USER_GROUP.Where(e => e.ID == group.GroupID).FirstOrDefault();

                Assert.AreEqual(entity.NAME, group.Name);

                var rights = db.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == group.GroupID).Select(e => e.NAME).ToList();

                Assert.AreEqual(string.Join(",", rights), string.Join(",", group.Rights));
            }
        }

    }
}
