using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    public class GroupFilter : SelectFilter
    {
        public int? AccountId { get; set; }

        public int? ClientId { get; set; }
    }
}
