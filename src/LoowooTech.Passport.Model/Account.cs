using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Common;

namespace LoowooTech.Passport.Model
{
    public class Account
    {
        public Account()
        {
            CreateTime = DateTime.Now;
            LastLoginTime = CreateTime;
            Role = Model.Role.Everyone;
        }

        public int AccountID { get; set; }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != null)
                    _username = value.ToLower();
            }
        }

        public string Password { get; set; }

        public string EncyptedPassword
        {
            get { return EncyptPassword(Password); }
        }

        public static string EncyptPassword(string password)
        {
            return password.SHA1().MD5();
        }

        public int AgentID { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string LastLoginIP { get; set; }

        public string TrueName { get; set; }

        public Role Role { get; set; }

        public bool Deleted { get; set; }

        public bool IsAuthenticated
        {
            get { return AccountID > 0; }
        }
    }
}