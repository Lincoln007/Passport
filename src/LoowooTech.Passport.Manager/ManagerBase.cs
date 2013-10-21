using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Manager
{
    public class ManagerBase
    {
        protected Core Core;
        public ManagerBase(Core core)
        {
            Core = core;
        }
    }
}
