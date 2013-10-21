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
            return Dao.GetGroups(accountId);
        }

        public void Create(Group group)
        {
            Dao.Create(group);
        }

        public void Update(Group group)
        {
            Dao.Update(group);
        }

        public void Delete(int groupId)
        {
            Dao.Delete(groupId);
        }
    }
}
