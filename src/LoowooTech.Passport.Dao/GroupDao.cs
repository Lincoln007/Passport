using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class GroupDao : DaoBase
    {
        public List<Group> GetGroups(GroupFilter filter, Paging page = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.Group.AsQueryable();
                if (filter.Deleted.HasValue)
                {
                    query = query.Where(e => e.Deleted == (short)(filter.Deleted.Value ? 1 : 0));
                }

                if (filter.AccountId.HasValue)
                {
                    var account = db.Account.FirstOrDefault(e => e.AccountId == filter.AccountId);
                    if (!string.IsNullOrEmpty(account.Groups))
                    {
                        var groupIds = account.Groups.Split(',').Select(s => int.Parse(s)).ToArray();
                        query = query.Where(e => groupIds.Contains(e.GroupID));
                    }
                }

                return query.OrderByDescending(e => e.GroupID).SetPage(page).ToList();
            }
        }

        public List<Group> GetGroups(int accountId)
        {
            return GetGroups(new GroupFilter { AccountId = accountId, GetRights = true });
        }


        public void Create(Group group)
        {
            using (var db = GetDataContext())
            {
                db.Group.Add(group);
                db.SaveChanges();
                AddRights(group);
            }
        }

        private void AddRights(Group group)
        {
            if (group.Rights == null) return;
            using (var db = GetDataContext())
            {
                foreach (var right in group.Rights)
                {
                    right.GroupID = group.GroupID;
                    db.GroupRight.Add(right);
                }
                db.SaveChanges();
            }
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
        }

        public void Update(Group group)
        {
            using (var db = GetDataContext())
            {
                var toUpdate = db.Group.FirstOrDefault(e => e.GroupID == group.GroupID);
                if (toUpdate == null)
                {
                    throw new ArgumentException("更新失败，没找到这个组！");
                }
                RemoveRigths(toUpdate.GroupID);
                db.Entry(toUpdate).CurrentValues.SetValues(group);
                db.SaveChanges();
                AddRights(group);
            }

        }

        public void Delete(int groupId)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Group.FirstOrDefault(e => e.GroupID == groupId);
                if (entity == null)
                {
                    throw new ArgumentException("更新失败，没找到这个组！");
                }

                entity.Deleted = 1;
                db.SaveChanges();

                RemoveRigths(groupId);
            }
        }

        public Group GetGroup(int groupId)
        {
            using (var db = GetDataContext())
            {
                return db.Group.FirstOrDefault(e => e.GroupID == groupId);
            }
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
