using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public class OperateLogDao : DaoBase
    {
        public void Create(IEnumerable<OperateLog> logs)
        {
            //using (var db = GetDataContext())
            //{
            //    foreach (var log in logs)
            //    {
            //        db.OperateLog.Add(log);
            //    }

            //    db.SaveChanges();
            //}
        }
    }
}
