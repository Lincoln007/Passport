using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    /*
     
     */
    [Table("VACCOUNT")]
    public class VAccount
    {
        [Key]
        [Column("ID")]
        public int AccountId { get; set; }

        [Column("USERNAME")]
        public string Username { get; set; }

        [Column("TRUENAME")]
        public string TrueName { get; set; }

        [Column("DEPARTMENT")]
        public string Department { get; set; }

        [Column("RANK")]
        public string Rank { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }

        [Column("STATUS")]
        public int Status { get; set; }

        [Column("DELETED")]
        public short Deleted { get; set; }

    }
}
