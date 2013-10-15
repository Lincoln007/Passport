using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Web.Models
{
    public class Client
    {
        public int ID { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Host { get; set; }
    }
}