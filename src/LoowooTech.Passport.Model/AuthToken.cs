using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Model
{
    public class AuthToken
    {
        public int ID { get; set; }

        public string Token { get; set; }

        public DateTime CreateTime { get; set; }

        public int AccountId { get; set; }
    }
}