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
            using (var db = DbHelper.GetDataContext())
            {
                var data = db.Account.FirstOrDefault();

                if (data != null)
                {
                    var list = target.GetGroups(data.AccountId);
                    Assert.AreEqual(data.Groups, string.Join(",", list.Select(e => e.GroupID)));
                }
            }
        }

        /// <summary>
        ///A test for GetGroup
        ///</summary>
        [TestMethod()]
        public void GetGroupTest()
        {
            using (var db = DbHelper.GetDataContext())
            {
                var data = db.Group.FirstOrDefault();
                if (data != null)
                {
                    var group = target.GetGroup(data.GroupID);
                    Assert.AreEqual(data.Name, group.Name);
                }
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
                var data = db.Group.FirstOrDefault();
                if (data != null)
                {
                    target.Delete(data.GroupID);
                    var group = target.GetGroup(data.GroupID);
                    Assert.AreEqual(1, group.Deleted);
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
                Rights = new List<GroupRight> {
                    new GroupRight{Name= "TEST.UPDATE"}, 
                    new GroupRight{Name="TEST.ADD" }
                }
            };

            target.Create(group);

            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Group.Where(e => e.Name == group.Name).FirstOrDefault();

                Assert.AreNotEqual(null, entity);

                var rights = db.GroupRight.Where(e => e.GroupID == entity.GroupID).Select(e => e.Name).ToList();

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

            using (var db = DbHelper.GetDataContext())
            {
                var data = db.Group.FirstOrDefault();
                if (data == null) return;

                group = target.GetGroup(data.GroupID);
                group.Name = "UpdateTest" + DateTime.Now.Ticks;
                group.Rights = new List<GroupRight> {
                    new GroupRight{Name= "TEST.UPDATE"}, 
                    new GroupRight{Name= "TEST.AAA"}, 
                    new GroupRight{Name="TEST.ADD" }
                };

                target.Update(group);

            }

            using (var db = DbHelper.GetDataContext())
            {
                var entity = db.Group.Where(e => e.GroupID == group.GroupID).FirstOrDefault();

                Assert.AreEqual(entity.Name, group.Name);

                var rights = db.GroupRight.Where(e => e.GroupID == group.GroupID).Select(e => e.Name).ToList();

                Assert.AreEqual(string.Join(",", rights), string.Join(",", group.Rights.Select(e => e.Name)));
            }
        }

    }
}
