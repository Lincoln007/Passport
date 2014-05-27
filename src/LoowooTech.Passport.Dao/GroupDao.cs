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
            using (var db = GetDataContext())
            {
                return db.GroupRight.ToList();
            }
        }

        private List<Group> GetAllGroups()
        {
            using (var db = GetDataContext())
            {
                return db.Group.Where(e => e.Deleted == 0).ToList().Select(e => new Group
                {
                    CreateTime = e.CreateTime,
                    Description = e.Description,
                    Deleted = e.Deleted,
                    GroupId = e.GroupId,
                    Name = e.Name,
                    Rights = GetAllRights().Where(r => r.GroupID == e.GroupId)
                }).ToList();
            }
        }

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
        }

        public Group GetGroup(int groupId)
        {
            return GetAllGroups().FirstOrDefault(e => e.GroupId == groupId);
        }
    }
}
