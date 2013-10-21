using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Model
{
    public class Client
    {
        public long ID { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Hosts { get; set; }

        public DateTime CreateTime { get; set; }

        public bool Deleted { get; set; }
    }
}