using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace LoowooTech.Passport.Model
{
    [Table("DEPARTMENT")]
    public class Department
    {
        public Department()
        {
            CreateTime = DateTime.Now;
        }

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

        [NotMapped]
        [JsonProperty("children")]
        public List<Department> Children { get; set; }
    }
}
