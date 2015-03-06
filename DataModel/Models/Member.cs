using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Models
{
    [Table("sec.Member")]
    public class Member
    {
        public Member()
        {
            SUser = new HashSet<SUser>();
            SRole = new HashSet<SRole>();
        }

        [Key]
        public int IdMember { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public SGroup SGroup { get; set; }

        public ICollection<SUser> SUser { get; set; }

        public ICollection<SRole> SRole { get; set; }
    }
}
