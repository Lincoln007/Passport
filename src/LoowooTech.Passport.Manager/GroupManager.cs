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

        public PagingResult<Group> GetGroups(GroupFilter filter, int page, int pageSize)
        {
            var paging = new Paging(page, pageSize);
            var list = Dao.GetGroups(filter, paging);

            return new PagingResult<Group>(paging, list);
        }

        public Dictionary<string, RightLevel> GetRightLevels(IEnumerable<Group> groups, IEnumerable<string> rightNames, RightLevel level)
        {
            var result = new Dictionary<string, RightLevel>();
            foreach (var group in groups)
            {
                foreach (var name in rightNames)
                {
                    result.Add(name, rightNames.Contains(name) ? level : RightLevel.NoRight);
                }
            }
            return result;
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
