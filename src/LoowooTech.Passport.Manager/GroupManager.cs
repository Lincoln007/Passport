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

        private static readonly GroupDao Dao = new GroupDao();

        public IEnumerable<Group> GetGroups(int accountId)
        {
            return Dao.GetGroups(accountId);
        }

        public bool HasRights(IEnumerable<Group> groups, IEnumerable<string> rightNames)
        {
            throw new NotImplementedException();
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
