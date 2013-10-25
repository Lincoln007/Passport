using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    public class OperateLog
    {
        public int ID { get; set; }

        public int AccountId { get; set; }

        public string Action { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
