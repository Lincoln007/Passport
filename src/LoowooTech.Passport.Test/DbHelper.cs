﻿using LoowooTech.Passport.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Test
{
    public class DbHelper
    {
        public static DBDataContext GetDataContext()
        {
            return new DBDataContext();
        }
    }
}
