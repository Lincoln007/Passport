using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    [Table("RANK")]
    public class Rank
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("DELETED")]
        public short Deleted { get; set; }
    }
}
