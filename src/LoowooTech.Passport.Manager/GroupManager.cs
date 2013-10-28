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
            if (accountId == 0)
            {
                return new List<Group>();
            }
            return Dao.GetGroups(accountId);
        }

        public PagingResult<Group> GetGroups(GroupFilter filter, int page, int pageSize)
        {
            var paging = new Paging(page, pageSize);
            var list = Dao.GetGroups(filter, paging);

            return new PagingResult<Group>(paging, list);
        }

        public IEnumerable<string> GetAllRightNames(int accountId)
        {
            var groups = GetGroups(accountId);
            foreach (var g in groups)
            {
                foreach (var right in g.Rights)
                {
                    yield return right;
                }
            }
        }

        public Dictionary<string, RightLevel> GetAllRightLevels(Account account)
        {

            var selfRights = GetAllRightNames(account.AccountId).ToDictionary(r => r, r => RightLevel.SelfRight);

            if (account.AgentId > 0)
            {
                var agentRights = GetAllRightNames(account.AgentId).ToDictionary(r => r, r => RightLevel.SelfRight);
                
                //把代理权限也赋予本人，但标明为代理权限。
                foreach (var kv in agentRights)
                {
                    if (selfRights.ContainsKey(kv.Key))
                    {
                        selfRights[kv.Key] = kv.Value;
                    }
                    else
                    {
                        selfRights.Add(kv.Key, kv.Value);
                    }
                }
            }

            return selfRights;
        }

        public Dictionary<string, RightLevel> GetRightLevels(Account account, IEnumerable<string> rightNames)
        {
            var result = rightNames.ToDictionary(r => r, r => RightLevel.NoRight);
            var allRights = GetAllRightLevels(account);

            foreach (var name in rightNames)
            {
                if (allRights.ContainsKey(name))
                {
                    result[name] = allRights[name];
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
