using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class GroupDao : DaoBase
    {
        public IEnumerable<USER_GROUP> GetGroups(int accountId)
        {
            var groupIds = DB.USER_ACCOUNT_GROUP.Where(a => a.ACCOUNT_ID == accountId).Select(ag => ag.GROUP_ID).ToArray();
            return DB.USER_GROUP.Where(g => groupIds.Contains(g.ID));
        }

        public IEnumerable<string> GetGroupRights(int groupId)
        {
            return DB.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == groupId).Select(e => e.NAME);
        }

        public void Create(USER_GROUP entity)
        {
            DB.USER_GROUP.Add(entity);
            DB.SaveChanges();
        }

        public void CreateGroupRights(IEnumerable<USER_GROUP_RIGHT> entities)
        {
            foreach (var entity in entities)
            {
                DB.USER_GROUP_RIGHT.Add(entity);
            }
            DB.SaveChanges();
        }

        public void DeleteGroupRights(int groupId)
        {
            var rights = DB.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == groupId);

            foreach (var item in rights)
            {
                DB.USER_GROUP_RIGHT.Remove(item);
            }

            DB.SaveChanges();
        }

        public void Update(USER_GROUP entity)
        {
            var updateEntity = DB.USER_GROUP.FirstOrDefault(e => e.ID == entity.ID);
            if (updateEntity == null)
            {
                throw new ArgumentException("更新失败，没找到这个组！");
            }

            updateEntity.NAME = entity.NAME;

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
