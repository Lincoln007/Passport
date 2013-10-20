﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int AgentID { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string LastLoginIP { get; set; }

        public string TrueName { get; set; }

        public Role Role { get; set; }

        public bool IsAuthenticated
        {
            get { return ID > 0; }
        }
    }
}