using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    public class SelectFilter
    {
        public bool? Enabled { get; set; }

        public bool? Deleted { get; set; }

        public string SearchKey { get; set; }

        public int Limit { get; set; }

        public int Skip { get; set; }
    }
}
