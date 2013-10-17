using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.Passport.Model
{
    public class Account
    {
        public Account()
        {
            Agent = new Account();
            Groups = new List<Group>();
            CreateTime = DateTime.Now;
            LastLoginTime = CreateTime;
            Role = Model.Role.Everyone;
        }

        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Account Agent { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string LastLoginIP { get; set; }

        public string TrueName { get; set; }

        public string ContactNo { get; set; }

        public Role Role { get; set; }

        public bool IsAuthenticated
        {
            get { return ID > 0; }
        }
    }
}