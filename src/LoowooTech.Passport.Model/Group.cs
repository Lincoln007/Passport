using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Model
{
    public class Group
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Rights { get; set; }
    }
}