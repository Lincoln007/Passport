using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    public class AccountFilter : SelectFilter
    {
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
