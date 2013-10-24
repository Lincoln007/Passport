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

        public PagingResult<Group> GetGroups(int page, int pageSize)
        {
            var paging = new Paging(page, pageSize);
            var list = Dao.GetGroups(null, paging);

            return new PagingResult<Group>(paging, list);
        }

        public bool HasRights(IEnumerable<Group> groups, IEnumerable<string> rightNames)
        {
            throw new NotImplementedException();
        }

        public void Delete(int groupId)
        {
            Dao.Delete(groupId);
        }

        public Group GetGroup(int groupId)
        {
            return Dao.GetGroup(groupId);
        }

        public void Save(Group group)
        {
            if (string.IsNullOrEmpty(group.Name))
            {
                throw new ArgumentNullException("用户组名称没有填写！");
            }

            if (group.GroupID > 0)
            {
                Dao.Update(group);
            }
            else
            {
                Dao.Create(group);
            }
        }
    }
}
