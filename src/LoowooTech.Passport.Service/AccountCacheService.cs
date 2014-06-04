using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LoowooTech.Common;
using LoowooTech.Passport.Manager;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Service
{
    public class AccountCacheService
    {
        public AccountCacheService()
        {
            _interval = int.Parse(AppSettings.Current["interval"]);
        }

        private Thread _worker = null;

        private int _interval = 1;

        public void Start()
        {
            _worker = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        DoWork();
                    }
                    catch
                    {
                    }
                    Thread.Sleep(1000 * 60 * _interval);
                }
            });
            _worker.Start();
        }

        public void Refresh()
        {
            DoWork();
        }

        private void DoWork()
        {
            var manager = new AccountManager();
            var result = manager.GetVAccounts(new AccountFilter { }, 1, int.MaxValue);
            foreach (var account in result.List)
            {
                var key = account.TrueName + "_" + account.Department + "_" + account.Rank;
                manager.UpdateCache(account);
                Cache.HSet("account_name", key, account);
                Cache.HSet("account_id", account.AccountId.ToString(), account);
            }
            Console.WriteLine("[刷新缓存]\t" + DateTime.Now.ToString() + "\t" + result.List.Count());
        }
    }
}
