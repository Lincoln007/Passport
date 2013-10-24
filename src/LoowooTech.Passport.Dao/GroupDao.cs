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

        public IEnumerable<Group> GetGroups(GroupFilter filter, Paging page = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.USER_GROUP.AsQueryable();
                if (filter.Deleted.HasValue)
                {
                    query = query.Where(e => e.DELETED == (short)(filter.Deleted.Value ? 1 : 0));
                }

                if (filter.AccountId.HasValue)
                {
                    var groupIds = db.USER_ACCOUNT_GROUP.Where(a => a.ACCOUNT_ID == filter.AccountId.Value).Select(ag => ag.GROUP_ID).ToArray();
                    query = query.Where(e => groupIds.Contains(e.ID));
                }

                return query.OrderByDescending(e => e.ID).SetPage(page).Select(e => ConvertEntity(e, filter.GetRights));
            }
        }

        public IEnumerable<Group> GetGroups(int accountId)
        {
            return GetGroups(new GroupFilter { AccountId = accountId, GetRights = true });
        }


        public void Create(Group group)
        {
            var entity = new USER_GROUP
            {
                NAME = group.Name,
                DELETED = 0,
                DESCRIPTION = group.Description,
                CREATE_TIME = group.CreateTime,

            };

            using (var db = GetDataContext())
            {
                db.USER_GROUP.Add(entity);
                db.SaveChanges();

                var rights = group.Rights.Select(name => new USER_GROUP_RIGHT
                {
                    GROUP_ID = entity.ID,
                    NAME = name
                });

                foreach (var item in rights)
                {
                    db.USER_GROUP_RIGHT.Add(item);
                }
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
                db.SaveChanges();

                foreach (var item in db.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == entity.ID))
                {
                    db.USER_GROUP_RIGHT.Remove(item);
                }

                foreach (var name in group.Rights)
                {
                    db.USER_GROUP_RIGHT.Add(new USER_GROUP_RIGHT
                    {
                        GROUP_ID = group.GroupID,
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
