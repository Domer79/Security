using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemTools.Interfaces;
using DataRepository;

namespace SecurityDataModel.Models
{
    [Table("sec.Members")]
    public class Member : ModelBase, IMember
    {
        [Key]
        public int IdMember { get; set; }

        [Required]
        [Column("name")]
        public string MemberName { get; set; }

        public bool IsUser { get; set; }

        string IMember.Name
        {
            get { return MemberName; }
            set { MemberName = value; }
        }
    }
}
