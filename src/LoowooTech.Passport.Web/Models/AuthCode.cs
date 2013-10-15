using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Web.Models
{
    public class AuthCode
    {
        public string ClientId { get; set; }

        public int AccountId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}