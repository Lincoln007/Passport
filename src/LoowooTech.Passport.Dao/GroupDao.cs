using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;
using Dapper;

namespace LoowooTech.Passport.Dao
{
    public class GroupDao : DaoBase
    {
        public IEnumerable<Group> GetGroups(long accountId)
        {
            using (var conn = GetConnection())
            {
                var groupIds = conn.Query("SELECT GROUP_ID FROM USER_ACCOUNT_GROUP WHERE ACCOUNT_ID = :AccountId", new { AccountId = accountId });

                var sql = @"
SELECT g.*,r.NAME AS RIGHT FROM USER_GROUP  g
LEFT JOIN USER_GROUP_RIGHT  r
ON g.ID = r.GROUP_ID
WHERE g.ID in ({0})";

                sql = string.Format(sql, string.Join(",", groupIds));

                var groups = new Dictionary<long, Group>();
                var list = conn.Query(sql);
                foreach (var d in list)
                {
                    if (groups.ContainsKey(d.ID))
                    {
                        var group = groups[(long)d.ID];
                        ((List<string>)group.Rights).Add(d.RIGHT);
                    }
                    else
                    {
                        groups.Add((long)d.ID, new Group
                        {
                            GroupID = d.ID,
                            Name = d.NAME,
                            Deleted = d.DELETED == 1,
                        });
                    }
                }

                return groups.Select(kv => kv.Value);
            }

        }


        public void Create(Group group)
        {
            //using (var conn = GetConnection())
            //{
            //    conn.Execute("", new { });
            //}

            //var entity = new USER_GROUP
            //{
            //    NAME = group.Name,
            //    DELETED = 0,
            //};
            //DB.USER_GROUP.Add(entity);
            //DB.SaveChanges();

            //foreach (var right in group.Rights)
            //{
            //    DB.USER_GROUP_RIGHT.Add(new USER_GROUP_RIGHT
            //    {
            //        NAME = right,
            //        GROUP_ID = entity.ID,
            //    });
            //}

            //DB.SaveChanges();
        }

        public void DeleteGroupRights(int groupId)
        {
            //var rights = DB.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == groupId);

            //foreach (var item in rights)
            //{
            //    DB.USER_GROUP_RIGHT.Remove(item);
            //}

            //DB.SaveChanges();
        }

        public void Update(Group group)
        {
            //var entity = DB.USER_GROUP.FirstOrDefault(e => e.ID == group.GroupID);
            //if (entity == null)
            //{
            //    throw new ArgumentException("更新失败，没找到这个组！");
            //}
            //entity.NAME = group.Name;
            //entity.DELETED = (short)(group.Deleted ? 1 : 0);

            //var rights = DB.USER_GROUP_RIGHT.Where(e => e.GROUP_ID == group.GroupID);

            //foreach (var item in rights)
            //{
            //    DB.USER_GROUP_RIGHT.Remove(item);
            //}

            //foreach (var right in group.Rights)
            //{
            //    DB.USER_GROUP_RIGHT.Add(new USER_GROUP_RIGHT
            //    {
            //        NAME = right,
            //        GROUP_ID = entity.ID,
            //    });
            //}

            //DB.SaveChanges();


        }

        public void Delete(int groupId)
        {
            //var entity = DB.USER_GROUP.FirstOrDefault(e => e.ID == groupId);
            //if (entity == null)
            //{
            //    throw new ArgumentException("更新失败，没找到这个组！");
            //}

            //entity.DELETED = 1;
            //DB.SaveChanges();

        }
    }
}
