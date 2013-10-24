using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class GroupDao : DaoBase
    {
        private Group ConvertEntity(USER_GROUP entity, bool getRights = false)
        {
            if (entity == null) return null;
            var model = new Group
            {
                GroupID = entity.ID,
                Name = entity.NAME,
                Deleted = entity.DELETED == 1,
                Description = entity.DESCRIPTION,
                CreateTime = entity.CREATE_TIME
            };

            using (var db = GetDataContext())
            {
                if (getRights)
                {
                    model.Rights = db.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == entity.ID).AsEnumerable().Select(e => e.NAME);
                }
            }

            return model;
        }

        public IEnumerable<Group> GetGroups(SelectFilter filter, Paging page = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.USER_GROUP.AsQueryable();
                if (filter.Deleted.HasValue)
                {
                    query = query.Where(e => e.DELETED == (short)(filter.Deleted.Value ? 1 : 0));
                }

                return query.OrderByDescending(e => e.ID).SetPage(page).Select(e => ConvertEntity(e, false));
            }
        }

        public IEnumerable<Group> GetGroups(int accountId)
        {
            using (var db = GetDataContext())
            {
                var groupIds = db.USER_ACCOUNT_GROUP.Where(a => a.ACCOUNT_ID == accountId).Select(ag => ag.GROUP_ID).ToArray();
                return db.USER_GROUP.Where(e => groupIds.Contains(e.ID)).Select(e => ConvertEntity(e, true)); ;
            }
        }


        public void Create(Group group)
        {
            var entity = new USER_GROUP
            {
                NAME = group.Name,
                DELETED = 0,
            };

            using (var db = GetDataContext())
            {

                entity.USER_GROUP_RIGHT = group.Rights.Select(name => new USER_GROUP_RIGHT { NAME = name }).ToArray();

                db.USER_GROUP.Add(entity);

                db.SaveChanges();
            }
        }

        public void DeleteGroupRights(int groupId)
        {
            using (var db = GetDataContext())
            {
                var rights = db.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == groupId);

                foreach (var item in rights)
                {
                    db.USER_GROUP_RIGHT.Remove(item);
                }

                db.SaveChanges();
            }
        }

        public void Update(Group group)
        {
            using (var db = GetDataContext())
            {
                var entity = db.USER_GROUP.FirstOrDefault(e => e.ID == group.GroupID);
                if (entity == null)
                {
                    throw new ArgumentException("更新失败，没找到这个组！");
                }
                entity.NAME = group.Name;
                entity.DELETED = (short)(group.Deleted ? 1 : 0);

                foreach (var item in entity.USER_GROUP_RIGHT)
                {
                    entity.USER_GROUP_RIGHT.Remove(item);
                }

                foreach (var name in group.Rights)
                {
                    entity.USER_GROUP_RIGHT.Add(new USER_GROUP_RIGHT
                    {
                        NAME = name,
                    });
                }

                db.SaveChanges();
            }

        }

        public void Delete(int groupId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.USER_GROUP.FirstOrDefault(e => e.ID == groupId);
                if (entity == null)
                {
                    throw new ArgumentException("更新失败，没找到这个组！");
                }

                entity.DELETED = 1;
                db.SaveChanges();
            }
        }

        public Group GetGroup(int groupId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.USER_GROUP.FirstOrDefault(e => e.ID == groupId);
                return ConvertEntity(entity, true);
            }
        }
    }
}
