using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{
    [Table("ACCOUNT_AGENT")]
    public class AccountAgent
    {
        public AccountAgent()
        {
            CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "INT")]
        public int ID { get; set; }

        [Column("ACCOUNT_ID")]
        public int AccountId { get; set; }

        [Column("AGENT_ID")]
        public int AgentId { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }

        [Column("DELETED")]
        public short Deleted { get; set; }
    }
}
