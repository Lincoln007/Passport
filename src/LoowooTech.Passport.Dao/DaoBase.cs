using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class DaoBase
    {
        //protected DBEntities db
        //{
        //    get
        //    {
        //        return new DBEntities();
        //    }
        //}

        protected DBDataContext GetDataContext()
        {
            return new DBDataContext();
        }
    }
}
