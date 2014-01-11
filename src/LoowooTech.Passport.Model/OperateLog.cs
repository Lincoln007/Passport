using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{
    [Table("OPERATE_LOG")]
    public class OperateLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("ACCOUNT_ID")]
        public int AccountId { get; set; }

        [Column("ACTION")]
        public string Action { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }
    }
}
