using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoowooTech.Common;
using LoowooTech.Passport.Manager;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Service
{
    public class AccountCacheService
    {
        public void Start()
        {
            var manager = new AccountManager();
            var result = manager.GetVAccounts(new AccountFilter { }, 0, int.MaxValue);
            foreach (var account in result.List)
            {
                var key = account.TrueName + "_" + account.Department + "_" + account.Rank;
                Console.WriteLine(key);
                manager.UpdateCache(account);
                Cache.HSet("account_name", key, account);
                Cache.HSet("account_id", account.AccountId.ToString(), account);
            }
        }
    }
}
