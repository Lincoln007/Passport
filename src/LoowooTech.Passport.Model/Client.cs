using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Common;

namespace LoowooTech.Passport.Model
{
    public class Client
    {
        public Client()
        {
            CreateTime = DateTime.Now;
            ClientId = CreateTime.Ticks.ToString();
            ClientSecret = Guid.NewGuid().ToString().MD5();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Hosts { get; set; }

        public DateTime CreateTime { get; set; }

        public bool Deleted { get; set; }
    }
}