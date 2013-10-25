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
            using (var db = GetDataContext())
            {
                foreach (var log in logs)
                {
                    db.OPERATE_LOG.Add(new OPERATE_LOG
                    {
                        ACCOUNT_ID = log.AccountId,
                        ACTION = log.Action,
                        CREATE_TIME = log.CreateTime,
                    });
                }

                db.SaveChanges();
            }
        }
    }
}
