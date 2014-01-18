using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{
    [Table("APP_CLIENT")]
    public class Client
    {
        public Client()
        {
            CreateTime = DateTime.Now;
            ClientId = CreateTime.Ticks.ToString();
            ClientSecret = Guid.NewGuid().ToString().MD5();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "INT")]
        public int ID { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("CLIENT_ID")]
        public string ClientId { get; set; }

        [Column("CLIENT_SECRET")]
        public string ClientSecret { get; set; }

        [Column("HOSTS")]
        public string Hosts { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }

        [Column("DELETED")]
        public short Deleted { get; set; }

        [Column("DEPARTMENT_ID")]
        public int DepartmentId { get; set; }
    }
}