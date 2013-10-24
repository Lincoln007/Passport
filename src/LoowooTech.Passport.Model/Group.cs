using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Model
{
    public class Group
    {
        public Group()
        {
            Rights = new List<string>();
        }

        public int GroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public bool Deleted { get; set; }

        public IEnumerable<string> Rights { get; set; }

        public bool HasRight(string rightName)
        {
            return Rights.Any(r => r.ToLower() == rightName.ToLower());
        }
    }
}