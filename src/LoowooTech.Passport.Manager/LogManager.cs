using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;
using LoowooTech.Passport.Dao;

namespace LoowooTech.Passport.Manager
{
    public class LogManager : ManagerBase
    {
        private static ConcurrentBag<OperateLog> _queue = new ConcurrentBag<OperateLog>();

        private OperateLogDao Dao = new OperateLogDao();

        public void AddLog(OperateLog log)
        {
            _queue.Add(log);
            if (_queue.Count > 10)
            {
                var logs = _queue;
                _queue = new ConcurrentBag<OperateLog>();
                Dao.Create(logs); 
            }
        }
    }
}
