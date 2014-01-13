using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;
using LoowooTech.Common;

namespace LoowooTech.Passport.Dao
{
    public class GroupDao : DaoBase
    {
        private string _groupKey = "AllGroups";
        private string _rightKey = "AllRights";

        private List<GroupRight> GetAllRights()
        {
            var list = Cache.Get<List<GroupRight>>(_rightKey);
            if (list == null)
            {
                using (var db = GetDataContext())
                {
                    list = db.GroupRight.ToList();
                }
                Cache.Set(_rightKey, list);
            }
            return list;
        }

        private List<Group> GetAllGroups()
        {
            var list = Cache.Get<List<Group>>(_groupKey);
            if (list == null)
            {
                using (var db = GetDataContext())
                {
                    list = db.Group.Where(e => e.Deleted == 0).ToList().Select(e => new Group
                    {
                        CreateTime = e.CreateTime,
                        Description = e.Description,
                        Deleted = e.Deleted,
                        GroupId = e.GroupId,
                        Name = e.Name,
                        Rights = GetAllRights().Where(r => r.GroupID == e.GroupId)
                    }).ToList();
                }
                Cache.Set(_groupKey, list);
            }
            return list;
        }

        private void RemoveCache()
        {
            Cache.Remove(_rightKey);
            Cache.Remove(_groupKey);
        }

        //public List<Group> GetGroups(GroupFilter filter, Paging page = null)
        //{
        //    IQueryable<Group> list = null;
        //    if (filter == null)
        //    {
        //        return GetAllGroups();
        //    }

        //    if (filter.AccountId.HasValue)
        //    {
        //        list = GetGroups(filter.AccountId.Value).AsQueryable();
        //    }
        //    else
        //    {
        //        list = GetAllGroups().AsQueryable();
        //    }

        //    if (filter.Deleted.HasValue)
        //    {
        //        list = list.Where(e => e.Deleted == (short)(filter.Deleted.Value ? 1 : 0));
        //    }
        //    return list.OrderByDescending(e => e.GroupId).SetPage(page).ToList();
        //}

        public List<Group> GetGroups(int accountId)
        {
            var query = GetAllGroups();
            if (accountId > 0)
            {
                var account = new AccountDao().GetAccount(accountId);
                if (string.IsNullOrEmpty(account.Groups))
                {
                    return new List<Group>();
                }
                var groupIds = account.Groups.Split(',').Select(s => int.Parse(s));
                return query.Where(e => groupIds.Contains(e.GroupId)).ToList();
            }
            else
            {
                return query;
            }
        }


        public void Create(Group group)
        {
            using (var db = GetDataContext())
            {
                db.Group.Add(group);
                db.SaveChanges();
                AddRights(group);
            }
            RemoveCache();
        }

        private void AddRights(Group group)
        {
            if (group.Rights == null) return;
            using (var db = GetDataContext())
            {
                foreach (var right in group.Rights)
                {
                    right.GroupID = group.GroupId;
                    db.GroupRight.Add(right);
                }
                db.SaveChanges();
            }
            RemoveCache();
        }

        private void RemoveRigths(int groupId)
        {
            using (var db = GetDataContext())
            {
                foreach (var right in db.GroupRight.Where(e => e.GroupID == groupId))
                {
                    db.GroupRight.Remove(right);
                }
                db.SaveChanges();
            }
            RemoveCache();
        }

        public void Update(Group group)
        {
            using (var db = GetDataContext())
            {
                var toUpdate = db.Group.FirstOrDefault(e => e.GroupId == group.GroupId);
                if (toUpdate == null)
                {
                    throw new ArgumentException("更新失败，没找到这个组！");
                }
                RemoveRigths(toUpdate.GroupId);
                db.Entry(toUpdate).CurrentValues.SetValues(group);
                db.SaveChanges();
                AddRights(group);
            }
            RemoveCache();

        }

        public void Delete(int groupId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Group.FirstOrDefault(e => e.GroupId == groupId);
                if (entity == null)
                {
                    throw new ArgumentException("更新失败，没找到这个组！");
                }

                entity.Deleted = 1;
                db.SaveChanges();

                RemoveRigths(groupId);
            }
            RemoveCache();
        }

        public Group GetGroup(int groupId)
        {
            return GetAllGroups().FirstOrDefault(e => e.GroupId == groupId);
        }

        //public List<GroupRight> GetGroupRights(int groupId)
        //{
        //    using (var db = GetDataContext())
        //    {
        //        return db.GroupRight.Where(e => e.GroupID == groupId).ToList();
        //    }
        //}
    }
}
