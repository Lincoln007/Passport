using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoowooTech.Passport.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new AccountCacheService();
            service.Start();
            while (true)
            {
                var cmd = Console.ReadLine();
                if (cmd == "refresh")
                {
                    service.Refresh();
                }
            }
        }
    }
}
