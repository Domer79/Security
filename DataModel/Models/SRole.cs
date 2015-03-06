using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Models
{
    [Table("sec._Role")]
    public sealed class SRole
    {
        public SRole()
        {
            SGrant = new HashSet<SGrant>();
            Member = new HashSet<Member>();
        }

        [Key]
        public int IdRole { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public ICollection<SGrant> SGrant { get; set; }

        public ICollection<Member> Member { get; set; }
    }
}
