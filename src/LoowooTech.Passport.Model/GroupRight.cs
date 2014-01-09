using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{
    [Table("USER_GROUP_RIGHT")]
    public class GroupRight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("GROUP_ID")]
        public int GroupID { get; set; }

        [Column("NAME")]
        public string Name { get; set; }
    }
}
