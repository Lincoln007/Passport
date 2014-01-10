using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{

    [Table("USER_ACCOUNT")]
    public class Account
    {
        public Account()
        {
            CreateTime = DateTime.Now;
            LastLoginTime = CreateTime;
            Role = (short)Model.Role.Everyone;
            LastLoginIP = string.Empty;
            Status = (short)Model.Status.Enabled;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int AccountId { get; set; }

        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("GROUPS")]
        public string Groups { get; set; }

        public bool HasGroup(int groupId)
        {
            if (string.IsNullOrEmpty(Groups))
            {
                return false;
            }
            return Groups.Split(',').Contains(groupId.ToString());
        }

        public static string GetEncyptPassword(string password, DateTime createTime)
        {
            return (password + createTime.ToString()).SHA1().MD5();
        }

        [NotMapped]
        public int AgentId { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }

        [Column("LAST_LOGIN_TIME")]
        public DateTime LastLoginTime { get; set; }

        [Column("LAST_LOGIN_IP")]
        public string LastLoginIP { get; set; }

        [Column("TRUENAME")]
        public string TrueName { get; set; }

        [Column("ROLE")]
        public short Role { get; set; }

        [Column("DELETED")]
        public short Deleted { get; set; }

        [Column("STATUS")]
        public short Status { get; set; }

        [NotMapped]
        public bool IsAuthenticated
        {
            get { return AccountId > 0; }
        }
    }
}