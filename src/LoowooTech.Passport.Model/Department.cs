using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{
    [Table("DEPARTMENT")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("PARENT_ID")]
        public int ParentID { get; set; }

        [Column("CLIENT_ID")]
        public int ClientID { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }

        [Column("DELETED")]
        public short Deleted { get; set; }
    }
}
