using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Model
{
    public class AuthCode
    {
        public string ClientId { get; set; }

        public int AccountId { get; set; }

        public string AccessToken { get; set; }

        public DateTime CreateTime { get; set; }
    }
}