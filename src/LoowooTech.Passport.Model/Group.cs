using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace LoowooTech.Passport.Model
{
    [Table("GROUP")]
    public class Group
    {
        public Group()
        {
            CreateTime = DateTime.Now;
            Rights = new List<GroupRight>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "INT")]
        public int GroupId { get; set; }

        [Column("NAME")]
        public string Name { get; set; }


        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }

        [Column("DELETED")]
        public short Deleted { get; set; }

        [NotMapped]
        public string ClientName { get; set; }

        [ForeignKey("GROUP_ID")]
        public virtual IEnumerable<GroupRight> Rights { get; set; }
    }
}