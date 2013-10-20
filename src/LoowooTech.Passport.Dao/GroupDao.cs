using LoowooTech.Passport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class GroupDao : DaoBase
    {
        public IEnumerable<Group> GetGroups(int accountId)
        {
            var groupIds = DB.USER_ACCOUNT_GROUP.Where(a => a.ACCOUNT_ID == accountId).Select(ag => ag.GROUP_ID).ToArray();
            return DB.USER_GROUP
                .Where(g => groupIds.Contains(g.ID))
                .Select(g => new Group
                {
                    GroupID = g.ID,
                    Name = g.NAME,
                    Rights = g.USER_GROUP_RIGHT.Select(r => r.NAME)
                });
        }

        public Group Create(Group group)
        {
            var entity = new USER_GROUP
            {
                NAME = group.Name,
                USER_GROUP_RIGHT = group.Rights.Select(rightName => new USER_GROUP_RIGHT
                {
                    NAME = rightName
                }).ToArray()
            };

            DB.USER_GROUP.Add(entity);
            DB.SaveChanges();
            group.GroupID = entity.ID;
            return group;
        }

        public void Update(Group group)
        {
            var entity = DB.USER_GROUP.FirstOrDefault(e => e.ID == group.GroupID);
            if (entity == null)
            {
                throw new ArgumentException("更新失败，没找到这个组！");
            }
            entity.NAME = group.Name;

            var rights = DB.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == group.GroupID);

            foreach (var item in rights)
            {
                DB.USER_GROUP_RIGHT.Remove(item);
            }

            foreach (var name in group.Rights)
            {
                DB.USER_GROUP_RIGHT.Add(new USER_GROUP_RIGHT { NAME = name });
            }

            DB.SaveChanges();
        }

        public void Delete(int groupId)
        {
            var entity = DB.USER_GROUP.FirstOrDefault(e => e.ID == groupId);
            if (entity == null)
            {
                throw new ArgumentException("更新失败，没找到这个组！");
            }

            entity.DELETED = 1;
            DB.SaveChanges();

        }
    }
}
