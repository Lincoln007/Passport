using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Dao;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Manager
{
    public class GroupManager : ManagerBase
    {
        public GroupManager(Core core) : base(core) { }

        private GroupDao Dao = new GroupDao();

        public IEnumerable<Group> GetGroups(int accountId)
        {
            return Dao.GetGroups(accountId).Select(g => new Group
            {
                Name = g.NAME,
                Rights = Dao.GetGroupRights(g.ID)
            });
        }

        public void Create(Group group)
        {
            var entity = new USER_GROUP();
            entity.NAME = group.Name;
            Dao.Create(entity);

            group.GroupID = entity.ID;

            var rightEntities = ConvertModelRights(group);

            Dao.CreateGroupRights(rightEntities);
        }

        private IEnumerable<USER_GROUP_RIGHT> ConvertModelRights(Group group)
        {
            return group.Rights.Select(name => new USER_GROUP_RIGHT
            {
                GROUP_ID = group.GroupID,
                NAME = name
            });
        }

        public void Update(Group group)
        {
            Dao.DeleteGroupRights(group.GroupID);
            Dao.CreateGroupRights(ConvertModelRights(group));
            Dao.Update(new USER_GROUP { });
        }
    }
}
