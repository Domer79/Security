using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemTools.Interfaces;
using DataRepository;

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

        [NotMapped]
        string IRole.Description
        {
            get { return RoleDescription; }
            set { RoleDescription = value; }
        }

        public string RoleDescription { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMember { get; set; }

        [NotMapped]
        string IMember.Name
        {
            get { return MemberName; }
            set { MemberName = value; }
        }

        [StringLength(200)]
        public string MemberName { get; set; }

        public bool IsUser { get; set; }

        public virtual Role Role { get; set; }
    }
}
