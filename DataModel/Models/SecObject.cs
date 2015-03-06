using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Models
{
    [Table("sec.SecObject")]
    public class SecObject
    {
        public SecObject()
        {
            SGrant = new HashSet<SGrant>();
        }

        [Key]
        public int IdSecObject { get; set; }

        [Required]
        [StringLength(200)]
        public string ObjectName { get; set; }

        [StringLength(100)]
        public string Type1 { get; set; }

        [StringLength(100)]
        public string Type2 { get; set; }

        [StringLength(100)]
        public string Type3 { get; set; }

        [StringLength(100)]
        public string Type4 { get; set; }

        [StringLength(100)]
        public string Type5 { get; set; }

        [StringLength(100)]
        public string Type6 { get; set; }

        [StringLength(100)]
        public string Type7 { get; set; }

        public virtual ICollection<SGrant> SGrant { get; set; }
    }
}
