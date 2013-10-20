using LoowooTech.Passport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class GroupDao : DaoBase
    {
        public IEnumerable<Group> GetAccountGroups(int accountId)
        {
            var groupIds = DB.USER_ACCOUNT_GROUP.Where(a => a.ACCOUNT_ID == accountId).Select(ag => ag.GROUP_ID).ToArray();
            return DB.USER_GROUP
                .Where(g => groupIds.Contains(g.ID))
                .Select(g => new Group
                {
                    ID = g.ID,
                    Name = g.NAME,
                    Rights = DB.USER_GROUP_RIGHT.Where(r => r.GROUP_ID == g.ID).Select(r => r.NAME)
                });
        }
    }
}
