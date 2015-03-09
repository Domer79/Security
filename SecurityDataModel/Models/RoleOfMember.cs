using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository;
using Interfaces;

namespace SecurityDataModel.Models
{
    [Table("sec.RoleOfMember")]
    public class RoleOfMember : ModelBase, IRoleOfMember
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdRole { get; set; }

        [StringLength(200)]
        public string RoleName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMember { get; set; }

        [StringLength(200)]
        public string MemberName { get; set; }

        public bool IsUser { get; set; }

        public virtual Role Role { get; set; }
    }
}
